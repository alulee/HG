using System.Collections.Generic;
using Nop.Core;
using HGenealogy.Domain;

namespace HGenealogy.Services
{
    public partial interface IHGPedigreeInfoService
    {
        List<HGPedigreeInfo> GetAllHGPedigreeInfo(
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        HGPedigreeInfo GetHGPedigreeInfoById(int id);

        void InsertHGPedigreeInfo(HGPedigreeInfo hgPedigreeMeta);

        void UpdateHGPedigreeInfo(HGPedigreeInfo hgPedigreeMeta);

        void DeleteHGPedigreeInfo(HGPedigreeInfo hgPedigreeMeta);
    }
}
