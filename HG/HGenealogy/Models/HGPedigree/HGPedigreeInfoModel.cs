using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using HGenealogy.Domain;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HGenealogy.Models.HGPedigree
{
    public class HGPedigreeInfoModel : BaseNopEntityModel
    {
        public int HGPedigreeID { get; set; }
        public string InfoType { get; set; }
        [DisplayName("資料抬頭")]
        [Required]
        public string InfoTitle { get; set; }
        [DisplayName("描述")]
        [Required]
        public string InfoContent { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }

        public HGenealogy.Domain.HGPedigreeMeta hGPedigreeMeta { get; set; }
        //public IDictionary<string, string> infoTypeDic { get; set; }
        //public int currentHGPedigreeID { get; set; }
        //public string currentInfoType { get; set; }
    }
}