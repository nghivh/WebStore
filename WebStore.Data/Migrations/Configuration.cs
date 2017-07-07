namespace WebStore.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Data.WebStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebStore.Data.WebStoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            InitialPostCategory(context);
            InitialProductCategory(context);
            //InitialUserLogin(context);
        }

        private void InitialPostCategory(WebStoreDbContext context)
        {
            if (context.PostCategories.Count() == 0)
            {
                List<PostCategory> list = new List<PostCategory>()
                {
                    new PostCategory() { Name="Điện lạnh", Alias="dien-lanh", Status=true },
                    new PostCategory() { Name="Viễn thông", Alias="vien-thong", Status=true },
                    new PostCategory() { Name="Đồ gia dụng", Alias="do-gia-dung", Status=true },
                };
                context.PostCategories.AddRange(list);
                context.SaveChanges();
            }
        }

        private void InitialProductCategory(WebStoreDbContext context)
        {
            if(context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> list = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Điện lạnh", Alias="dien-lanh", Status=true },
                    new ProductCategory() { Name="Viễn thông", Alias="vien-thong", Status=true },
                    new ProductCategory() { Name="Đồ gia dụng", Alias="do-gia-dung", Status=true },
                };
                context.ProductCategories.AddRange(list);
                context.SaveChanges();
            }
        }

        private void InitialUserLogin(WebStoreDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new WebStoreDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new WebStoreDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "nghivh",
                Email = "nghivh.qt@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nghi Vo"

            };

            manager.Create(user, "@123456@");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nghivh.qt@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}
