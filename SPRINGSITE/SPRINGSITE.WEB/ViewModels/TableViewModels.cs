using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPRINGSITE.WEB
{
    #region User Model Table Model
    public class UserTableModels
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string CreateDate { get; set; }
    }
    #endregion

    #region Role Model Table Model
    public class RoleTableModels
    {
        public string RoleName { get; set; }
    }
    #endregion
}