using FluentValidation;

namespace Records.Application.Results.Commands.DeleteCommand
{
    public class DeleteResultCommandValidator : AbstractValidator<DeleteResultCommand>
    {
        public DeleteResultCommandValidator() 
        {
            RuleFor(deleteCommand => deleteCommand.filename).NotEqual(string.Empty);
        }
    }
}
