using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain;
using Nop.Core;

namespace HGenealogy.Domain
{
    public partial class HGPedigreeInfo : BaseEntity
    {
        public int HGPedigreeID { get; set; }
        public string InfoType { get; set; }
        public string InfoTitle { get; set; }
        public string InfoContent { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }
    }
}