using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace Domain.Models.Location
{
    public class State : Unique, IUnique
    {
        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public Country? Country { get; set; }

        public List<City> Cities { get; }
    }
}