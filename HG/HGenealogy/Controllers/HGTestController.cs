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
//using HGenealogy.Models.Boards;
using HGenealogy.Services;
using HGenealogy.Models.Media;
using HGenealogy.Models.HGPedigree;

namespace HGenealogy.Controllers
{
    public partial class HGTestController : BasePublicController
    {
		#region Fields

        private readonly IHGTestService _hgTestService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICurrencyService _currencyService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAclService _aclService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ISearchTermService _searchTermService;
        private readonly MediaSettings _mediaSettings;
        private readonly ForumSettings _forumSettings;
        private readonly ICacheManager _cacheManager;
        
        #endregion

		#region Constructors

        public HGTestController(
            IHGTestService hgTestService,
            IWorkContext workContext, 
            IPictureService pictureService, 
            ILocalizationService localizationService,
            IWebHelper webHelper, 
            IGenericAttributeService genericAttributeService,
            IAclService aclService,
            IPermissionService permissionService, 
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ISearchTermService searchTermService,
            MediaSettings mediaSettings,
            ForumSettings  forumSettings,
            ICacheManager cacheManager)
        {
            this._hgTestService = hgTestService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._webHelper = webHelper;
            this._genericAttributeService = genericAttributeService;
            this._aclService = aclService;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
            this._eventPublisher = eventPublisher;
            this._searchTermService = searchTermService;
            this._mediaSettings = mediaSettings;
            this._forumSettings = forumSettings;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities
         
        #endregion

        #region Categories
        
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult HGTest(int hgTestId, HGPedigreePagingFilteringModel command)
        {
            var hgTest = _hgTestService.GetHGTestById(hgTestId);
            if (hgTest == null)
                return InvokeHttp404();

            //Check whether the current user has a "Manage catalog" permission
            //It allows him to preview a category before publishing
            //if (!hgPedigree.Published && !_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            //    return InvokeHttp404();

            //ACL (access control list)
            //if (!_aclService.Authorize(hgPedigree))
            //    return InvokeHttp404();

            var model = hgTest.ToModel();
  
            return View(model);
        }
  
        [NopHttpsRequirement(SslRequirement.No)]
        public string Index()
        {
            var hgTest = _hgTestService.GetHGTestById(2);
            if (hgTest == null)
                return "null";

            var model = hgTest.ToModel();

            return hgTest.ToString();
        }
        #endregion
    }
}
