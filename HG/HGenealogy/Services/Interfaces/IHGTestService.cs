using System.Collections.Generic;
using Nop.Core;
using HGenealogy.Domain;

namespace HGenealogy.Services
{
    /// <summary>
    /// HGTest service interface
    /// </summary>
    public partial interface IHGTestService
    {
        /// <summary>
        /// Gets a HGTest
        /// </summary>
        /// <param name="categoryId">HGTest identifier</param>
        /// <returns>HGTest</returns>
        HGTest GetHGTestById(int hgTestId);
    }
}
