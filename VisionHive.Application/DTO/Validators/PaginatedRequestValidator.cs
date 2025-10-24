﻿using FluentValidation;
using VisionHive.Application.DTO.Request;

namespace VisionHive.Application.DTO.Validators;

public class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    private const int MaxPageSize = 200;
    
    public PaginatedRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber deve ser >= 1.");
        
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, MaxPageSize)
            .WithMessage($"PageSize deve estar entre 1 e {MaxPageSize}.");

        RuleFor(x => x.Subject)
            .Must(_ => true)
            .WithMessage("Subject é opcional.");
        
    }
}