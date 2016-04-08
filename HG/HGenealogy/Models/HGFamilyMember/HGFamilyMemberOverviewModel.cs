using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;

namespace HGenealogy.Models.HGFamilyMember
{
    public partial class HGFamilyMemberOverviewModel : BaseNopEntityModel
    {
        public HGFamilyMemberOverviewModel()
        {
            DefaultPictureModel = new PictureModel();
        }

        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Description { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
 
    }
}