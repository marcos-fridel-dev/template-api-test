using Application.Dto.Models.Security;
using FluentValidation;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;
using System.Net;
using System.Text;

namespace Application.UseCases.Security.User.Commands.DeleteUserCommand
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserUseCase>
    {
        public DeleteUserValidator(IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            RuleFor(x => x)
                .Must(entity =>
                {
                    return unitOfWork.Users.Exists(x => x.Id == entity.Id && !x.IsDeleted);
                })
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage(x => localizer.GetString(Language.IsNotFound, Language.Role))
                .OverridePropertyName(nameof(RoleDto.Name));
        }
    }
}