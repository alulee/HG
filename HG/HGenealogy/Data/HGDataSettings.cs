﻿using Nop.Core.Data;
using System;
using System.Collections.Generic;

namespace HGenealogy.Data
{
    /// <summary>
    /// HGenealogy Data settings (connection string information)
    /// </summary>
    public partial class HGDataSettings : DataSettings
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public HGDataSettings(): base()
        {           
        }

        ///// <summary>
        ///// Data provider
        ///// </summary>
        //public string DataProvider { get; set; }

        ///// <summary>
        ///// Connection string
        ///// </summary>
        //public string DataConnectionString { get; set; }

        ///// <summary>
        ///// Raw settings file
        ///// </summary>
        //public IDictionary<string, string> RawDataSettings { get; private set; }

        ///// <summary>
        ///// A value indicating whether entered information is valid
        ///// </summary>
        ///// <returns></returns>
        //public bool IsValid()
        //{
        //    return !String.IsNullOrEmpty(this.DataProvider) && !String.IsNullOrEmpty(this.DataConnectionString);
        //}
    }
}
