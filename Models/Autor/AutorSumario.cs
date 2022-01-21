using Cofoundry.Domain;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Autor
{
    public class AutorSumario
    {
        public string Nome { get; set; }
        public IHtmlContent Biografia { get; set; }
        public ImageAssetRenderDetails ImagemPerfil { get; set; }
    }
}
