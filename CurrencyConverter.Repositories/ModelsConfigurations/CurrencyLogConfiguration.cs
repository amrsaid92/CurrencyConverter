using CurrencyConverter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyConverter.Repositories.ModelsConfigurations
{
    internal class CurrencyLogConfiguration : IEntityTypeConfiguration<CurrencyLog>
    {
        public void Configure(EntityTypeBuilder<CurrencyLog> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
        }
    }
}
