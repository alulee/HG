using System;
using Nop.Core;
using Nop.Core.Domain.Media;


namespace HGenealogy.Domain.HGFamilyMembers
{
    /// <summary>
    /// Represents a FamilyMember picture mapping
    /// </summary>
    public partial class FamilyMemberPicture : BaseEntity
    {
        /// <summary>
        /// Gets or sets the HGFamilyMember identifier
        /// </summary>
        public int FamilyMemberId { get; set; }

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
        /// Gets the FamilyMember
        /// </summary>
        public virtual FamilyMember FamilyMember { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }  
    }

}
