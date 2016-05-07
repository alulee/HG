
using Nop.Core;
using Nop.Core.Domain.Media;
using System;

namespace HGenealogy.Domain.HGFamilyMembers
{
    /// <summary>
    /// Represents a FamilyMember Relation 
    /// </summary>
    public partial class FamilyMemberInfo : BaseEntity
    {
        /// <summary>
        /// Gets or sets the FamilyMember identifier
        /// </summary>
        public int FamilyMemberId { get; set; }
 
        /// <summary>
        /// Gets or sets the InfoType
        /// </summary>
        public string InfoType { get; set; }

        /// <summary>
        /// Gets or sets the InfoTitle
        /// </summary>
        public string InfoTitle { get; set; }

        /// <summary>
        /// Gets or sets the InfoContent
        /// </summary>
        public string InfoContent { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        /// 
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public decimal? Latitude { get; set; } 

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
 
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
