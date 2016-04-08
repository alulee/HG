using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;

namespace HGenealogy.Models.HGPedigree
{
    public partial class HGPedigreePagingFilteringModel : BasePageableModel
    {
        #region Constructors

        public HGPedigreePagingFilteringModel()
        {
            this.AvailableSortOptions = new List<SelectListItem>();
            this.AvailableViewModes = new List<SelectListItem>();
            this.PageSizeOptions = new List<SelectListItem>();
 
        }

        #endregion

        #region Properties



        public bool AllowPersonSorting { get; set; }
        public IList<SelectListItem> AvailableSortOptions { get; set; }

        public bool AllowPersonViewModeChanging { get; set; }
        public IList<SelectListItem> AvailableViewModes { get; set; }

        public bool AllowCustomersToSelectPageSize { get; set; }
        public IList<SelectListItem> PageSizeOptions { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        public int OrderBy { get; set; }

        /// <summary>
        /// Product sorting
        /// </summary>
        public string ViewMode { get; set; }
        

        #endregion

        #region Nested classes
     

        public partial class SpecificationFilterItem : BaseNopModel
        {
            public string SpecificationAttributeName { get; set; }
            public string SpecificationAttributeOptionName { get; set; }
            public string FilterUrl { get; set; }
        }

        #endregion
    }
}