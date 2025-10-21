using FluentValidation;
using VisionHive.Application.DTO.Request;

namespace VisionHive.Application.DTO.Validators;

public class FilialRequestValidator : AbstractValidator<FilialRequest>
{ 
    public FilialRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da filial deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage("O bairro é obrigatório.")
            .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("O CNPJ é obrigatório.")
            .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter exatamente 14 dígitos (somente números).");
    }
}