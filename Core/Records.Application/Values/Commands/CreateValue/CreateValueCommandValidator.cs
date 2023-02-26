using FluentValidation;

namespace Records.Application.Values.Commands.CreateValue
{
    public class CreateValueCommandValidator : AbstractValidator<CreateValueCommand>
    {
        public CreateValueCommandValidator()
        {
            RuleFor(createValueCommand => createValueCommand.dateTime.Date >= new DateTime(2000, 1, 1));
            RuleFor(createValueCommand => createValueCommand.dateTime.Date <= DateTime.Now);
            RuleFor(createValueCommand => createValueCommand.timeInt >= 0);
            RuleFor(createValueCommand => createValueCommand.timeFloat >= 0);  
        }
    }
}
