using Nop.Data.Mapping;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Mapping.FamilyMembes
{
    public partial class FamilyMemberInfoMap : NopEntityTypeConfiguration<FamilyMemberInfo>
    {
        public FamilyMemberInfoMap()
        {
            this.ToTable("HGFamilyMemberInfo");
            this.HasKey(pp => pp.Id);
        }
    }
}