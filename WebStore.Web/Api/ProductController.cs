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
    [RoutePrefix("api/products")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this.productService = productService;
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
                List<Product> products = null;
                int totalCount = 0;

                var model = productService.GetAll(filter).ToList();
                totalCount = model.Count();

                products = model.OrderByDescending(x=>x.CreatedDate).Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();

                IEnumerable<ProductViewModel> productsVM = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

                PaginationSet<ProductViewModel> pagedSet = new PaginationSet<ProductViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((decimal)totalCount / currentPageSize),
                    Items = productsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Product product = productService.GetById(id);

                if(product == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Product.");
                }
                else
                {
                    ProductViewModel productVM = Mapper.Map<Product, ProductViewModel>(product);
                    response = request.CreateResponse(HttpStatusCode.OK, productVM);
                }

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductViewModel productVM)
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
                    Product newProduct = new Product();
                    newProduct.UpdateProduct(productVM);

                    productService.Add(newProduct);
                    productService.Save();

                    productVM = Mapper.Map<Product, ProductViewModel>(newProduct);

                    response = request.CreateResponse(HttpStatusCode.Created, productVM);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
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
                    Product editProduct = productService.GetById(productVM.ID);

                    if(editProduct == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Product.");
                    }
                    else
                    {
                        editProduct.UpdateProduct(productVM);

                        productService.Update(editProduct);
                        productService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK, productVM);
                    }
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
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
                    Product delProduct = productService.GetById(id);

                    if (delProduct == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Product.");
                    }
                    else
                    {
                        productService.Delete(id);
                        productService.Save();
                        ProductViewModel productVM = Mapper.Map<Product, ProductViewModel>(delProduct);

                        response = request.CreateResponse(HttpStatusCode.OK, productVM);
                    }
                }

                return response;
            });
        }
        
        [HttpDelete]
        [Route("deleteMulti")]
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
                        productService.Delete(id);
                    }
                    productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listId);
                }

                return response;
            });
        }
    }
}
