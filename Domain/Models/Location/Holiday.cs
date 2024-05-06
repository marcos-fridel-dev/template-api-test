using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;

namespace Domain.Models.Location
{
    public class Holiday : Auditable, IAuditable
    {
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}