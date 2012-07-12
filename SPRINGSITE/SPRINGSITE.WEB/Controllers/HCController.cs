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
    public class HCController : Controller
    {
        #region Member Variable

        private readonly IHomeContentService _iHomeContentService;

        #endregion

        #region Constructor

        public HCController(IHomeContentService iHomeContentService)
        {
            this._iHomeContentService = iHomeContentService;
        }

        #endregion

        //
        // GET: /HC/

        public ActionResult Index()
        {
            return View();
        }

    }
}
