using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class BlogPostSumario
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public string Autor { get; set; }
        public string Categoria { get; set; }
        public ImageAssetRenderDetails ThumbnailImageAsset { get; set; }

        public string Caminho { get; set; }
        public DateTime? DataPost { get; set; }

    }
}
