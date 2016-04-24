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
    public partial class HGPedigreeInfoService : IHGPedigreeInfoService
    {
        private readonly IRepository<HGPedigreeInfo> _hGPedigreeInfoRepository;

        public HGPedigreeInfoService(
            IRepository<HGPedigreeInfo> hGPedigreeInfoRepository)
        {
            this._hGPedigreeInfoRepository = hGPedigreeInfoRepository;
        }


        public virtual List<HGPedigreeInfo> GetAllHGPedigreeInfo(
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _hGPedigreeInfoRepository.Table;
            //if (!showHidden)
            //    query = query.Where(c => c.Published);
            //if (!String.IsNullOrWhiteSpace(hgPedigreeName))
            //    query = query.Where(c => c.Name.Contains(hgPedigreeName));
            //query = query.Where(c => !c.Deleted);
            //query = query.OrderBy(c => c.DisplayOrder);

            //if (!showHidden)
            //{
            //    //only distinct categories (group by ID)
            //    query = from c in query
            //            group c by c.Id
            //                into cGroup
            //                orderby cGroup.Key
            //                select cGroup.FirstOrDefault();
            //    query = query.OrderBy(c => c.DisplayOrder);
            //}

            var unsortedHGPedigrees = query.ToList();

            //sort hgPedigrees
            //var sortedHGPedigrees = unsortedHGPedigrees.SortHGPedigreesForTree();
            return query.ToList();
            //paging
            //return  query.ToList();
        }

        public HGPedigreeInfo GetHGPedigreeInfoById(int id)
        {
            var hGPedigreeMeta = _hGPedigreeInfoRepository.Table
                                                        .Where(x => x.Id == id)
                                                        .FirstOrDefault();

            return hGPedigreeMeta;
        }

        public virtual void InsertHGPedigreeInfo(HGPedigreeInfo hGPedigreeInfo)
        {
            if (hGPedigreeInfo == null)
                throw new ArgumentNullException("hGPedigreeInfo");

            _hGPedigreeInfoRepository.Insert(hGPedigreeInfo);

        }



        public virtual void UpdateHGPedigreeInfo(HGPedigreeInfo hGPedigreeInfo)
        {
            if (hGPedigreeInfo == null)
                throw new ArgumentNullException("hGPedigreeInfo");

            _hGPedigreeInfoRepository.Update(hGPedigreeInfo);
            
        }

        public virtual void DeleteHGPedigreeInfo(HGPedigreeInfo hGPedigreeInfo)
        {
            if (hGPedigreeInfo == null)
                throw new ArgumentNullException("hGPedigreeInfo");

            _hGPedigreeInfoRepository.Delete(hGPedigreeInfo);

        }
    }
}