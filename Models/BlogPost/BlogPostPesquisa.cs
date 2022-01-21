using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.BlogPost
{
    public class SearchBlogPostsQuery : SimplePageableQuery
    {
        public int CategoryId { get; set; }
    }
}
