using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;

namespace HGenealogy.Models.HGFamilyMembers
{
    public partial class FamilyMemberOverviewModel : BaseNopEntityModel
    {
        public FamilyMemberOverviewModel()
        {
            DefaultPictureModel = new PictureModel();
        }

        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Description { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
 
    }
}