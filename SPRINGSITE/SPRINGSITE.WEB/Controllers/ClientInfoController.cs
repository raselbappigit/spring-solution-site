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
    public class ClientInfoController : Controller
    {
        #region Member Variable

        private readonly IClientInfoService _iClientInfoService;

        #endregion

        #region Constructor

        public ClientInfoController(IClientInfoService iClientInfoService)
        {
            this._iClientInfoService = iClientInfoService;
        }

        #endregion

        //
        // GET: /ClientInfo/

        public ActionResult Index()
        {
            return View();
        }

    }
}
