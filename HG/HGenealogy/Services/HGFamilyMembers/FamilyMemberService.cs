using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Security;
using HGenealogy.Domain;
using Nop.Core.Domain.Media;
using HGenealogy.Domain.HGFamilyMembers;
using HGenealogy.Domain.Common;

namespace HGenealogy.Services.HGFamilyMembers
{
    /// <summary>
    /// HGFamilyMember Service
    /// </summary>
    public partial class FamilyMemberService : IFamilyMemberService
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

        
        /// {1} : RelatedFamilyMember  
        /// </remarks>
        private const string HGRELATEDFAMILYMEMBER_BY_ID_KEY = "HG.RelatedFamilyMember.id-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string HGRELATEDFAMILYMEMBER_PATTERN_KEY = "HG.RelatedFamilyMember.";

        #endregion

        #region Fields

        private readonly IRepository<FamilyMember> _familyMemberRepository;
        private readonly IRepository<FamilyMemberPicture> _familyMemberPictureRepository;
        private readonly IRepository<FamilyMemberRelation> _familyMemberRelationRepository;
        private readonly IRepository<FamilyMemberInfo> _familyMemberInfoRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly HGenealoySettings _hgenealogySettings;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="hgPedigreeRepository"></param>
        /// <param name="aclRepository"></param>
        /// <param name="workContext"></param>
        public FamilyMemberService(ICacheManager cacheManager,
            IRepository<FamilyMember> hgFamilyMemberRepository,
            IRepository<FamilyMemberPicture> familyMemberPictureRepository,
            IRepository<FamilyMemberRelation> familyMemberRelationRepository,
            IRepository<FamilyMemberInfo> familyMemberInfoRepository,
            IRepository<Picture> pictureRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext,
            HGenealoySettings hgenealogySettings)
        {
            this._cacheManager = cacheManager;
            this._familyMemberRepository = hgFamilyMemberRepository;
            this._familyMemberPictureRepository = familyMemberPictureRepository;
            this._familyMemberRelationRepository = familyMemberRelationRepository;
            this._familyMemberInfoRepository = familyMemberInfoRepository;
            this._pictureRepository = pictureRepository;
            this._aclRepository = aclRepository;
            this._workContext = workContext;
            this._hgenealogySettings = hgenealogySettings;
        }

        #endregion

        #region Methods

        public virtual IList<FamilyMember> GetAllHGFamilyMember()
        {
            var query = _familyMemberRepository.Table;

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
        public virtual IPagedList<FamilyMember> GetAllHGFamilyMember(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _familyMemberRepository.Table;
 
            query = query.Where(c => !c.IsDeleted);

            var unsortedHGFamilyMember = query.ToList();

            //sort hgPedigrees
            //var sortedHGPedigrees = unsortedHGPedigrees.SortHGPedigreesForTree();

            //paging
            return new PagedList<FamilyMember>(unsortedHGFamilyMember, pageIndex, pageSize);
        }


        /// <summary>
        /// Gets pictures by hgFamilyMemberId identifier
        /// </summary>
        /// <param name="HGFamilyMemberId">HGFamilyMember identifier</param>
        /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
        /// <returns>Pictures</returns>
        public virtual IList<Picture> GetPicturesByFamilyMemberId(int familyMemberId, int recordsToReturn = 0)
        {
            if(familyMemberId == 0)
               return new List<Picture>();
          
            var mappingquery =
                (from hp in _familyMemberPictureRepository.Table
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
        public virtual FamilyMember GetFamilyMemberById(int familyMemberId)
        {
            if (familyMemberId == 0)
                return null;

            string key = string.Format(HGFAMILYMEMBER_BY_ID_KEY, familyMemberId);
            return _cacheManager.Get(key, () => this._familyMemberRepository.GetById(familyMemberId));
        }


        /// <summary>
        /// Get familyMembers by identifiers
        /// </summary>
        /// <param name="familyMemberIds">FamilyMember identifiers</param>
        /// <returns>FamilyMembers</returns>
        public virtual IList<FamilyMember> GetFamilyMembersByIds(int[] familyMemberIds)
        {
            if (familyMemberIds == null || familyMemberIds.Length == 0)
                return new List<FamilyMember>();

            var query = from p in _familyMemberRepository.Table
                        where familyMemberIds.Contains(p.Id)
                        select p;
            var familyMembers = query.ToList();
            //sort by passed identifiers
            var sortedfamilyMembers = new List<FamilyMember>();
            foreach (int id in familyMemberIds)
            {
                var familyMember = familyMembers.Find(x => x.Id == id);
                if (familyMember != null)
                    sortedfamilyMembers.Add(familyMember);
            }
            return sortedfamilyMembers;
        }

        /// <summary>
        /// Gets RelatedFamilyMember By FamilyMemberId
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual IList<FamilyMember> GetRelatedFamilyMemberById(int familyMemberId)
        {
            if (familyMemberId == 0)
                return null;

            var query = from p in this._familyMemberRelationRepository.Table
                        from fm in this._familyMemberRepository.Table
                        where p.FamilyMemberId == familyMemberId && fm.Id == p.RelatedFamilyMemberId
                        select fm;

            var relatedmembers = query.ToList();
            return relatedmembers;

        }


        /// <summary>
        /// Gets FamilyMemberInfo By FamilyMemberId
        /// </summary>
        /// <param name="FamilyMemberId">familyMemberId</param>
        /// <returns>List<FamilyMemberInfo></returns>
        public virtual IList<FamilyMemberInfo> GetFamilyMemberInfoByMemberId(int familyMemberId, string infoType)
        {
            if (familyMemberId == 0)
                return null;

            var query = from p in this._familyMemberInfoRepository.Table
                        where p.FamilyMemberId == familyMemberId && p.InfoType == infoType
                        select p;

            var infos = query.ToList();
            return infos;

        }
        #endregion
    }
}
