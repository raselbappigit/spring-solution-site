using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPRINGSITE.SERVICE;

namespace SPRINGSITE.WEB.Controllers
{
    public class HomeController : Controller
    {
        #region Member Variable

        private readonly ISecurityService _iSecurityService;

        #endregion

        #region Constructor

        public HomeController(ISecurityService iSecurityService)
        {
            this._iSecurityService = iSecurityService;
        }

        #endregion

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var test = _iSecurityService.GetRoles();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
