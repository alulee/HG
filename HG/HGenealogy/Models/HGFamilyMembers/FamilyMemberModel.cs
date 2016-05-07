using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;
using Nop.Web.Framework;
using System.Web.Mvc;
using System;

namespace HGenealogy.Models.HGFamilyMembers
{
    //[Validator(typeof(FamilyMemberValidator))]
    public partial class FamilyMemberModel : BaseNopEntityModel
    {
        public FamilyMemberModel()
        {
        }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.Id")]
        public override int Id { get; set; }

        //picture thumbnail
        [NopResourceDisplayName("HG.FamilyMembers.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [NopResourceDisplayName("姓")]
        [AllowHtml]
        public string FamilyName { get; set; }

        [NopResourceDisplayName("名")]
        [AllowHtml]
        public string GivenName { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.FatherMemberId")]
        public int FatherMemberId { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.MotherMemberId")]
        public int MotherMemberId { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.DateOfBirth")]
        [AllowHtml]
        public string DateOfBirthString { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.CurrentAddressId")]
        public int CurrentAddressId { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.Gender")]
        public string Gender { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.GenerationNo")]
        public string GenerationNo { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.JobDescription")]
        public string JobDescription { get; set; }

        public bool DateOfBirthEnabled { get; set; }
        [NopResourceDisplayName("HG.FamilyMembers.Fields.DateOfBirth")]
        public int? DateOfBirthDay { get; set; }
        [NopResourceDisplayName("HG.FamilyMembers.Fields.DateOfBirth")]
        public int? DateOfBirthMonth { get; set; }
        [NopResourceDisplayName("HG.FamilyMembers.Fields.DateOfBirth")]
        public int? DateOfBirthYear { get; set; }
        public bool DateOfBirthRequired { get; set; }
        public DateTime? ParseDateOfBirth()
        {
            if (!DateOfBirthYear.HasValue || !DateOfBirthMonth.HasValue || !DateOfBirthDay.HasValue)
                return null;

            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = new DateTime(DateOfBirthYear.Value, DateOfBirthMonth.Value, DateOfBirthDay.Value);
            }
            catch { }
            return dateOfBirth;
        }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.MobilePhone")]
        public string MobilePhone { get; set; }

        [NopResourceDisplayName("HG.FamilyMembers.Fields.FullCurrentAddress")]
        public string FullCurrentAddress { get; set; }
    }
}