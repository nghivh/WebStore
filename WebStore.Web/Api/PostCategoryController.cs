using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebStore.Model.Models;
using WebStore.Service;
using WebStore.Web.Infrastructure.Core;
using WebStore.Web.Infrastructure.Extensions;
using WebStore.Web.Models;

namespace WebStore.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this.postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var postCategory = postCategoryService.GetAll();
                var postCategoryVm = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(postCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, postCategoryVm);

                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryVm);

                    var category = postCategoryService.Add(postCategory);
                    postCategoryService.Save();

                    postCategoryVm = Mapper.Map<PostCategoryViewModel>(category);

                    response = request.CreateResponse(HttpStatusCode.Created, postCategoryVm);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = postCategoryService.GetById(postCategoryVm.ID);
                    if(postCategoryDb == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Post Category");
                    }
                    else
                    {
                        postCategoryDb.UpdatePostCategory(postCategoryVm);

                        postCategoryService.Update(postCategoryDb);
                        postCategoryService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int postCategoryId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    postCategoryService.Delete(postCategoryId);
                    postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });            
        }
    }
}
