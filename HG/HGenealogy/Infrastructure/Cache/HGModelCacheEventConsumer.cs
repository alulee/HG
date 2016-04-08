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
using HGenealogy.Domain;

namespace HGenealogy.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class ModelCacheEventConsumer:

        //hgFamilyMembers
        IConsumer<EntityInserted<HGFamilyMember>>,
        IConsumer<EntityUpdated<HGFamilyMember>>,
        IConsumer<EntityDeleted<HGFamilyMember>>,

        //HGFamilyMember picture mapping
        IConsumer<EntityInserted<HGFamilyMemberPicture>>,
        IConsumer<EntityUpdated<HGFamilyMemberPicture>>,
        IConsumer<EntityDeleted<HGFamilyMemberPicture>>                 
    {
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
        public const string HGFAMILYMEMBER_DEFAULTPICTURE_MODEL_KEY = "Nop.pres.hgfamilymember.picture-{0}-{1}-{2}-{3}-{4}-{5}";
        public const string HGFAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY = "Nop.pres.hgfamilymember.picture";
       
        //hgfamilymember
        public void HandleEvent(EntityInserted<HGFamilyMember> eventMessage)
        {
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<HGFamilyMember> eventMessage)
        {            
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<HGFamilyMember> eventMessage)
        {
            _cacheManager.RemoveByPattern(SITEMAP_PATTERN_KEY);
        }

        //hgfamilymemberpicture
        public void HandleEvent(EntityInserted<HGFamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(HGFAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<HGFamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(HGFAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<HGFamilyMemberPicture> eventMessage)
        {
            _cacheManager.RemoveByPattern(HGFAMILYMEMBER_DEFAULTPICTURE_PATTERN_KEY);
        }
 
    }
}
