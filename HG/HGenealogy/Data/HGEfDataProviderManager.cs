using System;
using Nop.Core;
using Nop.Core.Data;
using Nop.Data;

namespace HGenealogy.Data
{
    public partial class HGEfDataProviderManager : BaseDataProviderManager
    {
        public HGEfDataProviderManager(HGDataSettings settings)
            : base(settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            this.HGSettings = settings;
        }

        /// <summary>
        /// Gets or sets settings
        /// </summary>
        protected HGDataSettings HGSettings { get; private set; }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = HGSettings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new NopException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new NopException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }          
    }
}
