using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy
{
    public partial class HGFamilyMemberMap : NopEntityTypeConfiguration<HGFamilyMember>
    {
        public HGFamilyMemberMap()
        {
            this.ToTable("HGFamilyMember");
            this.HasKey(c => c.Id);            
            this.Property(c => c.FamilyName).HasMaxLength(30);
            this.Property(c => c.GivenName).IsRequired().HasMaxLength(50);
            this.Property(c => c.Description);
        }
    }
}