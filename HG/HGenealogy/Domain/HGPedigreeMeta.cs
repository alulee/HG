using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain;
using Nop.Core;

namespace HGenealogy.Domain
{
    /// <summary>
    /// Represents a HGPedigreeMeta
    /// </summary>
    public partial class HGPedigreeMeta : BaseEntity, ILocalizedEntity, ISlugSupported
    {       
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the FamilyName
        /// </summary>
        public string FamilyName { get; set; }


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
