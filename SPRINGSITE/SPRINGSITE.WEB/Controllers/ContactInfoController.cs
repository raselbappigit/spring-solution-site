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
    public class ContactInfoController : Controller
    {
        #region Member Variable

        private readonly IContactInfoService _iContactInfoService;

        #endregion

        #region Constructor

        public ContactInfoController(IContactInfoService iContactInfoService)
        {
            this._iContactInfoService = iContactInfoService;
        }

        #endregion

        //
        // GET: /ContactInfo/

        public ActionResult Index()
        {
            return View();
        }

    }
}
