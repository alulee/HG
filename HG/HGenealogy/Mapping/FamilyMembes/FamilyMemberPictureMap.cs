using Nop.Data.Mapping;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Mapping.FamilyMembes
{
    public partial class FamilyMemberPictureMap : NopEntityTypeConfiguration<FamilyMemberPicture>
    {
        public FamilyMemberPictureMap()
        {
            this.ToTable("HGFamilyMember_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.FamilyMember)
                .WithMany()
                .HasForeignKey(pp => pp.FamilyMemberId);

            this.HasRequired(pp => pp.Picture)
                .WithMany()
                .HasForeignKey(pp => pp.PictureId);

        }
    }
}