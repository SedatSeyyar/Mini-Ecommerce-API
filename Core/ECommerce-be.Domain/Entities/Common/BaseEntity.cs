namespace ECommerce_be.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        // This property is virtual because it may not be used in the subclass.
        virtual public DateTime UpdatedTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedTime { get; set; }
    }
}
