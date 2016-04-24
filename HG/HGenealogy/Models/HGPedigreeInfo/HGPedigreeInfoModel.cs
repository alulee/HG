using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using HGenealogy.Domain;

namespace HGenealogy.Models.HGPedigreeInfo
{
    public class HGPedigreeInfoModel : BaseNopEntityModel
    {
        public int HGPedigreeID { get; set; }
        public string InfoType { get; set; }
        public string InfoTitle { get; set; }
        public string InfoContent { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }

        public HGenealogy.Domain.HGPedigreeMeta hGPedigreeMeta { get; set; }
    }
}