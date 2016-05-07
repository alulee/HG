using System;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Common;
using HGenealogy.Services.Common;
using HGenealogy.Domain.Common;



namespace HGenealogy.Services.Common
{
    /// <summary>
    /// HGAddressService service
    /// </summary>
    public partial class HGAddressService : IHGAddressService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : HGAddress ID
        /// </remarks>
        private const string HGADDRESSES_BY_ID_KEY = "HG.address.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string HGADDRESSES_PATTERN_KEY = "HG.address.";

        #endregion

        #region Fields

        private readonly IRepository<HGAddress> _hgAddressRepository; 
        private readonly IEventPublisher _eventPublisher;
        private readonly HGAddressSettings _hgAddressSettings;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="hgAddressRepository"></param>
        /// <param name="addressAttributeService"></param>
        /// <param name="eventPublisher"></param>
        /// <param name="hgAddressSettings"></param>
        public HGAddressService(ICacheManager cacheManager,
            IRepository<HGAddress> hgAddressRepository,
            IAddressAttributeService addressAttributeService,
            IEventPublisher eventPublisher,
            HGAddressSettings hgAddressSettings)
        {
            this._cacheManager = cacheManager;
            this._hgAddressRepository = hgAddressRepository; 
            this._eventPublisher = eventPublisher;
            this._hgAddressSettings = hgAddressSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void DeleteAddress(HGAddress hgaddress)
        {
            if (hgaddress == null)
                throw new ArgumentNullException("hgaddress");

            _hgAddressRepository.Delete(hgaddress);

            //cache
            _cacheManager.RemoveByPattern(HGADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(hgaddress);
        }
 
        /// <summary>
        /// Gets an HGAddress by address identifier
        /// </summary>
        /// <param name="addressId">Address identifier</param>
        /// <returns>HGAddress</returns>
        public virtual HGAddress GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

            string key = string.Format(HGADDRESSES_BY_ID_KEY, addressId);
            return _cacheManager.Get(key, () => _hgAddressRepository.GetById(addressId));
        }

        /// <summary>
        /// Inserts an HGAddress
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void InsertAddress(HGAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            
            address.CreatedOnUtc = DateTime.UtcNow;

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _hgAddressRepository.Insert(address);

            //cache
            _cacheManager.RemoveByPattern(HGADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(address);
        }

        /// <summary>
        /// Updates the HGAddress
        /// </summary>
        /// <param name="address">HGAddress</param>
        public virtual void UpdateAddress(HGAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("hgAddress");

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _hgAddressRepository.Update(address);

            //cache
            _cacheManager.RemoveByPattern(HGADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(address);
        }
        
        /// <summary>
        /// Gets a value indicating whether address is valid (can be saved)
        /// </summary>
        /// <param name="address">Address to validate</param>
        /// <returns>Result</returns>
        public virtual bool IsAddressValid(HGAddress address)
        {
            if (address == null)
                throw new ArgumentNullException("HGAddress");
 
            return true;
        }

        public virtual string GetFullAddressString(int addressId)
        {
            if (addressId <=0)
                throw new ArgumentNullException("addressId");

            var address = this.GetAddressById(addressId);
            if (address == null || !IsAddressValid(address))
                throw new ArgumentNullException("address");

            string fullAddressString = string.Empty;

            fullAddressString = string.Format("{0} {1} {2} {3} {4} ",
                address.CountryId == null ? "" : address.CountryId.ToString(),
                address.City,
                address.Address1,
                address.Address2,
                address.ZipPostalCode
                );

            return fullAddressString;
        }

        #endregion
    }
}