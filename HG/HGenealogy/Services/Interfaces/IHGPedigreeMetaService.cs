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
        //void DeleteHGPedigree(HGPedigree hgPedigree);

        List<HGPedigreeMeta> GetAllHGPedigreeMeta(
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        HGPedigreeMeta GetHGPedigreeMetaById(int id);

        void InsertHGPedigreeMeta(HGPedigreeMeta hgPedigreeMeta);

        void UpdateHGPedigreeMeta(HGPedigreeMeta hgPedigreeMeta);

        void DeleteHGPedigreeMeta(HGPedigreeMeta hgPedigreeMeta);

    }
}
