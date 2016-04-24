using Nop.Data.Mapping;
using HGenealogy.Domain;

namespace HGenealogy.Mapping
{
    public partial class HGPedigreeEventMap : NopEntityTypeConfiguration<HGPedigreeEvent>
    {
        public HGPedigreeEventMap()
        {
            this.ToTable("HGPedigreeEvent");
            this.HasKey(c => c.Id);
        }
    }
}