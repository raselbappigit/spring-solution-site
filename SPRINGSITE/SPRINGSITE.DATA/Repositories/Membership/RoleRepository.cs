using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
    public interface IRoleRepository : IRepository<Role>
    {

    }
}
