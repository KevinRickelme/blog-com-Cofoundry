using blog.Cofoundry.CustomEntities.Autor;
using blog.Cofoundry.CustomEntities.Categoria;
using blog.Models;
using blog.Models.Autor;
using blog.Models.Categoria;
using Cofoundry.Core;
using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Cofoundry.CustomEntities.BlogPost
{
    public class BlogPostDisplayModelMapper
        : ICustomEntityDisplayModelMapper<BlogPostDataModel, BlogPostDisplayModel>
    {
        private readonly IContentRepository _contentRepository;
        public BlogPostDisplayModelMapper(
            IContentRepository contentRepository
            )
        {
            _contentRepository = contentRepository;
        }

        public async Task<BlogPostDisplayModel> MapDisplayModelAsync(CustomEntityRenderDetails renderDetails, BlogPostDataModel dataModel, PublishStatusQuery publishStatusQuery)
        {

            var vm = new BlogPostDisplayModel()
            {
                MetaDescription = dataModel.Tag,
                PageTitle = renderDetails.Title,
                Titulo = renderDetails.Title,
                Descricao = dataModel.Descricao,
                Data = renderDetails.CreateDate,
                Caminho = renderDetails.PageUrls.FirstOrDefault(),
                Tag = dataModel.Tag,
                
            };

            vm.Categorias = await MapCategorias(dataModel, publishStatusQuery);
            vm.Autor = await MapAutores(dataModel, publishStatusQuery);

            return vm;
        }


        private async Task<ICollection<CategoriaSumario>> MapCategorias(
           BlogPostDataModel dataModel,
           PublishStatusQuery publishStatusQuery)
        {
            if (EnumerableHelper.IsNullOrEmpty(dataModel.CategoriaIds))
            {
                return Array.Empty<CategoriaSumario>();
            }

            var resultados = await _contentRepository
                .CustomEntities()
                .GetByIdRange(dataModel.CategoriaIds)
                .AsRenderSummaries(publishStatusQuery)
                .FilterAndOrderByKeys(dataModel.CategoriaIds)
                .MapItem(MapCategoria)
                .ExecuteAsync();

            return resultados;
        }

        /// We could use AutoMapper here, but to keep it simple let's just do manual mapping.
        
        private CategoriaSumario MapCategoria(CustomEntityRenderSummary renderSummary)
        {
            // A CustomEntityRenderSummary will always contain the data model for the custom entity 
            var model = renderSummary.Model as CategoriaDataModel;

            var categoria = new CategoriaSumario()
            {
                Id = renderSummary.CustomEntityId,
                Nome = renderSummary.Title,
                Descricao = model.Descricao
            };
            return categoria;
        }

        private async Task<AutorSumario> MapAutores(BlogPostDataModel dataModel, PublishStatusQuery publishStatusQuery)
        {
            if (dataModel.AutorId < 1) return null;

            var dbAutor = await _contentRepository
                .CustomEntities()
                .GetById(dataModel.AutorId)
                .AsRenderSummary(publishStatusQuery)
                .ExecuteAsync();

            var model = dbAutor?.Model as AutorDataModel;
            if (model == null) return null;

            var autor = new AutorSumario()
            {
                Nome = dbAutor.Title,
                Biografia = HtmlFormatter.ConvertToBasicHtml(model.Biografia)
            };

            if (model.ProfileImageAssetId < 1) return autor;

            autor.ImagemPerfil = await _contentRepository
                .ImageAssets()
                .GetById(model.ProfileImageAssetId.Value)
                .AsRenderDetails()
                .ExecuteAsync();
            return autor;
        } 

    }
}
