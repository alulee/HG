using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain;
using Nop.Core;

namespace HGenealogy.Domain.HGFamilyMembers
{
    /// <summary>
    /// Represents a FamilyMember
    /// </summary>
    public partial class FamilyMember : BaseEntity, ILocalizedEntity, ISlugSupported
    {       
        /// <summary>
        /// Gets or sets the FamilyName
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the GivenName
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the FatherMember identifier
        /// </summary>
        public int FatherMemberId { get; set; }

        /// <summary>
        /// Gets or sets the MotherMember identifier
        /// </summary>
        public int MotherMemberId { get; set; }

        /// <summary>
        /// Gets or sets the BirthDay
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the Current Address identifier
        /// </summary>
        public int CurrentAddressId { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the MobilePhone
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the GenerationNo
        /// </summary>
        public int GenerationNo { get; set; }

        /// <summary>
        /// Gets or sets the JobDescription
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// Gets or sets the IsPublic
        /// </summary>
        public bool IsPublic { get; set; }
 
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }
 
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
