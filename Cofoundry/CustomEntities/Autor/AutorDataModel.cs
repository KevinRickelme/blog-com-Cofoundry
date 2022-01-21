using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.CustomEntities.Autor
{
    public class AutorDataModel : ICustomEntityDataModel
    {
        [Image(MinWidth = 460, MinHeight = 460)]
        [Display(Name = "Imagem de Perfil", Description = "Imagem quadrada que aparecerá ao lado da biografia do autor.")]
        public int? ProfileImageAssetId { get; set; }

        [MaxLength(500)]
        [Display(Description = "Uma pequena biografia que aparecerá ao lado da imagem do autor.")]
        [MultiLineText]
        public string Biografia { get; set; }
    }
}
