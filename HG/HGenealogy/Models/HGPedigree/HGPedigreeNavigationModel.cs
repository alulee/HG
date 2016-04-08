using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace HGenealogy.Models.Catalog
{
    public partial class HGPedigreeNavigationModel : BaseNopModel
    {
        public HGPedigreeNavigationModel()
        {
            Categories = new List<HGPedigreeSimpleModel>();
        }

        public int CurrentCategoryId { get; set; }
        public List<HGPedigreeSimpleModel> Categories { get; set; }
    }
}