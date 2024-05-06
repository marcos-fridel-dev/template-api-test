using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;
using System;

namespace Application.Dto.Models.Location
{
    public class CityDto : UniqueDto, IUniqueDto
    {
        public string Name { get; set; }
        public int? StateId { get; set; }
        public StateDto? State { get; set; }
        public string StateName => this.State?.Name ?? string.Empty;
        public int? CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public string? CountryName => this.Country?.Name ?? string.Empty;
    }

    public class CityPostDto
    {
        public string Name { get; set; }
    }

    public class CityUpdateDto : CityPostDto, IIdentityDto
    {
        public Guid Id { get; set; }
    }
}