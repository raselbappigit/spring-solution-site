using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.SERVICE;
using SPRINGSITE.WEB.Helpers;
using System.Web.Security;

namespace SPRINGSITE.WEB.Controllers
{
    //[Authorize]
    //[System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] //for stopping browser previous and next option
    public class UMController : Controller
    {
        #region Member Variable

        private readonly ISecurityService _iSecurityService;
        private readonly IProfileService _iProfileService;

        #endregion

        #region Constructor

        public UMController(ISecurityService iSecurityService, IProfileService iProfileService)
        {
            this._iSecurityService = iSecurityService;
            this._iProfileService = iProfileService;
        }

        #endregion

        //
        // GET: /UM/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetUsers(DataTableParamModel param)
        {
            var users = _iSecurityService.GetUsers().ToList();

            var viewUsers = users.Select(u => new UserTableModels() { UserName = u.UserName, Email = u.Email, FullName = u.Profile == null ? null : Convert.ToString(u.Profile.FullName), Address = u.Profile == null ? null : Convert.ToString(u.Profile.Address), Phone = u.Profile == null ? null : Convert.ToString(u.Profile.PhoneNumber), Mobile = u.Profile == null ? null : Convert.ToString(u.Profile.MobileNumber), CreateDate = u.Profile == null ? null : Convert.ToString(u.DateCreated) });

            IEnumerable<UserTableModels> filteredUsers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = viewUsers.Where(usr => (usr.UserName ?? "").Contains(param.sSearch) || (usr.FullName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredUsers = viewUsers;
            }

            var viewOdjects = filteredUsers.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from usrMdl in viewOdjects
                         select new[] { usrMdl.UserName, usrMdl.UserName, usrMdl.Email, usrMdl.FullName, usrMdl.Address, usrMdl.Phone, usrMdl.Mobile, usrMdl.CreateDate };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = users.Count(),
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /User/Details/

        public ActionResult Details(string id = null)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Details");

            if (!string.IsNullOrEmpty(id))
            {
                var roles = _iSecurityService.GetRoles().ToList();

                var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
                {
                    RoleName = role.Users.Where(x => x.UserName.ToLower() == id.ToLower()).Count() == 0 ? null : role.RoleName,
                }).ToList());

                User user = _iSecurityService.GetUser(id);

                Profile profile = _iProfileService.GetProfiles().Where(x => x.UserName.ToLower() == id.ToLower()).FirstOrDefault();

                if (user == null)
                {
                    this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
                    return RedirectToAction("Index");
                }

                CreateUserModel viewUserModel = new CreateUserModel();

                if (profile == null)
                {
                    viewUserModel.UserName = user.UserName;
                    viewUserModel.Email = user.Email;
                    viewUserModel.Password = null;
                    viewUserModel.ConfirmPassword = null;
                }
                else
                {
                    viewUserModel.UserName = user.UserName;
                    viewUserModel.Email = user.Email;
                    viewUserModel.Password = null;
                    viewUserModel.ConfirmPassword = null;
                    viewUserModel.FirstName = profile.FirstName;
                    viewUserModel.LastName = profile.LastName;
                    viewUserModel.SurName = profile.SurName;
                    viewUserModel.DateOfBirth = profile.DateOfBirth == null ? null : profile.DateOfBirth.Value.ToString("MM/dd/yyyy");
                    viewUserModel.Address = profile.Address;
                    viewUserModel.PhoneNumber = profile.PhoneNumber;
                    viewUserModel.MobileNumber = profile.MobileNumber;
                    viewUserModel.ThumbImageUrl = profile.ThumbImageUrl;
                    viewUserModel.SmallImageUrl = profile.SmallImageUrl;
                }

                viewUserModel.RoleModels = roleModels;

                //return View("_Details", viewUserModel);
                return View(viewUserModel);
            }
            this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Create");

            var roles = _iSecurityService.GetRoles().ToList();

            CreateUserModel createUserModel = new CreateUserModel();

            var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
            {
                RoleName = role.RoleName
            }).ToList());

            createUserModel.RoleModels = roleModels;

            //return PartialView("_Create", createUserModel);
            return View(createUserModel);
        }

        //
        // POST: /User/Create/by object

        [HttpPost]
        public ActionResult Create(CreateUserModel model, string[] privilegeName)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Create");

            var roles = _iSecurityService.GetRoles().ToList();

            var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
            {
                RoleName = role.RoleName
            }).ToList());

            model.RoleModels = roleModels;

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {

                    User user = _iSecurityService.GetUser(model.UserName);

                    if (user != null)
                    {
                        Profile profile = new Profile
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            SurName = model.SurName,
                            Address = model.Address,
                            DateOfBirth = Convert.ToDateTime(model.DateOfBirth),
                            MobileNumber = model.MobileNumber,
                            PhoneNumber = model.PhoneNumber,
                            ThumbImageUrl = model.ThumbImageUrl,
                            SmallImageUrl = model.SmallImageUrl,
                            UserName = model.UserName
                        };

                        //Profile create for user
                        _iProfileService.CreateProfile(profile);

                        var selectRoles = roles;

                        var lstRoles = new List<Role>();

                        foreach (var roleName in privilegeName)
                        {
                            string id = roleName;
                            lstRoles.Add(selectRoles.Where(x => x.RoleName == id).FirstOrDefault());
                        }

                        user.Roles = lstRoles;
                        user.Profile = profile;

                        //User Update
                        _iSecurityService.UpdateUser(user);
                        this.ShowMessage("User created successfully", MessageType.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user is invalid.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", AccountController.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            //return PartialView("_Create", model);
            return View(model);
        }

        //
        // GET: /User/Edit/by id

        public ActionResult Edit(string id = null)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Edit");

            if (!string.IsNullOrEmpty(id))
            {
                var roles = _iSecurityService.GetRoles().ToList();

                var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
                {
                    RoleName = role.RoleName,
                    Assigned = role.Users.Where(x => x.UserName.ToLower() == id.ToLower()).Count() == 0 ? false : true
                }).ToList());

                User user = _iSecurityService.GetUser(id);

                Profile profile = _iProfileService.GetProfiles().Where(x => x.UserName.ToLower() == id.ToLower()).FirstOrDefault();

                if (user == null)
                {
                    this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
                    return RedirectToAction("Index");
                }

                EditUserModel editUserModel = new EditUserModel();

                if (profile == null)
                {
                    editUserModel.UserName = user.UserName;
                    editUserModel.Email = user.Email;
                    editUserModel.OldPassword = null;
                    editUserModel.NewPassword = null;
                    editUserModel.ConfirmPassword = null;

                }
                else
                {
                    editUserModel.UserName = user.UserName;
                    editUserModel.Email = user.Email;
                    editUserModel.OldPassword = null;
                    editUserModel.NewPassword = null;
                    editUserModel.ConfirmPassword = null;
                    editUserModel.FirstName = profile.FirstName;
                    editUserModel.LastName = profile.LastName;
                    editUserModel.SurName = profile.SurName;
                    editUserModel.DateOfBirth = profile.DateOfBirth == null ? null : profile.DateOfBirth.Value.ToString("MM/dd/yyyy");
                    editUserModel.Address = profile.Address;
                    editUserModel.PhoneNumber = profile.PhoneNumber;
                    editUserModel.MobileNumber = profile.MobileNumber;
                    editUserModel.ThumbImageUrl = profile.ThumbImageUrl;
                    editUserModel.SmallImageUrl = profile.SmallImageUrl;
                }

                editUserModel.RoleModels = roleModels;

                //return PartialView("_Edit", editUserModel);
                return View(editUserModel);
            }
            this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
            return RedirectToAction("Index");
        }

        //
        // POST: /User/Edit/by object

        [HttpPost]
        public ActionResult Edit(EditUserModel model, string[] privilegeName)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Edit");

            var roles = _iSecurityService.GetRoles().ToList();

            var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
            {
                RoleName = role.RoleName,
                Assigned = role.Users.Where(x => x.UserName.ToLower() == model.UserName.ToLower()).Count() == 0 ? false : true
            }).ToList());

            model.RoleModels = roleModels;

            if (ModelState.IsValid)
            {
                User user = _iSecurityService.GetUser(model.UserName);

                if (user != null)
                {
                    bool changePasswordSucceeded;

                    if (model.OldPassword != null && model.NewPassword != null && model.ConfirmPassword != null)
                    {
                        try
                        {
                            MembershipUser currentUser = Membership.GetUser(model.UserName, userIsOnline: true);
                            changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                        }
                        catch (Exception)
                        {
                            changePasswordSucceeded = false;
                        }
                    }
                    else
                    {
                        changePasswordSucceeded = true;
                    }

                    if (changePasswordSucceeded)
                    {
                        try
                        {
                            var selectRoles = roles;

                            var lstRoles = new List<Role>();

                            foreach (var roleName in privilegeName)
                            {
                                string id = roleName;
                                lstRoles.Add(selectRoles.Where(x => x.RoleName == id).FirstOrDefault());
                            }

                            user.Roles = lstRoles;

                            Profile profile = _iProfileService.GetProfiles().Where(x => x.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault();

                            if (profile != null)
                            {
                                profile.FirstName = model.FirstName;
                                profile.LastName = model.LastName;
                                profile.SurName = model.SurName;
                                profile.Address = model.Address;
                                profile.DateOfBirth = Convert.ToDateTime(model.DateOfBirth);
                                profile.MobileNumber = model.MobileNumber;
                                profile.PhoneNumber = model.PhoneNumber;
                                profile.ThumbImageUrl = model.ThumbImageUrl;
                                profile.SmallImageUrl = model.SmallImageUrl;
                                profile.UserName = model.UserName;

                                _iProfileService.UpdateProfile(profile);

                                user.Profile = profile;
                            }
                            else
                            {
                                Profile tempProfile = new Profile
                                {
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    SurName = model.SurName,
                                    Address = model.Address,
                                    DateOfBirth = Convert.ToDateTime(model.DateOfBirth),
                                    MobileNumber = model.MobileNumber,
                                    PhoneNumber = model.PhoneNumber,
                                    ThumbImageUrl = model.ThumbImageUrl,
                                    SmallImageUrl = model.SmallImageUrl,
                                    UserName = model.UserName
                                };

                                _iProfileService.CreateProfile(tempProfile);

                                user.Profile = tempProfile;
                            }

                            _iSecurityService.UpdateUser(user);
                            this.ShowMessage("User updated successfully", MessageType.Success);
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }


            }
            //return PartialView("_Edit", model);
            return View(model);
        }

        //
        // GET: /User/Delete/by id

        public ActionResult Delete(string id = null)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Delete");

            if (!string.IsNullOrEmpty(id))
            {
                var roles = _iSecurityService.GetRoles().ToList();

                var appPrivilegeModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
                {
                    RoleName = role.Users.Where(x => x.UserName.ToLower() == id.ToLower()).Count() == 0 ? null : role.RoleName,
                }).ToList());

                User user = _iSecurityService.GetUser(id);

                Profile profile = _iProfileService.GetProfiles().Where(x => x.UserName.ToLower() == id.ToLower()).FirstOrDefault();

                if (user == null)
                {
                    this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
                    return RedirectToAction("Index");
                }

                CreateUserModel viewUserModel = new CreateUserModel();

                if (profile == null)
                {
                    viewUserModel.UserName = user.UserName;
                    viewUserModel.Email = user.Email;
                    viewUserModel.Password = null;
                    viewUserModel.ConfirmPassword = null;
                }
                else
                {
                    viewUserModel.UserName = user.UserName;
                    viewUserModel.Email = user.Email;
                    viewUserModel.Password = null;
                    viewUserModel.ConfirmPassword = null;
                    viewUserModel.FirstName = profile.FirstName;
                    viewUserModel.LastName = profile.LastName;
                    viewUserModel.SurName = profile.SurName;
                    viewUserModel.DateOfBirth = profile.DateOfBirth == null ? null : profile.DateOfBirth.Value.ToString("MM/dd/yyyy");
                    viewUserModel.Address = profile.Address;
                    viewUserModel.PhoneNumber = profile.PhoneNumber;
                    viewUserModel.MobileNumber = profile.MobileNumber;
                    viewUserModel.ThumbImageUrl = profile.ThumbImageUrl;
                    viewUserModel.SmallImageUrl = profile.SmallImageUrl;
                }

                viewUserModel.RoleModels = appPrivilegeModels;

                //return PartialView("_Delete", viewUserModel);
                return View(viewUserModel);
            }
            this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
            return RedirectToAction("Index");
        }

        //
        // POST: /User/Delete/by id

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Delete");

            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    User user = _iSecurityService.GetUser(id);

                    if (user != null)
                    {
                        _iSecurityService.DeleteUser(user.UserName);
                        this.ShowMessage("User deleted successfully", MessageType.Success);
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    this.ShowMessage("Error on data generation with the following details " + ex.Message, MessageType.Error);
                }

            }

            this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
            return RedirectToAction("Index");
        }

        public ActionResult Role(string id)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Privilege");

            if (!string.IsNullOrEmpty(id))
            {
                string userName = id;

                var roles = _iSecurityService.GetRoles().ToList();

                if (roles == null)
                {
                    return HttpNotFound();
                }

                CreateUserModel createUserModel = new CreateUserModel { UserName = userName };

                var roleModels = roles.Count() == 0 ? null : (roles.Select(role => new SelectRoleModel
                {
                    RoleName = role.RoleName,
                    Assigned = role.Users.Where(x => x.UserName == userName).Count() == 0 ? false : true
                }).ToList());

                createUserModel.RoleModels = roleModels;

                //return PartialView("_Privilege", createUserModel);
                //return View("_Privilege", createUserModel);))
                return View(createUserModel);
            }
            this.ShowMessage("Sorry! Data not found. You've been redirected to the default page instead.", MessageType.Error);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Role(string userName, string[] privilegeName)
        {
            this.ShowTitle("User Management");
            this.ShowBreadcrumb("User", "Privilege");

            var tempRoles = _iSecurityService.GetRoles().ToList();

            CreateUserModel createUserModel = new CreateUserModel { UserName = userName };

            var roleModels = tempRoles.Count() == 0 ? null : (tempRoles.Select(role => new SelectRoleModel
            {
                RoleName = role.RoleName,
                Assigned = role.Users.Where(x => x.UserName == userName).Count() == 0 ? false : true
            }).ToList());

            createUserModel.RoleModels = roleModels;

            if (!string.IsNullOrEmpty(userName))
            {
                User user = _iSecurityService.GetUser(userName);

                if (user != null)
                {
                    try
                    {
                        List<string> roles = new List<string>();

                        foreach (var item in privilegeName)
                        {
                            roles.Add(item);
                        }

                        _iSecurityService.AddUserToRole(user.UserName, roles);
                        this.ShowMessage("User privilege seted successfully", MessageType.Success);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessage("Error on data generation with the following details " + ex.Message, MessageType.Error);
                    }
                }
            }
            //return PartialView("_Privilege", createUserModel);
            return View(createUserModel);
        }

        public PartialViewResult UserRoles(string usrId)
        {
            string userName = usrId;

            var user = _iSecurityService.GetUser(userName);

            var selectUsers = user.Roles.ToList();

            IEnumerable<SelectRoleModel> appPrivilegeModels = selectUsers.Count() == 0 ? null : (selectUsers.Select(role => new SelectRoleModel
            {
                RoleName = role.RoleName
            }).ToList());


            return PartialView("_UserRoles", appPrivilegeModels);
        }

    }
}
