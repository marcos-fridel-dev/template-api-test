using Domain.Interfaces.Common;
using Domain.Models.Common;
using Shared.Localization.Enums;
using System.Collections.Generic;

namespace Domain.Models.Location
{
    public class Country : Unique, IUnique
    {
        public string Name { get; set; }
        public Cultures SupportedCulture { get; set; }
        public List<Currency> Currencies { get; }
        public List<State> States { get; }
    }
}