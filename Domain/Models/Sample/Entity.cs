using Domain.Interfaces.Common;
using Domain.Models.Common;

namespace Domain.Models.Sample
{
    public class Entity : Auditable, IAuditable
    {
        public string Name { get; set; }
    }
}
