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
using HGenealogy.Models.HGFamilyMembers;
using HGenealogy.Domain.HGFamilyMembers;
using HGenealogy.Services.HGFamilyMembers;
using HGenealogy.Domain.Common; 

namespace HGenealogy.Controllers
{
    public partial class HGFamilyMembersController : BasePublicController
    {
		#region Fields

        private readonly IFamilyMemberService _familyMemberService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPermissionService _permissionService;
        private readonly IEventPublisher _eventPublisher;
        private readonly MediaSettings _mediaSettings;
        private readonly ICacheManager _cacheManager;
        private readonly HGenealoySettings _hgenealogySettings; 
        
        #endregion

		#region Constructors

        public HGFamilyMembersController(
            IFamilyMemberService familyMemberService,
            IWorkContext workContext, 
            IPictureService pictureService, 
            ILocalizationService localizationService,
            IWebHelper webHelper, 
            IGenericAttributeService genericAttributeService,
            IPermissionService permissionService, 
            IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            ICacheManager cacheManager,
            HGenealoySettings hgenealogySettings)
        {
            this._familyMemberService = familyMemberService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._webHelper = webHelper;
            this._genericAttributeService = genericAttributeService;
            this._permissionService = permissionService;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._cacheManager = cacheManager;
            this._hgenealogySettings = hgenealogySettings;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual IEnumerable<FamilyMemberOverviewModel> PrepareFamilyMemberOverviewModels(IEnumerable<FamilyMember> familyMembers,
            bool preparePictureModel = true,
            int? familyMemberThumbPictureSize = null)
        {
            return this.PrepareFamilyMemberOverviewModels(_workContext,
                _localizationService,
                _pictureService, 
                _webHelper, 
                _cacheManager,
                _mediaSettings,
                _familyMemberService,
                familyMembers,
                preparePictureModel,
                familyMemberThumbPictureSize
                );
        }

        [NonAction]
        protected virtual FamilyMemberOverviewModel PrepareFamilyMemberOverviewModel(FamilyMember familyMember,
            bool preparePictureModel = true,
            int? familyMemberThumbPictureSize = null,
            bool returnEmptyModel = false)
        {
            var familyMemberOverviewModel = this.PrepareFamilyMemberOverviewModel(
                _workContext,
                _localizationService,
                _pictureService,
                _webHelper,
                _cacheManager,
                _mediaSettings,
                _familyMemberService,
                familyMember,
                preparePictureModel,
                familyMemberThumbPictureSize,
                returnEmptyModel
                );

            return familyMemberOverviewModel;
        }

        [NonAction]
        protected virtual FamilyMemberDetailsModel PrepareFamilyMemberDetailsPageModel(FamilyMember familymember,
            bool isAssociatedFamilyMember = false)
        {

            if (familymember == null)
                throw new ArgumentNullException("familymember");

            #region Standard properties

            var model = new FamilyMemberDetailsModel
            {
                Id = familymember.Id,
                FamilyName = familymember.GetLocalized(x => x.FamilyName),
                GivenName = familymember.GetLocalized(x => x.GivenName),
                Description = familymember.GetLocalized(x => x.Description),
                DateOfBirth = familymember.BirthDay.ToShortDateString(),
                GenerationNo = familymember.GenerationNo > 0 ? familymember.GenerationNo.ToString() : "",
                Email = familymember.GetLocalized(x => x.Email),
                MobilePhone = familymember.GetLocalized(x => x.MobilePhone),
                Gender = familymember.GetLocalized(x => x.Gender),
                JobDescription = familymember.GetLocalized(x => x.JobDescription)
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

            if (!isAssociatedFamilyMember)
            {
                var breadcrumbCacheKey = string.Format(HGModelCacheEventConsumer.FAMILYMEMBER_BREADCRUMB_MODEL_KEY,
                    familymember.Id,
                    _workContext.WorkingLanguage.Id,
                    "", // string.Join(",", _familyMemberService.GetFamilyMemberRoleIds()),
                    ""); //_pedigreeContext.CurrentPedigree.Id);
                model.Breadcrumb = _cacheManager.Get(breadcrumbCacheKey, () =>
                {
                    var breadcrumbModel = new FamilyMemberDetailsModel.FamilyMemberBreadcrumbModel
                    {
                        Enabled = true, //_familymemberSettings.BreadcrumbEnabled,
                        FamilyMemberId = familymember.Id,
                        FamilyMemberName = familymember.GetLocalized(x => x.GivenName)
                    };

                    return breadcrumbModel;
                });
            }

            #endregion

            #region FatherMember
            
            var fathermember = _familyMemberService.GetFamilyMemberById(familymember.FatherMemberId);
            model.FatherMember = PrepareFamilyMemberOverviewModel(fathermember, familyMemberThumbPictureSize: _hgenealogySettings.ParentsMemberPictureThumbSizeOnDetailPage,  returnEmptyModel : true);

            #endregion

            #region MotherMember

            var mothermember = _familyMemberService.GetFamilyMemberById(familymember.MotherMemberId);
            model.MotherMember = PrepareFamilyMemberOverviewModel(mothermember, familyMemberThumbPictureSize: _hgenealogySettings.ParentsMemberPictureThumbSizeOnDetailPage, returnEmptyModel: true);

            #endregion

            #region Related familymember

            //var relatedmembers = _familyMemberService.GetRelatedFamilyMemberById(familymember.Id);
            //if (relatedmembers != null && relatedmembers.Count > 0)
            //    model.RelatedMembers = PrepareFamilyMemberOverviewModels(relatedmembers, familyMemberThumbPictureSize: _hgenealogySettings.ParentsMemberPictureThumbSizeOnDetailPage) as IList<FamilyMemberOverviewModel>;

            #endregion

            #region FamilyMember tags

            #endregion

            #region Templates

            #endregion

            #region Pictures
            model.DefaultPictureZoomEnabled = _mediaSettings.DefaultPictureZoomEnabled;
            //default picture
            var defaultPictureSize = this._hgenealogySettings.FamilyMemberDetailPictureThumbSize;
            //prepare picture models
            var familyMemberPicturesCacheKey = string.Format(HGModelCacheEventConsumer.FAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY, familymember.Id, defaultPictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), 0); ;
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
                        ImageUrl = _pictureService.GetPictureUrl(picture, _hgenealogySettings.RelatedMemberPictureThumbSizeOnDetailPage),
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
 
            return model;
        }


        [NonAction]
        protected virtual void PrepareFamilyMemberModel(FamilyMemberModel model, FamilyMember familymember, bool excludeProperties,
            string overrideCustomFamilyMemberAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (familymember != null)
            {
            }

        }

        #endregion

        #region HGFamilyMember Action

        #region 建立新家族成員

        //[NopHttpsRequirement(SslRequirement.Yes)]
        //available even when navigation is not allowed
        //[PublicPedigreeAllowNavigation(true)]
        public ActionResult Create()
        {
            //檢查是否允許建立新的家族成員 (族譜編修角色權限) 
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManagePedigree))
            //    return AccessDeniedView();

            var model = new FamilyMemberModel();
            PrepareFamilyMemberModel(model, null, false);
            

            return View(model);
        }

        
        #endregion
 
        [NopHttpsRequirement(SslRequirement.No)]
        public string Index()
        {
            var hgFamilyMembers = _familyMemberService.GetAllHGFamilyMember();
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
            var hgFamilyMembers = this._familyMemberService.GetAllHGFamilyMember();
            var model = new List<FamilyMemberOverviewModel>();
            model.AddRange(this.PrepareFamilyMemberOverviewModels(hgFamilyMembers));

            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult FamilyMemberDetails(int familymemberId, int updatecartitemid = 0)
        {
            var familymember = _familyMemberService.GetFamilyMemberById(familymemberId);
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

        [ChildActionOnly]
        public ActionResult RelatedFamilyMembers(int familymemberId, int? familymemberThumbPictureSize)
        {
            //load and cache report
            var familyMemberIds = _cacheManager.Get(string.Format(HGModelCacheEventConsumer.FAMILYMEMBER_RELATED_IDS_KEY, familymemberId, 0),
                () =>
                    this._familyMemberService.GetRelatedFamilyMemberById(familymemberId).Select(x => x.Id).ToArray()
                    );

            //load familyMembers
            var familyMembers = _familyMemberService.GetFamilyMembersByIds(familyMemberIds);

            //ACL and store mapping
            // products = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            // products = products.Where(p => p.IsAvailable()).ToList();

            if (familyMembers.Count == 0)
                return Content("");

            var model = this.PrepareFamilyMemberOverviewModels(familyMembers, true, familymemberThumbPictureSize).ToList();
            return PartialView(model);
        }
       
        #endregion
    }
}
