using Application.Dto.Models.Security;
using Application.UseCases.Security.User.Commands.CreateUserCommand;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;
using System.Net;

namespace Application.Validator.Models.Security.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserUseCase>
    {
        public CreateUserValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            RuleFor(x => x.User.UserName)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.UserName));

            RuleFor(x => x.User.Email)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.Email));

            RuleFor(x => x)
                .Must(entity =>
                {
                    if (entity.User.UserName.IsNullOrEmpty()) return true;
                    return !unitOfWork.Users.Exists(x => x.UserName == entity.User.UserName);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.UserName))
                .OverridePropertyName(nameof(UserDto.UserName));

            RuleFor(x => x)
                .Must(entity =>
                {
                    if (entity.User.Email.IsNullOrEmpty()) return true;
                    return !unitOfWork.Users.Exists(x => x.Email == entity.User.Email);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Email))
                .OverridePropertyName(nameof(UserDto.Email));
        }
    }
}