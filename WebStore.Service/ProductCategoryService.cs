using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Infrastructure;
using WebStore.Data.Repositories;
using WebStore.Model.Models;

namespace WebStore.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        ProductCategory Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);
        IEnumerable<ProductCategory> GetAllByParentId(int parentId);
        ProductCategory GetById(int id);
        void Save();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return productCategoryRepository.GetMulti(x => x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower()));
            }
            else
            {
                return productCategoryRepository.GetAll();
            }
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return productCategoryRepository.GetAll().Where(x => x.ParentID == parentId);
        }

        public ProductCategory GetById(int id)
        {
            return productCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            productCategoryRepository.Update(productCategory);
        }
    }
}
