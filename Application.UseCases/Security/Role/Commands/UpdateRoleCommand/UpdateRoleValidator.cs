using Application.Dto.Models.Security;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;
using System.Net;
using System.Text;

namespace Application.UseCases.Security.Role.Commands.UpdateRoleCommand
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleUseCase>
    {
        public UpdateRoleValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
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
                    return !unitOfWork.Roles.Exists(x => x.Id != entity.Id && x.Name == entity.Role.Name && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsAlreadyFound, Language.Role))
                .OverridePropertyName(nameof(RoleDto.Name));
        }
    }
}