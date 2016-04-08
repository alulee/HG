using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Services.Localization;
using HGenealogy.Models.Media;
using HGenealogy.Domain;

namespace HGenealogy.Models.HGFamilyMember
{
    public partial class HGFamilyMemberDetailsModel : BaseNopEntityModel
    {
        public HGFamilyMemberDetailsModel()
        {
            DefaultPictureModel = new PictureModel();
            PictureModels = new List<PictureModel>();
            //MemberAttributes = new List<MemberAttributeModel>();
            AssociatedMembers = new List<HGFamilyMemberDetailsModel>();
        }

        //picture(s)
        public bool DefaultPictureZoomEnabled { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
        public IList<PictureModel> PictureModels { get; set; }

        public string FamilyName { get; set; }  
        public string GivenName { get; set; }
        public string Description { get; set; }

        public IList<HGFamilyMemberDetailsModel> AssociatedMembers { get; set; }
 
    }
}