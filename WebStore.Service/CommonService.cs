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
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
    }
    public class CommonService : ICommonService
    {
        private readonly IFooterRepository footerRepository;
        private readonly ISlideRepository slideRepository;
        private readonly IUnitOfWork unitOfWork;

        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this.footerRepository = footerRepository;
            this.slideRepository = slideRepository;
            this.unitOfWork = unitOfWork;
        }

        public Footer GetFooter()
        {
            return footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);            
        }

        public IEnumerable<Slide> GetSlides()
        {
            return slideRepository.GetMulti(x => x.Status);
        }
    }
}
