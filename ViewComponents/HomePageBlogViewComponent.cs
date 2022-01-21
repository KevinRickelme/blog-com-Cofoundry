using blog.Cofoundry.CustomEntities.BlogPost;
using blog.Models;
using blog;
using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog
{
    public class HomePageBlogViewComponent : ViewComponent
    {
        private readonly IContentRepository _contentRepository;
        private readonly IVisualEditorStateService _visualEditorStateService;

        public HomePageBlogViewComponent(
        IContentRepository contentRepository,
        IVisualEditorStateService visualEditorStateService)
        {
            _contentRepository = contentRepository;
            _visualEditorStateService = visualEditorStateService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // We can use the current visual editor state (e.g. edit mode, live mode) to
            // determine whether to show unpublished blog posts in the list.
            var visualEditorState = await _visualEditorStateService.GetCurrentAsync();
            var ambientEntityPublishStatusQuery = visualEditorState.GetAmbientEntityPublishStatusQuery();

            var query = new SearchCustomEntityRenderSummariesQuery();
            query.CustomEntityDefinitionCode = BlogPostCustomEntityDefinition.DefinitionCode;
            query.PageSize = IntParser.ParseOrDefault(Request.Query[nameof(query.PageNumber)]);
            query.PublishStatus = ambientEntityPublishStatusQuery;

            var entities = await _contentRepository
                .CustomEntities()
                .Search()
                .AsRenderSummaries(query)
                .ExecuteAsync();

                var viewModel = await MapBlogPostsAsync(entities, ambientEntityPublishStatusQuery);

            return View(viewModel);
        }

        private async Task<PagedQueryResult<BlogPostSumario>> MapBlogPostsAsync(
            PagedQueryResult<CustomEntityRenderSummary> customEntityResult,
            PublishStatusQuery ambientEntityPublishStatusQuery
            )
        {
            var blogPosts = new List<BlogPostSumario>(customEntityResult.Items.Count());
            var imageAssetIds = customEntityResult
                .Items
                .Select(i => (BlogPostDataModel)i.Model)
                .Select(m => m.ThumbnailImageAssetId)
                .Distinct();

            var autorIds = customEntityResult
                .Items
                .Select(i => (BlogPostDataModel)i.Model)
                .Select(m => m.AutorId)
                .Distinct();

            var categoriaIds = customEntityResult
                .Items
                .Select(i => (BlogPostDataModel)i.Model)
                .Select(m => m.CategoriaIds.First())
                .Distinct();

            var imageLookup = await _contentRepository
                .ImageAssets()
                .GetByIdRange(imageAssetIds)
                .AsRenderDetails()
                .ExecuteAsync();

            var autorLookup = await _contentRepository
                .CustomEntities()
                .GetByIdRange(autorIds)
                .AsRenderSummaries(ambientEntityPublishStatusQuery)
                .ExecuteAsync();

            var categoriaLookup = await _contentRepository
                .CustomEntities()
                .GetByIdRange(categoriaIds)
                .AsRenderSummaries(ambientEntityPublishStatusQuery)
                .ExecuteAsync();

            foreach(var customEntity in customEntityResult.Items)
            {
                var model = (BlogPostDataModel)customEntity.Model;

                var blogPost = new BlogPostSumario();
                blogPost.Titulo = customEntity.Title;
                blogPost.Tag = model.Tag;
                blogPost.Descricao = model.Descricao;
                blogPost.ThumbnailImageAsset = imageLookup.GetOrDefault(model.ThumbnailImageAssetId);
                blogPost.Caminho = customEntity.PageUrls.FirstOrDefault();
                blogPost.DataPost = customEntity.PublishDate;

                var autor = autorLookup.GetOrDefault(model.AutorId);
                if (autor != null)
                {
                    blogPost.Autor = autor.Title;
                }
                blogPosts.Add(blogPost);
            }
            return customEntityResult.ChangeType(blogPosts);
        }
    }
}
