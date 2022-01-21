using blog.Cofoundry.CustomEntities.Autor;
using blog.Cofoundry.CustomEntities.Categoria;
using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.CustomEntities.BlogPost
{
    public class BlogPostDataModel : ICustomEntityDataModel
    {

        [MaxLength(25)]
        [Required]
        [Display(Description = "Tag que aparecerá no card da página inicial do blog.")]
        public string Tag { get; set; }

        [MaxLength(300)]
        [Required]
        [Display(Description = "Pequena descrição que aparecerá no card da tela inicial")]
        public string Descricao { get; set; }

        [Image(MinWidth = 316, MinHeight = 295)]
        [Display(Name = "Imagem de thumb", Description = "Imagem que será incluída no card da tela inicial.")]
        public int ThumbnailImageAssetId { get; set; }

        [Required]
        [Display(Name = "Autor", Description = "O autor do post.")]
        [CustomEntity(AutorCustomEntityDefinition.DefinitionCode)]
        public int AutorId { get; set; }

        [Display(Name = "Categorias", Description = "Puxe e arraste para configurar a ordem das categorias.")]
        [CustomEntityCollection(CategoriaCustomEntityDefinition.DefinitionCode, IsOrderable = true)]
        public ICollection<int> CategoriaIds { get; set; }
    }
}
