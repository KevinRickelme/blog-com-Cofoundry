using blog.Models.Autor;
using blog.Models.Categoria;
using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.CustomEntities.BlogPost
{
    public class BlogPostDisplayModel : ICustomEntityPageDisplayModel<BlogPostDataModel>
    {
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Titulo { get; set; }
        public string Tag { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public ImageAssetRenderDetails ImagemId { get; set; }
        public string Caminho { get; set; }

        public AutorSumario Autor { get; set; }

        public ICollection<CategoriaSumario> Categorias { get; set; }
    }
}
