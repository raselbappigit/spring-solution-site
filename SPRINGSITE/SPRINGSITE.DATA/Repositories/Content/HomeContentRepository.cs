using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class HomeContentRepository : RepositoryBase<HomeContent>, IHomeContentRepository
    {
        public HomeContentRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IHomeContentRepository : IRepository<HomeContent>
    {

    }
}
