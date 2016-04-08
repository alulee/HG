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
using HGenealogy.Infrastructure.Cache;
using HGenealogy.Models.HGPedigree;
using Nop.Services.Localization;
using Nop.Services.Media;
using HGenealogy.Models.HGFamilyMember;
using HGenealogy.Domain;
using HGenealogy.Models.Media;
using HGenealogy.Services;
//using Nop.Web.Models.Catalog;
//using Nop.Web.Models.Media;

namespace HGenealogy.Extensions
{
    //here we have some methods shared between controllers
    
    public static class ControllerExtensions
    {

        public static IEnumerable<HGFamilyMemberOverviewModel> PrepareHGFamilyMemberOverviewModels(this Controller controller,
            IWorkContext workContext,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IWebHelper webHelper,
            ICacheManager cacheManager,            
            MediaSettings mediaSettings,
            IHGFamilyMemberService hgFamilyMemberService,
            IEnumerable<HGFamilyMember> hgFamilyMembers,
            bool preparePictureModel = true,
            int? hgFamilyMemberThumbPictureSize = null)
        {
            if (hgFamilyMembers == null)
                throw new ArgumentNullException("hgFamilyMembers");

            var models = new List<HGFamilyMemberOverviewModel>();
            foreach (var familyMember in hgFamilyMembers)
            {
                var model = new HGFamilyMemberOverviewModel
                {
                    Id = familyMember.Id,
                    FamilyName = familyMember.GetLocalized(x => x.FamilyName),
                    GivenName = familyMember.GetLocalized(x => x.GivenName),
                    Description = familyMember.GetLocalized(x => x.Description) 
                };
 
                //picture
                if (preparePictureModel)
                {
                    #region Prepare HGFamilyMember picture

                    //If a size has been set in the view, we use it in priority
                    int pictureSize = hgFamilyMemberThumbPictureSize.HasValue ? hgFamilyMemberThumbPictureSize.Value : mediaSettings.ProductThumbPictureSize;
                    //prepare picture model
                    var defaultHGFamilyMemberPictureCacheKey = string.Format(ModelCacheEventConsumer.HGFAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY, familyMember.Id, pictureSize, true, workContext.WorkingLanguage.Id, webHelper.IsCurrentConnectionSecured(), 0);
                    model.DefaultPictureModel = cacheManager.Get(defaultHGFamilyMemberPictureCacheKey, () =>
                    {
                        var picture = hgFamilyMemberService.GetPicturesByHGFamilyMemberId(familyMember.Id, 1).FirstOrDefault();
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
 
                models.Add(model);
            }
            return models;
        }
    }
  
}