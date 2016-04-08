using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Security;
using HGenealogy.Domain;
using Nop.Core.Domain.Media;

namespace HGenealogy.Services
{
    /// <summary>
    /// HGFamilyMember Service
    /// </summary>
    public partial class HGFamilyMemberService : IHGFamilyMemberService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : FamilyMember ID
        /// </remarks>
        private const string HGFAMILYMEMBER_BY_ID_KEY = "HG.FamilyMember.id-{0}";       
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string HGFAMILYMEMBER_PATTERN_KEY = "HG.FamilyMember.";

        #endregion

        #region Fields

        private readonly IRepository<HGFamilyMember> _hgFamilyMemberRepository;
        private readonly IRepository<HGFamilyMemberPicture> _hgFamilyMemberPictureRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="hgPedigreeRepository"></param>
        /// <param name="aclRepository"></param>
        /// <param name="workContext"></param>
        public HGFamilyMemberService(ICacheManager cacheManager,
            IRepository<HGFamilyMember> hgFamilyMemberRepository,
            IRepository<HGFamilyMemberPicture> hgFamilyMemberPictureRepository,
            IRepository<Picture> pictureRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext)
        {
            this._cacheManager = cacheManager;
            this._hgFamilyMemberRepository = hgFamilyMemberRepository;
            this._hgFamilyMemberPictureRepository = hgFamilyMemberPictureRepository;
            this._pictureRepository = pictureRepository;
            this._aclRepository = aclRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        public virtual IList<HGFamilyMember> GetAllHGFamilyMember()
        {
            var query = _hgFamilyMemberRepository.Table;

            query = query.Where(c => !c.IsDeleted);

            var unsortedHGFamilyMember = query.ToList();
 
            return unsortedHGFamilyMember;
        }
        
        /// <summary>
        /// Gets all hgPedigrees
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<HGFamilyMember> GetAllHGFamilyMember(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _hgFamilyMemberRepository.Table;
 
            query = query.Where(c => !c.IsDeleted);

            var unsortedHGFamilyMember = query.ToList();

            //sort hgPedigrees
            //var sortedHGPedigrees = unsortedHGPedigrees.SortHGPedigreesForTree();

            //paging
            return new PagedList<HGFamilyMember>(unsortedHGFamilyMember, pageIndex, pageSize);
        }


        /// <summary>
        /// Gets pictures by hgFamilyMemberId identifier
        /// </summary>
        /// <param name="HGFamilyMemberId">HGFamilyMember identifier</param>
        /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
        /// <returns>Pictures</returns>
        public virtual IList<Picture> GetPicturesByHGFamilyMemberId(int hgFamilyMemberId, int recordsToReturn = 0)
        {
            if (hgFamilyMemberId == 0)
                return new List<Picture>();

            var mappingquery =
                (from hp in _hgFamilyMemberPictureRepository.Table
                 orderby hp.DisplayOrder
                 select hp).FirstOrDefault();

            if (mappingquery != null)
            {
                var query = from p in _pictureRepository.Table
                            where p.Id == mappingquery.PictureId
                            select p;

                if (recordsToReturn > 0)
                    query = query.Take(recordsToReturn);

                var pics = query.ToList();
                return pics;
            }

            return new List<Picture>();
        }


        /// <summary>
        /// Gets HGFamilyMember
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual HGFamilyMember GetHGFamilyMemberById(int familyMemberId)
        {
            if (familyMemberId == 0)
                return null;

            string key = string.Format(HGFAMILYMEMBER_BY_ID_KEY, familyMemberId);
            return _cacheManager.Get(key, () => this._hgFamilyMemberRepository.GetById(familyMemberId));
        }
 
        #endregion
    }
}
