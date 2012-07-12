using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class BannerContentRepository : RepositoryBase<BannerContent>, IBannerContentRepository
    {
        public BannerContentRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IBannerContentRepository : IRepository<BannerContent>
    {

    }
}
