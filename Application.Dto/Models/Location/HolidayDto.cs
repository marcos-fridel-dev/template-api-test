using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;
using System;

namespace Application.Dto.Models.Location
{
    public class HolidayDto : UniqueDto, IUniqueDto
    {
        public int? CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public string CountryName => this.Country?.Name ?? string.Empty;
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }

    public class HolidayCreateUpdateDto
    {
        public int CountryId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}