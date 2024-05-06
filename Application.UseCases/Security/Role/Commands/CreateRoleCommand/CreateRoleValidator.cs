using Application.Dto.Models.Security;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;
using System.Net;

namespace Application.UseCases.Security.Role.Commands.CreateRoleCommand
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleUseCase>
    {
        public CreateRoleValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            RuleFor(x => x.Role.Name)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage(x => localizer.GetString(Language.ShouldNotBeEmpty, Language.Role));

            RuleFor(x => x)
                .Must(entity =>
                {
                    if (entity.Role.Name.IsNullOrEmpty()) return true;
                    return !unitOfWork.Roles.Exists(x => x.Name == entity.Role.Name);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Role))
                .OverridePropertyName(nameof(RoleDto.Name));

        }
    }
}