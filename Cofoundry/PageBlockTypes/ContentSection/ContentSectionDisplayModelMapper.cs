using Cofoundry.Domain;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.PageBlockTypes.ContentSection
{
    public class ContentSectionDisplayModelMapper : IPageBlockTypeDisplayModelMapper<ContentSectionDataModel>
    {
        public Task MapAsync(
            PageBlockTypeDisplayModelMapperContext<ContentSectionDataModel> context,
            PageBlockTypeDisplayModelMapperResult<ContentSectionDataModel> result
            )
        {
            foreach (var input in context.Items)
            {
                var output = new ContentSectionDisplayModel();
                output.HtmlText = new HtmlString(input.DataModel.HtmlText);

                result.Add(input, output);
            }

            return Task.CompletedTask;
        }
    }
}
