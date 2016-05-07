using Nop.Core.Configuration;

namespace HGenealogy.Domain.Common
{
    public class HGAddressSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is enabled
        /// </summary>
        public bool StreetAddressEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is required
        /// </summary>
        public bool StreetAddressRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is enabled
        /// </summary>
        public bool StreetAddress2Enabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is required
        /// </summary>
        public bool StreetAddress2Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is enabled
        /// </summary>
        public bool ZipPostalCodeEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is required
        /// </summary>
        public bool ZipPostalCodeRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'City' is enabled
        /// </summary>
        public bool CityEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'City' is required
        /// </summary>
        public bool CityRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Country' is enabled
        /// </summary>
        public bool CountryEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'State / province' is enabled
        /// </summary>
        public bool StateProvinceEnabled { get; set; }
    }
}