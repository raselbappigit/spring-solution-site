using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class AboutContentRepository : RepositoryBase<AboutContent>, IAboutContentRepository
    {
        public AboutContentRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IAboutContentRepository : IRepository<AboutContent>
    {

    }
}
