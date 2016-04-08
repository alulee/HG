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
    /// HGTestService
    /// </summary>
    public partial class HGTestService : IHGTestService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : HGTest ID
        /// </remarks>
        private const string HGTEST_BY_ID_KEY = "HG.HGTest.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string HGTEST_PATTERN_KEY = "HG.HGTest.";

        #endregion

        #region Fields

        private readonly IRepository<HGTest> _hgPedigreeRepository;
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
        public HGTestService(ICacheManager cacheManager,
            IRepository<HGTest> hgPedigreeRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext)
        {
            this._cacheManager = cacheManager;
            this._hgPedigreeRepository = hgPedigreeRepository;       
            this._aclRepository = aclRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
              
        /// <summary>
        /// Gets a HGTest
        /// </summary>
        /// <param name="categoryId">HGTest identifier</param>
        /// <returns>HGTest</returns>
        public virtual HGTest GetHGTestById(int hgTestId)
        {
            if (hgTestId == 0)
                return null;

            string key = string.Format(HGTEST_BY_ID_KEY, hgTestId);
            return _cacheManager.Get(key, () => _hgPedigreeRepository.GetById(hgTestId));
        }
 
        #endregion
    }
}
