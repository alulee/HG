using HGenealogy.Domain.Common;

namespace HGenealogy.Services.Common
{
    /// <summary>
    /// HGAddress service interface
    /// </summary>
    public partial interface IHGAddressService
    {        
        /// <summary>
        /// Gets total number of HGAddresses by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Number of HGAddresses</returns>
        //int GetHGAddressTotalByCountryId(int countryId);

        /// <summary>
        /// Gets total number of HGAddresses by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">State/province identifier</param>
        /// <returns>Number of HGAddresses</returns>
        //int GetHGAddressTotalByStateProvinceId(int stateProvinceId);

        /// <summary>
        /// Deletes an HGAddress
        /// </summary>
        /// <param name="HGHGAddress">HGAddress</param>
        void DeleteAddress(HGAddress HGAddress);

        /// <summary>
        /// Gets an HGAddress by HGAddress identifier
        /// </summary>
        /// <param name="HGAddressId">HGAddress identifier</param>
        /// <returns>HGAddress</returns>
        HGAddress GetAddressById(int HGAddressId);

        /// <summary>
        /// Inserts an HGAddress
        /// </summary>
        /// <param name="HGAddress">HGAddress</param>
        void InsertAddress(HGAddress HGAddress);

        /// <summary>
        /// Updates the HGAddress
        /// </summary>
        /// <param name="HGAddress">HGAddress</param>
        void UpdateAddress(HGAddress HGAddress);

        /// <summary>
        /// Gets a value indicating whether HGAddress is valid (can be saved)
        /// </summary>
        /// <param name="HGAddress">HGAddress to validate</param>
        /// <returns>Result</returns>
        bool IsAddressValid(HGAddress HGAddress);

        string GetFullAddressString(int HGAddressId);
    }
}