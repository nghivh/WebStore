using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Model.Models;
using WebStore.Web.Models;

namespace WebStore.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                cfg.CreateMap<Post, PostViewModel>();
            });
        }
    }
}