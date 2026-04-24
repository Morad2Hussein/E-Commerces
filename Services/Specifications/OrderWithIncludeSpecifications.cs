
using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : BaseSpecifications<Order, Guid>
    {

        // Get Order By Id with Include [OrderItems and DeliveryMethod]
        public OrderWithIncludeSpecifications(Guid id) : base(o=>o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
        // Get Order By Email with Include [OrderItems and DeliveryMethod]
        public OrderWithIncludeSpecifications(string useremail) : base(o=>o.UserName == useremail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
