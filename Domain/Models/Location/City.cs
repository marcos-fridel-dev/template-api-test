using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;

namespace Domain.Models.Location
{
    public class City : Unique, IUnique
    {
        public string Name { get; set; }
        public Guid StateId { get; set; }
        public State? State { get; set; }
    }
}