using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Media;
using HGenealogy.Domain;

namespace HGenealogy.Services
{
    /// <summary>
    /// FamilyMember Service interface
    /// </summary>
    public partial interface IHGFamilyMemberService
    {
 
        /// <summary>
        /// Get All  FamilyMember
        /// </summary>       
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>HGPedigrees</returns>
        IPagedList<HGFamilyMember> GetAllHGFamilyMember(
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get Pictures By HGFamilyMemberID
        /// </summary>
        /// <param name="hgFamilyMemberId"></param>
        /// <param name="recordsToReturn"></param>
        /// <returns></returns>
        IList<Picture> GetPicturesByHGFamilyMemberId(int hgFamilyMemberId, int recordsToReturn = 0);

        /// <summary>
        /// 以 Id 取得家族成員 HGFamilyMember
        /// </summary>
        /// <param name="familyMemberId"></param>
        /// <returns></returns>
        HGFamilyMember GetHGFamilyMemberById(int familyMemberId);

    }
}
