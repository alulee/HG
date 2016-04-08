using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy
{
    public partial class HGTestMap : NopEntityTypeConfiguration<HGTest>
    {
        public HGTestMap()
        {
            this.ToTable("HGTest");
            this.HasKey(c => c.Id);
            this.Property(c => c.Comment).IsRequired().HasMaxLength(400);
        }
    }
}