using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;
using Shared.Localization.Enums;
using System;
using System.Collections.Generic;

namespace Application.Dto.Models.Location
{
    public class CountryDto : UniqueDto, IUniqueDto
    {
        public string Name { get; set; }
        public Cultures SupportedCulture { get; set; }
        public string SupportedCultureName => this.SupportedCulture.ToString();
        public List<CurrencyDto>? Currencies { get; set; }
        public List<StateDto>? States { get; set; }
    }

    public class CountryPostDto
    {
        public string Name { get; set; }
    }

    public class CountryUpdateDto : CountryPostDto, IIdentityDto
    {
        public Guid Id { get; set; }
    }
}