using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy.Mapping
{
    public partial class HGPedigreeInfoMap : NopEntityTypeConfiguration<HGPedigreeInfo>
    {
        public HGPedigreeInfoMap()
        {
            this.ToTable("HGPedigreeInfo");
            this.HasKey(c => c.Id);
        }
    }
}