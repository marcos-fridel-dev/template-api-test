﻿using Application.Dto.Models.Location;
using Application.UseCases.Location.Currency.Commands.CreateCurrencyCommand;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;
using System.Net;
using System.Text;

namespace Application.Validator.Models.Location.Currency
{
    public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyUseCase>
    {
        public CreateCurrencyValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            RuleFor(x => x.Currency.Name)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.Name));

            RuleFor(x => x.Currency.Code)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.Code));

            RuleFor(x => x.Currency.Symbol)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.Symbol));

            RuleFor(x => x.Currency.CountryId)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.YouMustSelectAtLeastOneItem, Language.Country));

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(entity =>
                {
                    if (entity.Currency.Name.IsNullOrEmpty()) return true;
                    return !unitOfWork.Currencies.Exists(x => x.Name == entity.Currency.Name && x.CountryId == entity.Currency.CountryId && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Name))
                .OverridePropertyName(nameof(CurrencyPostDto.Name));

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(entity =>
                {
                    if (entity.Currency.Code.IsNullOrEmpty()) return true;
                    return !unitOfWork.Currencies.Exists(x => x.Code == entity.Currency.Code && x.CountryId == entity.Currency.CountryId && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Code))
                .OverridePropertyName(nameof(CurrencyPostDto.Code));

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(entity =>
                {
                    if (entity.Currency.Symbol.IsNullOrEmpty()) return true;
                    return !unitOfWork.Currencies.Exists(x => x.Symbol == entity.Currency.Symbol && x.CountryId == entity.Currency.CountryId && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Symbol))
                .OverridePropertyName(nameof(CurrencyPostDto.Symbol));
        }
    }
}