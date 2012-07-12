using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class ContactContentRepository : RepositoryBase<ContactContent>, IContactContentRepository
    {
        public ContactContentRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IContactContentRepository : IRepository<ContactContent>
    {

    }
}
