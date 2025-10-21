using FluentValidation;
using VisionHive.Application.DTO.Request;

namespace VisionHive.Application.DTO.Validators;

public class PatioRequestValidator : AbstractValidator<PatioRequest>
{
    public PatioRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do pátio é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do pátio deve ter no máximo 100 caracteres.");

        RuleFor(x => x.LimiteMotos)
            .GreaterThan(0).WithMessage("O limite de motos deve ser maior que zero.");

        RuleFor(x => x.FilialId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");
    }
}