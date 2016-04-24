using Nop.Data.Mapping;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Mapping.FamilyMembes
{
    public partial class FamilyMemberRelationMap : NopEntityTypeConfiguration<FamilyMemberRelation>
    {
        public FamilyMemberRelationMap()
        {
            this.ToTable("HGFamilyMemberRelation");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.FamilyMember)
                .WithMany()
                .HasForeignKey(pp => pp.FamilyMemberId);
        }
    }
}