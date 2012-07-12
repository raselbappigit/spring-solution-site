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
    public class BCController : Controller
    {
        #region Member Variable

        private readonly IBannerContentService _iBannerContentService;

        #endregion

        #region Constructor

        public BCController(IBannerContentService iBannerContentService)
        {
            this._iBannerContentService = iBannerContentService;
        }

        #endregion

        //
        // GET: /BC/

        public ActionResult Index()
        {
            return View();
        }

    }
}
