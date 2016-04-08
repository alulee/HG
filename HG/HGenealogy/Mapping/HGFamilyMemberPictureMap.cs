using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy
{
    public partial class HGFamilyMemberPictureMap : NopEntityTypeConfiguration<HGFamilyMemberPicture>
    {
        public HGFamilyMemberPictureMap()
        {
            this.ToTable("HGFamilyMember_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.HGFamilyMember)
                .WithMany()
                .HasForeignKey(pp => pp.HGFamilyMemberId);

            this.HasRequired(pp => pp.Picture)
                .WithMany()
                .HasForeignKey(pp => pp.PictureId);

        }
    }
}