
using Nop.Core;
using Nop.Core.Domain.Media;

namespace HGenealogy.Domain
{
    /// <summary>
    /// Represents a HGFamilyMember picture mapping
    /// </summary>
    public partial class HGFamilyMemberPicture : BaseEntity
    {
        /// <summary>
        /// Gets or sets the HGFamilyMember identifier
        /// </summary>
        public int HGFamilyMemberId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        
        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Gets the HGFamilyMember
        /// </summary>
        public virtual HGFamilyMember HGFamilyMember { get; set; }
    }

}
