using Domain.Interfaces.Common;
using Domain.Models.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Queue
{
    public class QueueDetail : Unique, IUnique
    {
        public Guid QueueHeaderId { get; set; }

        [ForeignKey(nameof(QueueHeaderId))]
        public virtual QueueHeader? QueueHeader { get; set; }

        public DateTime? DateDelivered { get; set; }
        //public Guid UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public virtual User? User { get; set; }
    }
}