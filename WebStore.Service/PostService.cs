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
    public interface IPostService
    {
        Post Add(Post post);
        void Update(Post post);
        Post Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPaging(int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Post> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow);
        Post GetById(int id);
        void Save();
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }

        public Post Add(Post post)
        {
            return postRepository.Add(post);
        }

        public Post Delete(int id)
        {
            return postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow)
        {
            return postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, pageIndex, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            return postRepository.GetAllByTag(tag, pageIndex, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int pageIndex, int pageSize, out int totalRow)
        {
            return postRepository.GetMultiPaging(x=>x.Status, out totalRow, pageIndex, pageSize);
        }

        public Post GetById(int id)
        {
            return postRepository.GetSingleById(id);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            postRepository.Update(post);
        }
    }
}
