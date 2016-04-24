using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

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