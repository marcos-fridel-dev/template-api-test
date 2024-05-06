using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;
using System;
using System.Collections.Generic;

namespace Application.Dto.Models.Location
{
    public class StateDto : UniqueDto, IUniqueDto
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public string CountryName => this.Country?.Name ?? string.Empty;
        public List<CityDto> Cities { get; set; }
    }

    public class StatePostDto
    {
        public string Name { get; set; }
    }

    public class StateUpdateDto : StatePostDto, IIdentityDto
    {
        public Guid Id { get; set; }
    }
}