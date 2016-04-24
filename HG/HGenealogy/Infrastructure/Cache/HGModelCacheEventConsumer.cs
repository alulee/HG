using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Configuration;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Topics;
using Nop.Core.Domain.Vendors;
using Nop.Core.Events;
using Nop.Core.Infrastructure;
using Nop.Services.Events;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class HGModelCacheEventConsumer:

        //hgFamilyMembers
        IConsumer<EntityInserted<FamilyMember>>,
        IConsumer<EntityUpdated<FamilyMember>>,
        IConsumer<EntityDeleted<FamilyMember>>,

        //HGFamilyMember picture mapping
        IConsumer<EntityInserted<FamilyMemberPicture>>,
        IConsumer<EntityUpdated<FamilyMemberPicture>>,
        IConsumer<EntityDeleted<FamilyMemberPicture>>                 
    {
        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic system name
        /// {1} : language id
        /// {2} : store id
        /// </remarks>
        public const string TOPIC_MODEL_BY_SYSTEMNAME_KEY = "HG.topic.details.bysystemname-{0}-{1}-{2}";
        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic id
        /// {1} : language id
        /// {2} : store id
        /// </remarks>
        public const string TOPIC_MODEL_BY_ID_KEY = "HG.topic.details.byid-{0}-{1}-{2}";
        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic system name
        /// {1} : language id
        /// {2} : store id
        /// </remarks>
        public const string TOPIC_SENAME_BY_SYSTEMNAME = "HG.topic.sename.bysystemname-{0}-{1}-{2}";
        /// <summary>
        /// Key for TopMenuModel caching
        /// </summary>
        /// <remarks>
        /// {0} : language id
        /// {1} : current store ID
        /// </remarks>
        public const string TOPIC_TOP_MENU_MODEL_KEY = "HG.topic.topmenu-{0}-{1}";
        /// <summary>
        /// Key for TopMenuModel caching
        /// </summary>
        /// <remarks>
        /// {0} : language id
        /// {1} : current store ID
        /// </remarks>
        public const string TOPIC_FOOTER_MODEL_KEY = "HG.topic.footer-{0}-{1}";
        public const string TOPIC_PATTERN_KEY = "HG.topic";

        /// <summary>
        /// Key for CategoryTemplate caching
        /// </summary>
        /// <remarks>
        /// {0} : category template id
        /// </remarks>
        public const string CATEGORY_TEMPLATE_MODEL_KEY = "HG.categorytemplate-{0}";
        public const string CATEGORY_TEMPLATE_PATTERN_KEY = "HG.categorytemplate";

        /// <summary>
        /// Key for sitemap on the sitemap page
        /// </summary>
        /// <remarks>
        /// {0} : language id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string SITEMAP_PAGE_MODEL_KEY = "HG.sitemap.page-{0}-{1}-{2}";

        /// <summary>
        /// Key for sitemap on the sitemap SEO page
        /// </summary>
        /// <remarks>
        /// {0} : language id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string SITEMAP_SEO_MODEL_KEY = "HG.sitemap.seo-{0}-{1}-{2}";
        public const string SITEMAP_PATTERN_KEY = "HG.sitemap";

        /// <summary>
        /// Key for default hgfamilymember picture caching (all pictures)
        /// </summary>
        /// <remarks>
        /// {0} : hgfamilymember id
        /// {1} : picture size
        /// {2} : isAssociatedMember?
        /// {3} : language ID ("alt" and "title" can depend on localized product name)
        /// {4} : is connection SSL secured?
        /// {5} : current HGPedigree ID
        /// </remarks>
        public const string FAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY = "HG.familymember.picture-{0}-{1}-{2}-{3}-{4}-{5}";
        public const string FAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY = "HG.familymember.picture";

        /// <summary>
        /// Key for FamilyMemberBreadcrumbModel caching
        /// </summary>
        /// <remarks>
        /// {0} : familymember id
        /// {1} : language id
        /// {2} : comma separated list of familymember roles
        /// {3} : current pedigree id
        /// </remarks>
        public const string FAMILYMEMBER_BREADCRUMB_MODEL_KEY = "HG.familymember.breadcrumb-{0}-{1}-{2}-{3}";
        public const string FAMILYMEMBER_BREADCRUMB_PATTERN_KEY = "HG.familymember.breadcrumb";


        /// <summary>
        /// Key for FamilyMemberBreadcrumbModel caching
        /// </summary>
        /// <remarks>
        /// {0} : familymember id
        /// {1} : current pedigree id
        /// </remarks>
        public const string FAMILYMEMBER_RELATED_IDS_KEY = "HG.familymember.related-{0}-{1}";
        public const string FAMILYMEMBER_RELATED_PATTERN_KEY = "HG.familymember.related";

        private readonly ICacheManager _cacheManager;
        
        public HGModelCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        //familymember
        public void HandleEvent(EntityInserted<FamilyMember> eventMessage)
        {
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<FamilyMember> eventMessage)
        {            
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<FamilyMember> eventMessage)
        {
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }

        //familymemberpicture
        public void HandleEvent(EntityInserted<FamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(FAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<FamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(FAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<FamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(FAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
 
    }
}
