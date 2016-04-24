using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain;
using Nop.Core;

namespace HGenealogy.Domain
{
    public partial class HGPedigreeEvent : BaseEntity, ILocalizedEntity//, ISlugSupported, IAclSupported
    {
        public int ID { get; set; }
        public int HGPedigreeID { get; set; }
        public string EventTitle { get; set; }
        public string EventContent { get; set; }
        public DateTime EventDateOnUtc { get; set; }
        public string EventPlace { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }
        public string MetaKeywords { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }

    }
}