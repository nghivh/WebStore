using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Model.Models;
using WebStore.Service;
using WebStore.Web.Models;

namespace WebStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductService productService;
        private readonly ICommonService commonService;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
            this.commonService = commonService;
        }

        // GET: Home
        public ActionResult Index()
        {
            //Get slides
            var slideModel = commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;

            //Get Lastest products
            var lastestProductsModel = productService.GetLastestProducts(6);
            var lastestProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductsModel);
            homeViewModel.LastestProducts = lastestProductsViewModel;

            //Get Top products
            var topProductsModel = productService.GetTopProducts(6);
            var topProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topProductsModel);
            homeViewModel.TopProducts = topProductsViewModel;            

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            var productCategoriesModel = productCategoryService.GetAll();
            var productCategoriesViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategoriesModel);

            return PartialView(productCategoriesViewModel);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);

            return PartialView(footerViewModel);
        }


    }
}