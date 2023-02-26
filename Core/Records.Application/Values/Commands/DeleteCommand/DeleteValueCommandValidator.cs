using FluentValidation;

namespace Records.Application.Values.Commands.DeleteCommand
{
    public class DeleteValueCommandValidator : AbstractValidator<DeleteValueCommand>
    {
        public DeleteValueCommandValidator()
        {
            RuleFor(deleteCommand => deleteCommand.filename).NotEqual(string.Empty);
        }
    }
}
