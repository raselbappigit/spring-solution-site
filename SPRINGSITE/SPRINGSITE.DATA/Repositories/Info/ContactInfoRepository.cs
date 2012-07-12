using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.DATA
{
    public class ContactInfoRepository : RepositoryBase<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
        }

    }
    public interface IContactInfoRepository : IRepository<ContactInfo>
    {

    }
}
