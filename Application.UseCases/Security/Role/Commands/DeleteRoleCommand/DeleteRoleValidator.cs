using Application.Dto.Models.Security;
using Application.UseCases.Security.Role.Commands.UpdateRoleCommand;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;
using System.Net;
using System.Text;

namespace Application.UseCases.Security.Role.Commands.DeleteRoleCommand
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleUseCase>
    {
        public DeleteRoleValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            RuleFor(x => x)
                .Must(entity =>
                {
                    return unitOfWork.Roles.Exists(x => x.Id == entity.Id && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsNotFound, Language.Role))
                .OverridePropertyName(nameof(RoleDto.Name));
        }
    }
}