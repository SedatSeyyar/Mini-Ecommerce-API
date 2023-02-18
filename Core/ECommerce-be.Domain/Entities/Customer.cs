using ECommerce_be.Domain.Entities.Common;

namespace ECommerce_be.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
