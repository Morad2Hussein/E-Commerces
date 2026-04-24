

using Domain.Entities.OrderModule;

namespace Presistance.Data.configurations.OrderConfiguration
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
                builder.OwnsOne(o => o.ShippingAddress, sa =>
                {
                    sa.WithOwner();
                });
    
              builder.HasMany(o => o.OrderItems).WithOne();
            builder.Property(o => o.PaymentStatus).HasConversion(
                ps => ps.ToString(),
                ps => Enum.Parse<OrderPaymentStatus>(ps)
            );
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,4)");
        
        
        }
    }
}
