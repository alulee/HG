using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
//using Nop.Services.Catalog;
//using Nop.Services.Directory;
//using Nop.Services.Localization;
//using Nop.Services.Media;
//using Nop.Services.Security;
//using Nop.Services.Seo;
//using Nop.Services.Tax;
//using Nop.Web.Models.Catalog;
//using Nop.Web.Models.Media;
using Nop.Services.Localization;
using Nop.Services.Media;

using HGenealogy.Infrastructure.Cache;
using HGenealogy.Services;
using HGenealogy.Domain;
using HGenealogy.Models.Media;
using HGenealogy.Models.HGPedigree;
using HGenealogy.Models.HGFamilyMembers;
using HGenealogy.Domain.HGFamilyMembers;
using HGenealogy.Services.HGFamilyMembers;

namespace HGenealogy.Extensions
{
    //here we have some methods shared between controllers
    
    public static class ControllerExtensions
    {

        public static IEnumerable<FamilyMemberOverviewModel> PrepareFamilyMemberOverviewModels(this Controller controller,
            IWorkContext workContext,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            MediaSettings mediaSettings,
            IFamilyMemberService familyMemberService,
            IEnumerable<FamilyMember> familyMembers,
            bool preparePictureModel = true,
            int? hgFamilyMemberThumbPictureSize = null)
        {
            if (familyMembers == null)
                throw new ArgumentNullException("familyMembers");

            var models = new List<FamilyMemberOverviewModel>();
            foreach (var familyMember in familyMembers)
            {
                var model = PrepareFamilyMemberOverviewModel(controller, workContext, localizationService, pictureService, webHelper,
                                                             cacheManager, mediaSettings,familyMemberService, familyMember);
                if (model != null)
                    models.Add(model);
            }
            return models;
        }

        public static FamilyMemberOverviewModel PrepareFamilyMemberOverviewModel(this Controller controller,
           IWorkContext workContext,
           ILocalizationService localizationService,
           IPictureService pictureService,
           IWebHelper webHelper,
           ICacheManager cacheManager,
           MediaSettings mediaSettings,
           IFamilyMemberService familyMemberService,
           FamilyMember familyMember,
           bool preparePictureModel = true,
           int? familyMemberThumbPictureSize = null,
            bool returnEmptyModel = false)
        {
            var model = new FamilyMemberOverviewModel();
            if (familyMember == null)
            {
                if (!returnEmptyModel)
                    throw new ArgumentNullException("familyMember");
                else
                {
                    model.Id = 0;
                    model.FamilyName = "";
                    model.GivenName = "";
                    model.Description = "";
                }   
            }
            else
            {
                model.Id = familyMember.Id;
                model.FamilyName = familyMember.GetLocalized(x => x.FamilyName);
                model.GivenName = familyMember.GetLocalized(x => x.GivenName);
                model.Description = familyMember.GetLocalized(x => x.Description);
            }
            

            //picture
            if (preparePictureModel)
            {
                #region Prepare FamilyMember picture

                //If a size has been set in the view, we use it in priority
                int pictureSize = familyMemberThumbPictureSize.HasValue ? familyMemberThumbPictureSize.Value : mediaSettings.ProductThumbPictureSize;
                //prepare picture model
                var defaultFamilyMemberPictureCacheKey = string.Format(HGModelCacheEventConsumer.FAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY, model.Id, pictureSize, true, workContext.WorkingLanguage.Id, webHelper.IsCurrentConnectionSecured(), 0);
                model.DefaultPictureModel = cacheManager.Get(defaultFamilyMemberPictureCacheKey, () =>
                {
                    var picture = familyMemberService.GetPicturesByFamilyMemberId(model.Id, 1).FirstOrDefault();

                    var pictureModel = new PictureModel
                    {
                        ImageUrl = pictureService.GetPictureUrl(picture, pictureSize),
                        FullSizeImageUrl = pictureService.GetPictureUrl(picture)
                    };

                    //"title" attribute
                    pictureModel.Title = (picture != null && !string.IsNullOrEmpty(picture.TitleAttribute)) ?
                        picture.TitleAttribute :
                        string.Format(localizationService.GetResource("Media.FamilyMember.ImageLinkTitleFormat"), model.GivenName);
                    //"alt" attribute
                    pictureModel.AlternateText = (picture != null && !string.IsNullOrEmpty(picture.AltAttribute)) ?
                        picture.AltAttribute :
                        string.Format(localizationService.GetResource("Media.FamilyMember.ImageAlternateTextFormat"), model.Description);

                    return pictureModel;

                });

                #endregion
            }

            return model;
        }
    }
  
}