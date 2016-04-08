using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;

namespace HGenealogy.Models.HGPedigree
{
    public partial class HGTestModel : BaseNopEntityModel
    {
        public HGTestModel()
        {
            //PictureModel = new PictureModel();
            //PagingFilteringContext = new HGPedigreePagingFilteringModel(); 
        }

        public int Test1 { get; set; }
        public int Test2 { get; set; } 
        public string Comment { get; set; } 
    }
}