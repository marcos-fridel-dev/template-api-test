using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;
using Shared.Common.Extensions.Core;
using System;

namespace Application.Dto.Models.Location
{
    public class CurrencyDto : UniqueDto, IUniqueDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public Guid? CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public string? CountryName => this.Country?.Name ?? string.Empty;
        public bool IsFiat { get; set; }
        public string IsFiatText => this.IsFiat.ToYesNo();
    }

    public class CurrencyPostDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public Guid CountryId { get; set; }
        public bool IsFiat { get; set; }
    }

    public class CurrencyUpdateDto : CurrencyPostDto, IIdentityDto
    {
        public Guid Id { get; set; }
    }
}