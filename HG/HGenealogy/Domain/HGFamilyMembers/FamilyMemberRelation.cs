using Nop.Core;
using Nop.Core.Domain.Media;
using System;

namespace HGenealogy.Domain.HGFamilyMembers
{
    /// <summary>
    /// Represents a FamilyMember Relation 
    /// </summary>
    public partial class FamilyMemberRelation : BaseEntity
    {
        /// <summary>
        /// Gets or sets the FamilyMember identifier
        /// </summary>
        public int FamilyMemberId { get; set; }

        /// <summary>
        /// Gets or sets the RelatedFamilyMemberId identifier
        /// </summary>
        public int RelatedFamilyMemberId { get; set; }

        /// <summary>
        /// Gets or sets the RelationType
        /// </summary>
        public int RelationType { get; set; }

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

        /// <summary>
        /// Gets the FamilyMember
        /// </summary>
        public virtual FamilyMember FamilyMember { get; set; }

    }

}
