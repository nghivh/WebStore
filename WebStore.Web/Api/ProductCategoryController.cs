using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebStore.Model.Models;
using WebStore.Service;
using WebStore.Web.Infrastructure.Core;
using WebStore.Web.Infrastructure.Extensions;
using WebStore.Web.Models;

namespace WebStore.Web.Api
{
    [RoutePrefix("api/productcategories")]
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this.productCategoryService = productCategoryService;
        }

        [HttpGet]
        [Route("getallparents")]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<ProductCategory> parents = productCategoryService.GetAll();
                var parentsVM = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(parents);

                response = request.CreateResponse(HttpStatusCode.OK, parentsVM);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ProductCategory> productCategories = null;
                int totalCount = 0;

                var model = productCategoryService.GetAll(filter).ToList();
                totalCount = model.Count();

                productCategories = model.OrderBy(p => p.ID).Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();

                IEnumerable<ProductCategoryViewModel> productCategoriesVM = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategories);

                PaginationSet<ProductCategoryViewModel> pagedSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((decimal)totalCount/currentPageSize),
                    Items = productCategoriesVM
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpGet]
        [Route("getbyid/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductCategory productCategory = productCategoryService.GetById(id);
                if(productCategory == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Product Category.");
                }else
                {
                    ProductCategoryViewModel productCategoryVM = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                }

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
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
                    ProductCategory newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVM);

                    productCategoryService.Add(newProductCategory);
                    productCategoryService.Save();

                    //Update ViewModel
                    productCategoryVM = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, productCategoryVM);
                }                

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
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
                    ProductCategory productCategoryDb = productCategoryService.GetById(productCategoryVM.ID);
                    if (productCategoryDb == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ProductCategory.");
                    }
                    else
                    {
                        productCategoryDb.UpdateProductCategory(productCategoryVM);

                        productCategoryService.Update(productCategoryDb);
                        productCategoryService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                    }
                }

                return response;

            });
        }

        [HttpDelete]
        [Route("delete")] 
        [AllowAnonymous]      
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    ProductCategory delProductCategory = productCategoryService.GetById(id);

                    if(delProductCategory == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ProductCategory.");
                    }
                    else
                    {
                        productCategoryService.Delete(id);
                        productCategoryService.Save();
                        ProductCategoryViewModel productCategoryVM = Mapper.Map<ProductCategory, ProductCategoryViewModel>(delProductCategory);
                        response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                    }
                }

                return response;
            });
        }

        [HttpDelete]
        [Route("deleteMulti")]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedId)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedId);
                   
                    foreach(int id in listId)
                    {
                        productCategoryService.Delete(id);                        
                    }
                    productCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listId);
                }

                return response;
            });
        }
    }
}
