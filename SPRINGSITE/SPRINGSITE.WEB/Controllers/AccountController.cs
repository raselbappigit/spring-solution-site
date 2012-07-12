﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SPRINGSITE.SERVICE;
using SPRINGSITE.DOMAIN;

namespace SPRINGSITE.WEB.Controllers
{

    [Authorize]
    //[System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] //for stopping browser previous and next option
    public class AccountController : Controller
    {
        #region Member Variable

        private readonly ISecurityService _iSecurityService;

        #endregion

        #region Constructor

        public AccountController(ISecurityService iSecurityService)
        {
            this._iSecurityService = iSecurityService;
        }

        #endregion

        #region Login and Logoff

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Registation

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Change Password

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #endregion

        #region Reset Paswword

        [AllowAnonymous]
        public ActionResult ResetPassword(string username, string reset)
        {
            string userName = username;

            MembershipUser currentUser = Membership.GetUser(userName);

            if (!string.IsNullOrEmpty(reset) && !string.IsNullOrEmpty(username))
            {
                if (currentUser != null)
                {
                    if (_iSecurityService.HashResetParams(currentUser.UserName) == reset)
                    {

                        var user = _iSecurityService.GetUser(userName);

                        if (user != null)
                        {
                            ChangePasswordModel changePasswordModel = new ChangePasswordModel
                            {
                                UserName = user.UserName,
                                OldPassword = reset,
                                NewPassword = null,
                                ConfirmPassword = null
                            };
                            return View(changePasswordModel);
                        }
                    }

                }

            }

            return RedirectToAction("Login", "Security");

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                string userName = model.UserName;

                MembershipUser currentUser = Membership.GetUser(userName);

                bool resetPasswordSucceeded = false;

                if (currentUser != null)
                {
                    if (_iSecurityService.HashResetParams(currentUser.UserName) == model.OldPassword)
                    {
                        string oldPassword = currentUser.ResetPassword();

                        if (!string.IsNullOrEmpty(oldPassword))
                        {
                            resetPasswordSucceeded = currentUser.ChangePassword(oldPassword, model.NewPassword);
                        }
                    }
                }

                if (resetPasswordSucceeded)
                {
                    return RedirectToAction("ResetPasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }

            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                User user = _iSecurityService.GetUsers().Where(x => x.Email == model.UserEmail).FirstOrDefault();

                if (user != null)
                {
                    if (_iSecurityService.ForgetPassword(user))
                    {
                        //return RedirectToAction("Login", "Account");
                        const string returnStr = @"<div class='message-info'>Please, check you email for reset password.</div>";
                        return Content(returnStr);
                    }
                    else
                    {
                        return Content("Sorry, we could not find anyone with that email address!");
                    }
                }
                else
                {
                    return Content("You are not valid user!");
                }

            }
            else
            {
                return Content("Please review your form!");
            }
        }

        #endregion

        #region Status Codes
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
