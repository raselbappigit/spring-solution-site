using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPRINGSITE.SERVICE;

namespace SPRINGSITE.WEB.Controllers
{
    [Authorize]
    //[System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] //for stopping browser previous and next option
    public class AdminController : Controller
    {
        #region Member Variable

        private readonly ISecurityService _iSecurityService;

        #endregion

        #region Constructor

        public AdminController(ISecurityService iSecurityService)
        {
            this._iSecurityService = iSecurityService;
        }

        #endregion

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

    }
}
