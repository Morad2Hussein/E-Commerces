

using Domain.Entities.OrderModule;

namespace Presistance.Data.configurations.OrderConfiguration
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(18,4)");
        }
    }
}
