using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class PortfolioContentRepository : RepositoryBase<PortfolioContent>, IPortfolioContentRepository
    {
        public PortfolioContentRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IPortfolioContentRepository : IRepository<PortfolioContent>
    {

    }
}
