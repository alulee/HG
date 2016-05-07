using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Media;
using HGenealogy.Domain.HGFamilyMembers;

namespace HGenealogy.Services.HGFamilyMembers
{
    /// <summary>
    /// FamilyMember Service interface
    /// </summary>
    public partial interface IFamilyMemberService
    {
 
        /// <summary>
        /// Get All  FamilyMember
        /// </summary>       
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>HGPedigrees</returns>
        IPagedList<FamilyMember> GetAllHGFamilyMember(
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get Pictures By HGFamilyMemberID
        /// </summary>
        /// <param name="hgFamilyMemberId"></param>
        /// <param name="recordsToReturn"></param>
        /// <returns></returns>
        IList<Picture> GetPicturesByFamilyMemberId(int hgFamilyMemberId, int recordsToReturn = 0);

        /// <summary>
        /// �H Id ���o�a�ڦ��� FamilyMember
        /// </summary>
        /// <param name="familyMemberId"></param>
        /// <returns></returns>
        FamilyMember GetFamilyMemberById(int familyMemberId);

        /// <summary>
        /// �H Id ���o��������������
        /// </summary>
        /// <param name="familyMemberId"></param>
        /// <returns></returns>
        IList<FamilyMember> GetRelatedFamilyMemberById(int familyMemberId);

        /// <summary>
        /// �H Id[] ���o�a�ڦ���
        /// </summary>
        /// <param name="familyMemberIds"></param>
        /// <returns>FamilyMembers</returns>
        IList<FamilyMember> GetFamilyMembersByIds(int[] familyMemberIds);

        /// <summary>
        /// �H familyMemberId ���o�������Ӹ�T
        /// </summary>
        /// <param name="familyMemberId"></param>
        /// <param name="infoType"></param>
        /// <returns></returns>
        IList<FamilyMemberInfo> GetFamilyMemberInfoByMemberId(int familyMemberId, string infoType);
    
    }
}
