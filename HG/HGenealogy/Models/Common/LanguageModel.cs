﻿using Nop.Web.Framework.Mvc;

namespace HGenealogy.Models.Common
{
    public partial class LanguageModel : BaseNopEntityModel
    {
        public string Name { get; set; }

        public string FlagImageFileName { get; set; }

    }
}