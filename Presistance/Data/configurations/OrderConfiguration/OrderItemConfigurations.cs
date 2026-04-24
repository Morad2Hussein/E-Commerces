

using Domain.Entities.OrderModule;

namespace Presistance.Data.configurations.OrderConfiguration
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           builder.Property(oi => oi.Price).HasColumnType("decimal(18,4)");
            builder.OwnsOne(p => p.Product, p => p.WithOwner());
        }
    }
}
