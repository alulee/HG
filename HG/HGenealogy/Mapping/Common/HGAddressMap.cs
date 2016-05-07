using Nop.Data.Mapping;
using HGenealogy.Domain.Common;

namespace HGenealogy.Mapping.Common
{
    public partial class HGAddressMap : NopEntityTypeConfiguration<HGAddress>
    {
        public HGAddressMap()
        {
            this.ToTable("HGAddress");
            this.HasKey(pp => pp.Id);

        }
    }
}