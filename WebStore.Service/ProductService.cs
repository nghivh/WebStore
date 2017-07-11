using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common;
using WebStore.Data.Infrastructure;
using WebStore.Data.Repositories;
using WebStore.Model.Models;

namespace WebStore.Service
{
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetAllPaging(string keyword, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Product> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<Product> GetLastestProducts(int top);
        IEnumerable<Product> GetTopProducts(int top);

        Product GetById(int id);
        IEnumerable<Product> GetRelatedProducts(int id, int top);
        void Save();
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ITagRepository tagRepository;
        private readonly IProductTagRepository productTagRepository;
        private readonly IUnitOfWork unitOfWork;


        public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.tagRepository = tagRepository;
            this.productTagRepository = productTagRepository;
            this.unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var _product =  productRepository.Add(product);
            unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for(int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if(tagRepository.Count(x=>x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i].ToString();
                        tag.Type = CommonConstants.ProductTag;
                        tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = _product.ID;
                    productTag.TagID = tagId;
                    productTagRepository.Add(productTag);
                }
            }

            return _product;
        }

        public Product Delete(int id)
        {
            return productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return productRepository.GetMulti(x => x.Status && (x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower())), new string[] { "ProductCategory" } );
            }
            else
            {
                return productRepository.GetAll(new string[] { "ProductCategory" });
            }
        }

        public IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow)
        {
            //return productRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, pageIndex, pageSize, new string[] { "ProductCategory" });
            var query = productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            totalRow = query.Count();

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);      
        } 

        public IEnumerable<Product> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            return productRepository.GetAllByTag(tag, pageIndex, pageSize, out totalRow);
        }

        public IEnumerable<Product> GetAllPaging(string keyword, int pageIndex, int pageSize, out int totalRow)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return productRepository.GetMultiPaging(x => x.Status, out totalRow, pageIndex, pageSize, new string[] { "ProductCategory" });
            }
            else
            {
                return productRepository.GetMultiPaging(x => x.Status && (x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower())), out totalRow, pageIndex, pageSize, new string[] { "ProductCategory" });
            }            
        }

        public Product GetById(int id)
        {
            return productRepository.GetSingleById(id);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
            unitOfWork.Commit();
            unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i].ToString();
                        tag.Type = CommonConstants.ProductTag;
                        tagRepository.Add(tag);
                    }

                    productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    productTagRepository.Add(productTag);
                }
            }
        }

        public IEnumerable<Product> GetLastestProducts(int top)
        {
            return productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetTopProducts(int top)
        {
            return productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = productRepository.GetSingleById(id);
            return productRepository.GetMulti(x => x.Status && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }
    }
}
