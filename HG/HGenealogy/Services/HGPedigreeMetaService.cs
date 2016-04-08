using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Security;
using HGenealogy.Domain;

namespace HGenealogy.Services
{
    /// <summary>
    /// HGPedigree service
    /// </summary>
    public partial class HGPedigreeMetaService : IHGPedigreeMetaService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string HGPEDIGREE_BY_ID_KEY = "HG.HGPedigreeMeta.id-{0}";       
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string HGPEDIGREE_PATTERN_KEY = "HG.HGPedigreeMeta.";

        #endregion

        #region Fields

        private readonly IRepository<HGPedigreeMeta> _hgPedigreeMetaRepository;
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
        public HGPedigreeMetaService(ICacheManager cacheManager,
            IRepository<HGPedigreeMeta> hgPedigreeMetaRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext)
        {
            this._cacheManager = cacheManager;
            this._hgPedigreeMetaRepository = hgPedigreeMetaRepository;       
            this._aclRepository = aclRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete HGPedigree
        /// </summary>
        /// <param name="hgPedigree">HGPedigree</param>
        public virtual void DeleteHGPedigree(HGPedigreeMeta hgPedigreeMeta)
        {
            if (hgPedigreeMeta == null)
                throw new ArgumentNullException("hgPedigreeMeta");

            hgPedigreeMeta.IsDeleted = true;
            UpdateHGPedigree(hgPedigreeMeta);            
        }
        
        /// <summary>
        /// Gets all hgPedigrees
        /// </summary>
        /// <param name="hgPedigreeName">HGPedigree name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<HGPedigreeMeta> GetAllHGPedigrees(string hgPedigreeMetaTitle = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _hgPedigreeMetaRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.IsPublished);
            if (!String.IsNullOrWhiteSpace(hgPedigreeMetaTitle))
                query = query.Where(c => c.Title.Contains(hgPedigreeMetaTitle));
            query = query.Where(c => !c.IsDeleted); 
            
            if (!showHidden)
            {
                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault(); 
            }

            var unsortedHGPedigrees = query.ToList();

            //sort hgPedigrees
            //var sortedHGPedigrees = unsortedHGPedigrees.SortHGPedigreesForTree();

            //paging
            return new PagedList<HGPedigreeMeta>(unsortedHGPedigrees, pageIndex, pageSize);
        }
         
        /// <summary>
        /// Gets all HGPedigree displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IList<HGPedigreeMeta> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            var query = from c in _hgPedigreeMetaRepository.Table 
                        where c.IsPublished &&
                        !c.IsDeleted
                        select c;

            var categories = query.ToList();
            //if (!showHidden)
            //{
            //    categories = categories
            //        .Where(c => _aclService.Authorize(c) && _storeMappingService.Authorize(c))
            //        .ToList();
            //}

            return categories;
        }
                
        /// <summary>
        /// Gets a HGPedigree
        /// </summary>
        /// <param name="categoryId">HGPedigree identifier</param>
        /// <returns>HGPedigree</returns>
        public virtual HGPedigreeMeta GetHGPedigreeById(int hgPedigreeMetaId)
        {
            if (hgPedigreeMetaId == 0)
                return null;

            string key = string.Format(HGPEDIGREE_BY_ID_KEY, hgPedigreeMetaId);
            return _cacheManager.Get(key, () => _hgPedigreeMetaRepository.GetById(hgPedigreeMetaId));
        }

        /// <summary>
        /// Inserts HGPedigree
        /// </summary>
        /// <param name="category">HGPedigree</param>
        public virtual void InsertHGPedigree(HGPedigreeMeta hgPedigree)
        {
            if (hgPedigree == null)
                throw new ArgumentNullException("hgPedigreeMeta");

            _hgPedigreeMetaRepository.Insert(hgPedigree);

            //cache
            _cacheManager.RemoveByPattern(HGPEDIGREE_BY_ID_KEY); 

            //event notification
            //_eventPublisher.EntityInserted(hgPedigree);
        }

        /// <summary>
        /// Updates the HGPedigree
        /// </summary>
        /// <param name="category">HGPedigree</param>
        public virtual void UpdateHGPedigree(HGPedigreeMeta hgPedigree)
        {
            if (hgPedigree == null)
                throw new ArgumentNullException("hgPedigreeMeta");

            //validate category hierarchy
            //var parentCategory = GetCategoryById(category.ParentCategoryId);
            //while (parentCategory != null)
            //{
            //    if (category.Id == parentCategory.Id)
            //    {
            //        category.ParentCategoryId = 0;
            //        break;
            //    }
            //    parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            //}

            _hgPedigreeMetaRepository.Update(hgPedigree);

            //cache
            _cacheManager.RemoveByPattern(HGPEDIGREE_BY_ID_KEY); 

            //event notification
            //_eventPublisher.EntityUpdated(category);
        }
 
        #endregion
    }
}
