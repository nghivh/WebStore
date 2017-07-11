using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebStore.Common;
using WebStore.Model.Models;
using WebStore.Service;
using WebStore.Web.Infrastructure.Core;
using WebStore.Web.Models;

namespace WebStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductService productService;

        public ProductController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
        }

        // GET: Product
        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetValueByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetValueByKey("MaxPage"));
            int totalRow = 0;

            var productCategoryModel = productCategoryService.GetById(id);
            var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategoryModel);
            ViewBag.Category = productCategoryViewModel;

            var productsModel = productService.GetAllByCategoryPaging(id, page, pageSize, out totalRow);
            var productsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productsModel);
            int totalPages = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productsViewModel,
                Page = page,
                TotalPages = totalPages,
                TotalCount = totalRow,
                MaxPage = maxPage
            };

            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            //Chi tiết sản phẩm
            var productModel = productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            //Sản phẩm liên quan
            var relatedProductsModel = productService.GetRelatedProducts(id, 6);
            var relatedProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProductsModel);
            ViewBag.RelatedProducts = relatedProductsViewModel;

            //MoreImages
            var moreImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = moreImages;

            return View(productViewModel);
        }
    }
}