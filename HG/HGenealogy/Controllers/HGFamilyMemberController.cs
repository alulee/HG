using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Media;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using HGenealogy.Extensions;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Security;
using HGenealogy.Infrastructure.Cache; 
using HGenealogy.Services;
using HGenealogy.Models.Media;
using HGenealogy.Models.HGPedigree;
using HGenealogy.Models.HGFamilyMember;
using HGenealogy.Domain;

namespace HGenealogy.Controllers
{
    public partial class HGFamilyMemberController : BasePublicController
    {
		#region Fields

        private readonly IHGFamilyMemberService _hgFamilyMemberService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPermissionService _permissionService;
        private readonly IEventPublisher _eventPublisher;
        private readonly MediaSettings _mediaSettings;
        private readonly ICacheManager _cacheManager;
        
        #endregion

		#region Constructors

        public HGFamilyMemberController(
            IHGFamilyMemberService hgFamilyMemberService,
            IWorkContext workContext, 
            IPictureService pictureService, 
            ILocalizationService localizationService,
            IWebHelper webHelper, 
            IGenericAttributeService genericAttributeService,
            IPermissionService permissionService, 
            IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            ICacheManager cacheManager)
        {
            this._hgFamilyMemberService = hgFamilyMemberService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._webHelper = webHelper;
            this._genericAttributeService = genericAttributeService;
            this._permissionService = permissionService;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual IEnumerable<HGFamilyMemberOverviewModel> PrepareHGFamilyMemberOverviewModels(IEnumerable<HGFamilyMember> hgFamilyMembers,
            bool preparePictureModel = true,
            int? hgFamilyMemberThumbPictureSize = null)
        {
            return this.PrepareHGFamilyMemberOverviewModels(_workContext,
                _localizationService,
                _pictureService, 
                _webHelper, 
                _cacheManager,
                _mediaSettings,
                _hgFamilyMemberService,
                hgFamilyMembers,
                preparePictureModel,
                hgFamilyMemberThumbPictureSize
                );
        }


        [NonAction]
        protected virtual HGFamilyMemberDetailsModel PrepareFamilyMemberDetailsPageModel(HGFamilyMember familymember)
        {
            if (familymember == null)
                throw new ArgumentNullException("familymember");

            #region Standard properties

            var model = new HGFamilyMemberDetailsModel
            {
                Id = familymember.Id,
                FamilyName = familymember.GetLocalized(x => x.FamilyName),
                GivenName = familymember.GetLocalized(x => x.GivenName),
                Description = familymember.GetLocalized(x => x.Description) 
            };
             
 
            //email a friend
            //model.EmailAFriendEnabled = _catalogSettings.EmailAFriendEnabled;
            
            //compare familymember
            //model.CompareProductsEnabled = _catalogSettings.CompareProductsEnabled;

            #endregion

            #region Page sharing

            //if (_catalogSettings.ShowShareButton && !String.IsNullOrEmpty(_catalogSettings.PageShareCode))
            //{
            //    var shareCode = _catalogSettings.PageShareCode;
            //    if (_webHelper.IsCurrentConnectionSecured())
            //    {
            //        //need to change the addthis link to be https linked when the page is, so that the page doesnt ask about mixed mode when viewed in https...
            //        shareCode = shareCode.Replace("http://", "https://");
            //    }
            //    model.PageShareCode = shareCode;
            //}

            #endregion

            #region Breadcrumb

            #endregion

            #region FamilyMember tags

            #endregion

            #region Templates

            #endregion

            #region Pictures
            model.DefaultPictureZoomEnabled = _mediaSettings.DefaultPictureZoomEnabled;
            //default picture
            var defaultPictureSize = _mediaSettings.ProductDetailsPictureSize;
            //prepare picture models
            var familyMemberPicturesCacheKey = string.Format(ModelCacheEventConsumer.HGFAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY, familymember.Id, defaultPictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), 0); ;
            var cachedPictures = _cacheManager.Get(familyMemberPicturesCacheKey, () =>
            {
                var pictures = _pictureService.GetPicturesByProductId(familymember.Id);
                var defaultPicture = pictures.FirstOrDefault();
                var defaultPictureModel = new PictureModel
                {
                    ImageUrl = _pictureService.GetPictureUrl(defaultPicture, defaultPictureSize),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(defaultPicture, 0),
                    Title = string.Format(_localizationService.GetResource("Media.FamilyMember.ImageLinkTitleFormat.Details"), model.GivenName),
                    AlternateText = string.Format(_localizationService.GetResource("Media.FamilyMember.ImageAlternateTextFormat.Details"), model.Description),
                };
                //"title" attribute
                defaultPictureModel.Title = (defaultPicture != null && !string.IsNullOrEmpty(defaultPicture.TitleAttribute)) ?
                    defaultPicture.TitleAttribute :
                    string.Format(_localizationService.GetResource("Media.FamilyMember.ImageLinkTitleFormat.Details"), model.GivenName);
                //"alt" attribute
                defaultPictureModel.AlternateText = (defaultPicture != null && !string.IsNullOrEmpty(defaultPicture.AltAttribute)) ?
                    defaultPicture.AltAttribute :
                    string.Format(_localizationService.GetResource("Media.FamilyMember.ImageAlternateTextFormat.Details"), model.Description);

                //all pictures
                var pictureModels = new List<PictureModel>();
                foreach (var picture in pictures)
                {
                    var pictureModel = new PictureModel
                    {
                        ImageUrl = _pictureService.GetPictureUrl(picture, _mediaSettings.ProductThumbPictureSizeOnProductDetailsPage),
                        FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                        Title = string.Format(_localizationService.GetResource("Media.FamilyMember.ImageLinkTitleFormat.Details"), model.GivenName),
                        AlternateText = string.Format(_localizationService.GetResource("Media.FamilyMember.ImageAlternateTextFormat.Details"), model.Description),
                    };
                    //"title" attribute
                    pictureModel.Title = !string.IsNullOrEmpty(picture.TitleAttribute) ?
                        picture.TitleAttribute :
                        string.Format(_localizationService.GetResource("Media.FamilyMember.ImageLinkTitleFormat.Details"), model.GivenName);
                    //"alt" attribute
                    pictureModel.AlternateText = !string.IsNullOrEmpty(picture.AltAttribute) ?
                        picture.AltAttribute :
                        string.Format(_localizationService.GetResource("Media.FamilyMember.ImageAlternateTextFormat.Details"), model.Description);

                    pictureModels.Add(pictureModel);
                }

                return new { DefaultPictureModel = defaultPictureModel, PictureModels = pictureModels };
            });
            model.DefaultPictureModel = cachedPictures.DefaultPictureModel;
            model.PictureModels = cachedPictures.PictureModels;
            #endregion

            #region familymember attributes
 
            #endregion

            #region familymember review overview
 
            #endregion

            #region Pedigrees

            //pedigree

            #endregion

            #region Associated familymember
 
            #endregion

            return model;
        }

        #endregion
    
        #region HGFamilyMember

        [NopHttpsRequirement(SslRequirement.No)]
        public string Index()
        {
            var hgFamilyMembers = _hgFamilyMemberService.GetAllHGFamilyMember();
            if (hgFamilyMembers == null || hgFamilyMembers.Count == 0)
                return "無家族成員...";
            else
            {
                var model = hgFamilyMembers[0].ToDetailsModel();

                return model.GivenName + ", " + model.FamilyName; 
            }
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Overview()
        {
            var hgFamilyMembers = this._hgFamilyMemberService.GetAllHGFamilyMember();
            var model = new List<HGFamilyMemberOverviewModel>();
            model.AddRange(this.PrepareHGFamilyMemberOverviewModels(hgFamilyMembers));

            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult FamilyMemberDetails(int familymemberid, int updatecartitemid = 0)
        {
            var familymember = _hgFamilyMemberService.GetHGFamilyMemberById(familymemberid);
            if (familymember == null || familymember.IsDeleted)
                return InvokeHttp404();

            //ToDo: 查看使用者的角色是否可以查看 familymember
            //ACL (access control list)
            // if (!_aclService.Authorize(familymember))
            //   return InvokeHttp404();

            //published?
            //ToDo: 如果尚未出版，要檢查登入用戶是否有此 familymember 的編修權            
            if (!familymember.IsPublished //&& !_permissionService.Authorize(StandardPermissionProvider.ManageProducts)
                )
                return InvokeHttp404();

            //public?
            //ToDo: 如果非公開的成員資料，要檢查登入用戶是否有此 familymember 的編修權            
            if (!familymember.IsPublic //&& !_permissionService.Authorize(StandardPermissionProvider.ManageProducts)
                )
                return InvokeHttp404();

            //Pedigree Mapping
            //ToDo: 成員和族譜的對應關係 
            /*
            if (!_pedigreeMappingService.Authorize(familymember))
            {
                //visible individually?
                if (!familymember.VisibleIndividually)
                {
                }
            }
            */

            //prepare the model
            var model = this.PrepareFamilyMemberDetailsPageModel(familymember);

            //save as recently viewed
            //_recentlyViewedFamilyMembersService.AddFamilyMemberToRecentlyViewedList(product.Id);

            //activity log
            //_customerActivityService.InsertActivity("PublicStore.ViewProduct", _localizationService.GetResource("ActivityLog.PublicStore.ViewProduct"), product.Name);

            //return View(model.ProductTemplateViewPath, model);

            return View(model);
        }

        #endregion
    }
}
