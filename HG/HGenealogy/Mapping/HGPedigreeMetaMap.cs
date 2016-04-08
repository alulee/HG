using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy
{
    public partial class HGPedigreeMetaMap : NopEntityTypeConfiguration<HGPedigreeMeta>
    {
        public HGPedigreeMetaMap()
        {
            this.ToTable("HGPedigreeMeta");
            this.HasKey(c => c.Id);
            this.Property(c => c.Title).IsRequired().HasMaxLength(100);
            this.Property(c => c.FamilyName).HasMaxLength(10);
        }
    }
}