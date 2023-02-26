using Microsoft.AspNetCore.Mvc;
using Records.WebApi.Models;
using Records.Application.Values.Commands.CreateValue;
using Records.Application.Values.Queries.GetValueList;
using Records.Application.Results.Commands.CreateResult;
using Records.Application.Results.Queries.GetResultList;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Linq;
using Records.Application.Values.Commands.DeleteCommand;
using Records.Application.Results.Commands.DeleteCommand;

namespace Records.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MainController : BaseController
    {
        private readonly IMapper _mapper;
        public MainController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Получение списка результатов расчетов по таблице "Values". Сами результаты хранятся в таблице "Results".
        /// Могут применяться фильтры: 
        /// - По имени файла,
        /// - По времени запуска первой операции(в диапазоне от, до)
        /// - По среднему показателю(в диапазоне от, до)
        /// - По среднему времени(в диапазоне от, до)
        /// </summary>
        /// <remarks>
        /// Sample request(данные в диапазоне(от,до)):
        /// GET /GetResults?minValueDatetime=2021.01.01 05:05:05-2023.01.01 07:07:07
        /// GET /GetResults?averageIndicator=3222-3245        
        /// </remarks>
        /// <param name="filename"></param>
        /// <param name="minValueDatetime"></param>
        /// <param name="averageIndicator"></param>
        /// <param name="averageTime"></param>
        /// <returns>Returns ResultListVm</returns>
        [HttpGet("GetResults")]
        public async Task<ActionResult<ResultListVm>> GetResults(string? filename, string? minValueDatetime, string? averageIndicator, string? averageTime)
        {
            var query = new GetResultListQuery 
            {
                filename = filename,
                minValueDatetime = minValueDatetime,
                averageIndicator = averageIndicator,
                averageTime = averageTime
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Получение значений таблицы "Values" по имени файла
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /GetValue?filename=test.csv
        /// </remarks>
        /// <param name="filename"></param>
        /// <returns>Returns ValueListVm</returns>
        [HttpGet("GetValue")]
        public async Task<ActionResult<ValueListVm>> GetValue(string filename)
        {
            var query = new GetValueListQuery { filename = filename };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Выполняется действия, согласно заданию
        /// Принимает файл вида .csv, получает значения из файла и сохраняет их в БД в табдицу Values.
        /// Если значения из файла с таким именем уже записаны, соответствующие значение перезаписываются
        /// Далее вычисляются результаты, котоыре помещаются в таблицу Results
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/Main
        /// {
        ///     data = test.csv
        /// }
        /// </remarks>
        /// <returns>Return Ok()</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost()]       
        public async Task<IActionResult> Post()
        {
            // Получение CSV файла
            IFormFile csvFile = Request.Form.Files.First();                
            var records = new List<CreateValueDto>();

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture) 
                { 
                    Delimiter = ";", 
                    Encoding = Encoding.UTF8,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using var csv = new CsvReader(reader, config);
                records = csv.GetRecords<CreateValueDto>().ToList(); 
            }

            if(records.Count < 1 || records.Count > 10000) throw new Exception("Количество строк в файле не может быть меньше 1 и больше 10 000");

            try
            {
                // Подготовка бд к записи полученных значений
                var commandForDeleteValuesInTables = new DeleteValueCommand { filename = csvFile.FileName };
                var commandForDeleteInResultsInTables = new DeleteResultCommand { filename = csvFile.FileName };
                await Mediator.Send(commandForDeleteValuesInTables);               
                await Mediator.Send(commandForDeleteInResultsInTables);

                //Сохраниние в таблицу Values
                foreach (var item in records)
                {
                    item.filename = csvFile.FileName;
                    var command = _mapper.Map<CreateValueCommand>(item);
                    command.Id = Id;
                    var valueStringId = await Mediator.Send(command);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

            // Вычисление и сохранение Results  
            var tempData = new CreateResultDto()
            {
                allTime = records.Max(a => a.dateTime).Subtract(records.Min(a => a.dateTime)).Ticks,
                minValueDatetime = records.Min(a => a.dateTime),
                averageTime = records.Average(a => a.timeInt),
                averageIndicator = records.Average(a => a.timeFloat),
                medianaOfIndicator = CreateResultDto.Median(records),
                maxValueOfIndicator = records.Max(a => a.timeFloat),
                minValueOfIndicator = records.Min(a => a.timeFloat),
                countOfString = records.Count,
                filename= csvFile.FileName
            };

            var commandSaveResult = _mapper.Map<CreateResultCommand>(tempData);
            commandSaveResult.Id = Id;
            var valueStringIdOfSaveResult = await Mediator.Send(commandSaveResult);

            return Ok();
        }     
    }
}
