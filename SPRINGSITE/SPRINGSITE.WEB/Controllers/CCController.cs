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
    public class CCController : Controller
    {
        #region Member Variable

        private readonly IContactContentService _iContactContentService;

        #endregion

        #region Constructor

        public CCController(IContactContentService iContactContentService)
        {
            this._iContactContentService = iContactContentService;
        }

        #endregion

        //
        // GET: /CC/

        public ActionResult Index()
        {
            return View();
        }

    }
}
