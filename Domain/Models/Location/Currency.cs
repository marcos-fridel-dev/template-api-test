using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;

namespace Domain.Models.Location
{
    public class Currency : Unique, IUnique
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public bool IsFiat { get; set; }
    }
}