using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Tamanho máximo do titulo é de 30 caracteres.");

            RuleFor(p => p.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo da descrição é de 255 caracteres.");

        }
    }
}
