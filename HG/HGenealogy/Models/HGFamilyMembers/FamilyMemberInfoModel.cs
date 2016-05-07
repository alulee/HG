using System;
using System.Collections.Generic;
using Nop.Services.Localization;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using HGenealogy.Models.Media;
using HGenealogy.Domain;
using System.Web.Mvc;


namespace HGenealogy.Models.HGFamilyMembers
{
    public partial class FamilyMemberInfoModel : BaseNopEntityModel
    {    
        public int FamilyMemberId { get; set; }
        public string InfoType { get; set; }
        public string InfoTitle { get; set; }
        public string InfoContent { get; set; }
        public string Address { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }
 
    }

    public partial class FamilyMemberInfoNavigationModel : BaseNopEntityModel
    {
        public FamilyMemberInfoNavigationModel()
        {
            FamilyMemberInfosByInfoType = new List<FamilyMemberInfoModel>();
            SelectedTab = FamilyMemberInfoNavigationEnum.Biography; //default selectedtab
            SelectedInfoTypeTitle = SelectedTab.ToString();
        }

        public int CurrentFamilyMemberId { get; set; }
        public FamilyMemberInfoNavigationEnum SelectedTab { get; set; }
        public string SelectedInfoTypeTitle { get; set; }
        public IList<FamilyMemberInfoModel> FamilyMemberInfosByInfoType { get; set; }
    }

    public enum FamilyMemberInfoNavigationEnum
    {
        PersonalDetails = 0,
        Biography = 10,
        Event = 20
    }
}