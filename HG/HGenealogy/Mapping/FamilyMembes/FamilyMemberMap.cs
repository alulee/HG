using Nop.Data.Mapping;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Mapping.FamilyMembes
{
    public partial class FamilyMemberMap : NopEntityTypeConfiguration<FamilyMember>
    {
        public FamilyMemberMap()
        {
            this.ToTable("HGFamilyMember");
            this.HasKey(c => c.Id);            
            this.Property(c => c.FamilyName).HasMaxLength(30);
            this.Property(c => c.GivenName).IsRequired().HasMaxLength(50);
        }
    }
}