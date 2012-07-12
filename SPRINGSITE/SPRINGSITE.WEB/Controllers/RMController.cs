using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPRINGSITE.SERVICE;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.WEB.Controllers
{
    //[Authorize]
    //[System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] //for stopping browser previous and next option
    public class RMController : Controller
    {
        #region Member Variable

        private readonly ISecurityService _iSecurityService;

        #endregion

        #region Constructor

        public RMController(ISecurityService iSecurityService)
        {
            this._iSecurityService = iSecurityService;
        }

        #endregion

        //
        // GET: /RM/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetRoles(DataTableParamModel param)
        {
            var roles = _iSecurityService.GetRoles().ToList();

            var viewRoles = roles.Select(r => new RoleTableModels() { RoleName = r.RoleName });

            IEnumerable<RoleTableModels> filteredRoles;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRoles = viewRoles.Where(rol => rol.RoleName.Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredRoles = viewRoles;
            }

            var viewOdjects = filteredRoles.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from rolMdl in viewOdjects
                         select new[] { rolMdl.RoleName, rolMdl.RoleName };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = roles.Count(),
                iTotalDisplayRecords = filteredRoles.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Role/Details/by id

        public ActionResult Details(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Role role = _iSecurityService.GetRole(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                //return View("_Details", role);
                return View(role);
            }
            return HttpNotFound();
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            //return PartialView("_Create");
            return View();
        }

        //
        // POST: /Role/Create/by object

        [HttpPost]
        public ActionResult Create(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                Role role = new Role();
                role.RoleName = model.RoleName;

                _iSecurityService.CreateRole(role);
                return RedirectToAction("Index");
            }

            //return PartialView("_Create", model);
            return View(model);
        }

        //
        // GET: /Role/Edit/by id

        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Role role = _iSecurityService.GetRole(id);

                RoleModel roleModel = new RoleModel()
                {
                    Id = role.RoleName,
                    RoleName = role.RoleName
                };

                if (role == null)
                {
                    return HttpNotFound();
                }
                //return PartialView("_Edit", roleModel);
                return View(roleModel);
            }
            return HttpNotFound();
        }

        //
        // POST: /Role/Edit/by object

        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            if (ModelState.IsValid)
            {

                Role role = _iSecurityService.GetRole(model.Id);

                if (role != null)
                {
                    try
                    {
                        //role.RoleName = model.RoleName;

                        //_iSecurityService.UpdateRole(role);
                        //return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }


            }
            //return PartialView("_Edit", model);
            return View(model);
        }

        //
        // GET: /Role/Delete/by id

        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Role role = _iSecurityService.GetRole(id);

                RoleModel roleModel = new RoleModel()
                {
                    RoleName = role.RoleName,
                };

                if (role == null)
                {
                    return HttpNotFound();
                }
                //return PartialView("_Delete", roleModel);
                return View(roleModel);
            }
            return HttpNotFound();
        }

        //
        // POST: /Role/Delete/by id

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    Role role = _iSecurityService.GetRole(id);

                    if (role != null)
                    {
                        _iSecurityService.DeleteRole(role.RoleName);

                        return RedirectToAction("Index");
                    }

                }
                catch (Exception)
                {
                    //throw;
                }

            }

            return HttpNotFound();
        }


    }
}
