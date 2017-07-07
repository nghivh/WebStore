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
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory postCategory);
        void Update(PostCategory postCategory);
        PostCategory Delete(int id);
        IEnumerable<PostCategory> GetAll();
        IEnumerable<PostCategory> GetAll(string keyword);
        IEnumerable<PostCategory> GetAllByParentId(int parentId);
        PostCategory GetById(int id);
        void Save();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository postCategoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.postCategoryRepository = postCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory postCategory)
        {
            return postCategoryRepository.Add(postCategory);
        }

        public PostCategory Delete(int id)
        {
            return postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return postCategoryRepository.GetAll().Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return postCategoryRepository.GetAll();
            }
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return postCategoryRepository.GetAll().Where(x => x.ParentID == parentId && x.Status);
        }

        public PostCategory GetById(int id)
        {
            return postCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            postCategoryRepository.Update(postCategory);
        }
    }
}
