using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModels;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.TypeConfigurations
{
    public class IndividualDataModelConfiguration : IEntityTypeConfiguration<IndividualDataModel>
    {
        public void Configure(EntityTypeBuilder<IndividualDataModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.Birthdate).IsRequired();
            builder.Property(f => f.FirstName).IsRequired();
            builder.Property(f => f.LastName).IsRequired();
        }
    }
}