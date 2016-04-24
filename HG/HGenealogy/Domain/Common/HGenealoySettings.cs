using Nop.Core.Configuration;

namespace HGenealogy.Domain.Common
{
    public class HGenealoySettings : ISettings
    {
        /// <summary>
        /// EmptyMemberPictureUrl
        /// </summary>
        public string EmptyMemberPictureUrl { get; set; }

        public int FamilyMemberDetailPictureThumbSize { get; set; }
        public int ParentsMemberPictureThumbSizeOnDetailPage { get; set; }
        public int RelatedMemberPictureThumbSizeOnDetailPage { get; set; }
    }
}