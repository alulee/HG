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
    /// Represents a HGTest
    /// </summary>
    public partial class HGTest : BaseEntity, ILocalizedEntity, ISlugSupported 
    {       
        /// <summary>
        /// Gets or sets the Test1
        /// </summary>
        public int Test1 { get; set; }

        /// <summary>
        /// Gets or sets the Test2
        /// </summary>
        public int Test2 { get; set; }
 
        /// <summary>
        /// Gets or sets Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
