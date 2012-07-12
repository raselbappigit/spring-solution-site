using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class QuickMailInfoRepository : RepositoryBase<QuickMailInfo>, IQuickMailInfoRepository
    {
        public QuickMailInfoRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IQuickMailInfoRepository : IRepository<QuickMailInfo>
    {

    }
}
