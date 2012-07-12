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
    public class PCController : Controller
    {
        #region Member Variable

        private readonly IPortfolioContentService _iPortfolioContentService;

        #endregion

        #region Constructor

        public PCController(IPortfolioContentService iPortfolioContentService)
        {
            this._iPortfolioContentService = iPortfolioContentService;
        }

        #endregion

        //
        // GET: /PC/

        public ActionResult Index()
        {
            return View();
        }

    }
}
