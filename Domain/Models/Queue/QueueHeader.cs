using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;

namespace Domain.Models.Queue
{
    public class QueueHeader : Unique, IUnique
    {
        public DateTime DateCreated { get; set; }
        public string EntityName { get; set; }
        public string Body { get; set; }
        public bool ForAll { get; set; }
    }
}