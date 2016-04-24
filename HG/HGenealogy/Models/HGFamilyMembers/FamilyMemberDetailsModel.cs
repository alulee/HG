using System;
using System.Collections.Generic;
using Nop.Services.Localization;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using HGenealogy.Models.Media;
using HGenealogy.Domain;


namespace HGenealogy.Models.HGFamilyMembers
{
    public partial class FamilyMemberDetailsModel : BaseNopEntityModel
    {
        public FamilyMemberDetailsModel()
        {
            DefaultPictureModel = new PictureModel();
            PictureModels = new List<PictureModel>();
            Breadcrumb = new FamilyMemberBreadcrumbModel();
            RelatedMembers = new List<FamilyMemberOverviewModel>();
        }

        //picture(s)
        public bool DefaultPictureZoomEnabled { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
        public IList<PictureModel> PictureModels { get; set; }

        public FamilyMemberBreadcrumbModel Breadcrumb { get; set; }
        public FamilyMemberOverviewModel FatherMember { get; set; }
        public FamilyMemberOverviewModel MotherMember { get; set; }
        public IList<FamilyMemberOverviewModel> RelatedMembers { get; set; }
        public IList<FamilyMemberAttributeModel> FamilyMemberAttributes { get; set; }

        public string FamilyName { get; set; }  
        public string GivenName { get; set; }
        public string Description { get; set; }
        public string GenerationNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Gender { get; set; }
        public string JobDescription { get; set; }

        #region Nested Classes

        public partial class FamilyMemberBreadcrumbModel : BaseNopModel
        {
            public FamilyMemberBreadcrumbModel()
            {
                //CategoryBreadcrumb = new List<CategorySimpleModel>();
            }

            public bool Enabled { get; set; }
            public int FamilyMemberId { get; set; }
            public string FamilyMemberName { get; set; }
            //public IList<PedigreeSimpleModel> CategoryBreadcrumb { get; set; }
        }
 
        public partial class FamilyMemberAttributeModel : BaseNopEntityModel
        {
            public FamilyMemberAttributeModel()
            {
                AllowedFileExtensions = new List<string>();
                Values = new List<FamilyMemberAttributeValueModel>();
            }

            public int FamilyMemberId { get; set; }

            public int FamilyMemberAttributeId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string TextPrompt { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Default value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }
            /// <summary>
            /// Selected day value for datepicker
            /// </summary>
            public int? SelectedDay { get; set; }
            /// <summary>
            /// Selected month value for datepicker
            /// </summary>
            public int? SelectedMonth { get; set; }
            /// <summary>
            /// Selected year value for datepicker
            /// </summary>
            public int? SelectedYear { get; set; }

            /// <summary>
            /// A value indicating whether this attribute depends on some other attribute
            /// </summary>
            public bool HasCondition { get; set; }

            /// <summary>
            /// Allowed file extensions for user uploaded files
            /// </summary>
            public IList<string> AllowedFileExtensions { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<FamilyMemberAttributeValueModel> Values { get; set; }

        }

        public partial class FamilyMemberAttributeValueModel : BaseNopEntityModel
        {
            public FamilyMemberAttributeValueModel()
            {
                PictureModel = new PictureModel();
            }

            public string Name { get; set; }

            public string ColorSquaresRgb { get; set; }
 
            public bool IsPreSelected { get; set; }

            //picture model is used when we want to override a default product picture when some attribute is selected
            public PictureModel PictureModel { get; set; }
        }

        #endregion
    }
}