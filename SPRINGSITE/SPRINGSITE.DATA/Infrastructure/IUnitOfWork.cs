using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPRINGSITE.DATA
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
