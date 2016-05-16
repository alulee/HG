using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using HGenealogy.Models.Media;
using System;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework;
using FluentValidation.Attributes;
using Nop.Web.Framework.Localization;

namespace HGenealogy.Models.HGPedigree
{
    public partial class HGPedigreeMetaModel : BaseNopEntityModel
    {
        public string Title { get; set; }
        public string Editor { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [UIHint("Date")]
        public DateTime PublishDate { get; set; }
        public int Volumes { get; set; }

        public int Pages { get; set; }

        public string FamilyName { get; set; }
        public string OriginalAncestor { get; set; }
        public string DateMoveToTaiwan { get; set; }
        public string AncestorToTaiwan { get; set; }
        public string OriginalAddress { get; set; }
        public int TotalGenerations { get; set; }
        public int GenerationToTaiwan { get; set; }
        public string LivingAreaInTaiwan { get; set; }
        public string OriginalCollector { get; set; }
        public string ContentNotes { get; set; }
        public string TangName { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedWho { get; set; }
        public string UpdatedWho { get; set; }
 
    }
}