using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace blog
{
    /// <summary>
    /// An example page block type. 
    /// See https://github.com/cofoundry-cms/cofoundry/wiki/Page-Block-Types
    /// for more information
    /// </summary>
    public class ContentSectionDataModel : IPageBlockTypeDataModel, IPageBlockTypeDisplayModel
    {

        [Required]
        [Display(Name = "Text", Description = "Rich text displayed at full width")]
        [Html(HtmlToolbarPreset.AdvancedFormatting, HtmlToolbarPreset.Headings, HtmlToolbarPreset.Media)]
        public string HtmlText { get; set; }
    }
}