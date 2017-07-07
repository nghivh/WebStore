using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Infrastructure;
using WebStore.Model.Models;

namespace WebStore.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {

    }
    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
