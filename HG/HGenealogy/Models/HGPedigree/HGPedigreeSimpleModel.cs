using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace HGenealogy.Models.Catalog
{
    public class HGPedigreeSimpleModel : BaseNopEntityModel
    {
        public HGPedigreeSimpleModel()
        {
          
        }

        public string Name { get; set; } 

        public bool IncludeInTopMenu { get; set; }
 
    }
}