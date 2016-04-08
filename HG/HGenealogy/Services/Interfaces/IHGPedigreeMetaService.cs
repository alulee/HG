using System.Collections.Generic;
using Nop.Core;
using HGenealogy.Domain;

namespace HGenealogy.Services
{
    /// <summary>
    /// HGPedigree service interface
    /// </summary>
    public partial interface IHGPedigreeMetaService
    {
        /// <summary>
        /// Delete HGPedigree
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteHGPedigree(HGPedigreeMeta hgPedigree);

        /// <summary>
        /// Gets all HGPedigrees
        /// </summary>
        /// <param name="categoryName">HGPedigree name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>HGPedigrees</returns>
        IPagedList<HGPedigreeMeta> GetAllHGPedigrees(string hgPedigreeName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets all HGPedigrees displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>HGPedigrees</returns>
        //IList<HGPedigree> GetAllHGPedigreesDisplayedOnHomePage(bool showHidden = false);
                
        /// <summary>
        /// Gets a HGPedigree
        /// </summary>
        /// <param name="categoryId">HGPedigree identifier</param>
        /// <returns>HGPedigree</returns>
        HGPedigreeMeta GetHGPedigreeById(int hgPedigreeId);

        /// <summary>
        /// Inserts HGPedigree
        /// </summary>
        /// <param name="hgPedigree">HGPedigree</param>
        void InsertHGPedigree(HGPedigreeMeta hgPedigree);

        /// <summary>
        /// Updates the HGPedigree
        /// </summary>
        /// <param name="hgPedigree">HGPedigree</param>
        void UpdateHGPedigree(HGPedigreeMeta hgPedigree);

    }
}
