using System;
using Nop.Core.Configuration;
using Nop.Core;

namespace HGenealogy.Domain.Common
{
    public partial class HGAddress : BaseEntity, ICloneable
    {        
        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int? StateProvinceId { get; set; }
        
        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string ZipPostalCode { get; set; }
         
        /// <summary>
        /// Gets or sets the ContactName
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the Contact email
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the ContactPhone
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the ContactNumber
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes (see "AddressAttribute" entity for more info)
        /// </summary>
        public string CustomAttributes { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
 
        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
 
        public object Clone()
        {
            var addr = new HGAddress
            {               
                CountryId = this.CountryId,
                StateProvinceId = this.StateProvinceId,
                City = this.City,
                Address1 = this.Address1,
                Address2 = this.Address2,
                ZipPostalCode = this.ZipPostalCode,
                ContactName = this.ContactName,               
                ContactEmail = this.ContactEmail,
                ContactPhone = this.ContactPhone,
                ContactNumber = this.ContactNumber,
                Longitude = this.Longitude,
                Latitude = this.Latitude,
                CustomAttributes = this.CustomAttributes,
                IsDeleted = this.IsDeleted,
                CreatedOnUtc = this.CreatedOnUtc,
            };
            return addr;
        }
    }
}
