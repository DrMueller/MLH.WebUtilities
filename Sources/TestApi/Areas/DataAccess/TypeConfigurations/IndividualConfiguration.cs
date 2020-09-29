using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.TypeConfigurations
{
    public class IndividualConfiguration : IEntityTypeConfiguration<Individual>
    {
        public void Configure(EntityTypeBuilder<Individual> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.Birthdate).IsRequired();
            builder.Property(f => f.FirstName).IsRequired();
            builder.Property(f => f.LastName).IsRequired();
        }
    }
}