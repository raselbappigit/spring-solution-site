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
    public class ACController : Controller
    {
        #region Member Variable

        private readonly IAboutContentService _iAboutContentService;

        #endregion

        #region Constructor

        public ACController(IAboutContentService iAboutContentService)
        {
            this._iAboutContentService = iAboutContentService;
        }

        #endregion

        //
        // GET: /AC/

        public ActionResult Index()
        {
            return View();
        }

    }
}
