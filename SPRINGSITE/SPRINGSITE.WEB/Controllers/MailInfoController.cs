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
    public class MailInfoController : Controller
    {
        #region Member Variable

        private readonly IQuickMailInfoService _iQuickMailInfoService;

        #endregion

        #region Constructor

        public MailInfoController(IQuickMailInfoService iQuickMailInfoService)
        {
            this._iQuickMailInfoService = iQuickMailInfoService;
        }

        #endregion

        //
        // GET: /MailInfo/

        public ActionResult Index()
        {
            return View();
        }

    }
}
