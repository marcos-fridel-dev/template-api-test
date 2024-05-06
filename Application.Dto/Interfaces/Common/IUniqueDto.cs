namespace Application.Dto.Interfaces.Common
{
    public interface IUniqueDto : IIdentityDto
    {
        bool IsDeleted { get; set; }
        string IsDeletedLabel { get; }
    }
}