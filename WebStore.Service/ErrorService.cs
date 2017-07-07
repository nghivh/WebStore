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
    public interface IErrorService
    {
        Error Add(Error error);
        void Save();
    }

    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository errorRepository;
        private readonly IUnitOfWork unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this.errorRepository = errorRepository;
            this.unitOfWork = unitOfWork;
        }

        public Error Add(Error error)
        {
            return errorRepository.Add(error);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
