using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class ClientInfoRepository : RepositoryBase<ClientInfo>, IClientInfoRepository
    {
        public ClientInfoRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IClientInfoRepository : IRepository<ClientInfo>
    {

    }

}
