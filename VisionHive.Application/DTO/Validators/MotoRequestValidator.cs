using VisionHive.Application.DTO.Request;
using FluentValidation;

namespace VisionHive.Application.DTO.Validators;

public class MotoRequestValidator: AbstractValidator<MotoRequest>
{
    public MotoRequestValidator()
    {
        RuleFor(x => x.Placa)
            .MaximumLength(10).WithMessage("A placa deve ter no máximo 10 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Placa));

        RuleFor(x => x.Chassi)
            .MaximumLength(50).WithMessage("O chassi deve ter no máximo 50 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Chassi));

        RuleFor(x => x.NumeroMotor)
            .MaximumLength(50).WithMessage("O número do motor deve ter no máximo 50 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.NumeroMotor));

        RuleFor(x => x.Prioridade)
            .IsInEnum().WithMessage("A prioridade informada é inválida.");

        RuleFor(x => x.PatioId)
            .NotEmpty().WithMessage("O ID do pátio é obrigatório.");

        // Regra de negócio básica
        RuleFor(x => new { x.Placa, x.Chassi, x.NumeroMotor })
            .Must(x => !string.IsNullOrWhiteSpace(x.Placa)
                       || !string.IsNullOrWhiteSpace(x.Chassi)
                       || !string.IsNullOrWhiteSpace(x.NumeroMotor))
            .WithMessage("É necessário informar pelo menos a placa, o chassi ou o número do motor.");
    }
}