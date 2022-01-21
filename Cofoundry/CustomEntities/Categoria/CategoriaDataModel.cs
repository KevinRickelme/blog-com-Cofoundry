using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.CustomEntities.Categoria
{
    public class CategoriaDataModel : ICustomEntityDataModel
    {
        [MaxLength(30)]
        [Display(Description = "Uma descrição que aparecerá no blog.")]
        [MultiLineText]
        public string Descricao { get; set; }
    }
}
