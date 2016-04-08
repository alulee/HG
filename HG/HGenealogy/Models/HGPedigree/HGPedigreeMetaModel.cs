using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;

namespace HGenealogy.Models.HGPedigree
{
    public partial class HGPedigreeMetaModel : BaseNopEntityModel
    {
        public HGPedigreeMetaModel()
        {
            PagingFilteringContext = new HGPedigreePagingFilteringModel(); 
        }

        public string Title { get; set; }
        public string FamilyName { get; set; }  

        public HGPedigreePagingFilteringModel PagingFilteringContext { get; set; }
 
    }
}