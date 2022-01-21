using Cofoundry.Domain;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.PageBlockTypes.ContentSection
{
    public class ContentSectionDisplayModel : IPageBlockTypeDisplayModel
    {

        public IHtmlContent HtmlText { get; set; }
    }
}
