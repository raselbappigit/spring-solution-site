﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;
using System.Security.Cryptography;
using System.Web.Security;

namespace SPRINGSITE.SERVICE
{
    public interface ISecurityService
    {
        IEnumerable<User> GetUsers();
        //IEnumerable<User> GetLoggedInUsers();

        User GetUser(string userName);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string userName);

        //void ChangeUserLogInStatus(User user, bool inout);

        IEnumerable<Role> GetRoles();
        Role GetRole(string roleName);
        IEnumerable<User> GetUsersInRole(string roleName);

        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(string roleName);

        void AddUserToRole(string userName, List<string> roleNames);

        bool ForgetPassword(User user);
        string HashResetParams(string username);

        void Save();
    }

    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IRoleRepository _iRoleRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public SecurityService(IUserRepository iUserRepository, IRoleRepository iRoleRepository, IUnitOfWork iUnitOfWork)
        {
            this._iUserRepository = iUserRepository;
            this._iRoleRepository = iRoleRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _iUserRepository.GetAll();
            return users;
        }

        //public IEnumerable<User> GetLoggedInUsers()
        //{
        //    var users = userRepository.GetAll().Where(x => x.IsLoggedIn == true).ToList();
        //    return users;
        //}

        public User GetUser(string userName)
        {
            var user = _iUserRepository.GetById(userName);
            return user;
        }

        public void CreateUser(User user)
        {
            _iUserRepository.Add(user);
            Save();
        }

        public void UpdateUser(User user)
        {
            _iUserRepository.Update(user);
            Save();
        }

        public void DeleteUser(string userName)
        {
            var user = GetUser(userName);
            _iUserRepository.Delete(user);
            Save();
        }

        //public void ChangeUserLogInStatus(User user, bool inout)
        //{
        //    User userDb = userRepository.GetById(user.UserName);
        //    if (userDb != null)
        //    {
        //        userDb.IsLoggedIn = inout;
        //        userRepository.Update(userDb);
        //        Save();
        //    }
        //}

        public IEnumerable<Role> GetRoles()
        {
            var roles = _iRoleRepository.GetAll();
            return roles;
        }

        public Role GetRole(string roleName)
        {
            var role = _iRoleRepository.GetById(roleName);
            return role;
        }

        public IEnumerable<User> GetUsersInRole(string roleName)
        {
            List<User> users = null;
            var role = _iRoleRepository.GetById(roleName);

            if (role != null)
            {
                users = _iRoleRepository.GetById(roleName).Users.ToList();
            }

            return users;
        }

        public void CreateRole(Role role)
        {
            _iRoleRepository.Add(role);
            Save();
        }

        public void UpdateRole(Role role)
        {
            _iRoleRepository.Update(role);
            Save();
        }

        public void DeleteRole(string roleName)
        {
            var role = GetRole(roleName);
            _iRoleRepository.Delete(role);
            Save();
        }

        public void AddUserToRole(string userName, List<string> roleNames)
        {
            _iUserRepository.AssignRole(userName, roleNames);
            Save();
        }

        #region Reset Password

        //Reset Password
        public bool ForgetPassword(User user)
        {
            try
            {
                if (!String.IsNullOrEmpty(user.Email))
                {
                    SendResetEmail(user);
                    return true;
                }

                return false;

            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        //Send Email Method
        public void SendResetEmail(User user)
        {
            //string mailBody = GetResetEmailBody(user.UserName);
            //const string mailSubject = "Your new password";
            //const string mailFrom = "mail@springsolution.net";
            //string mailTo = user.Email;
            //MailService mailService = new MailService();

            //MailServiceResult mailResult = mailService.SendEmail(mailTo, mailFrom, mailSubject, mailBody);
        }

        public string HashResetParams(string username)
        {
            byte[] bytesofLink = Encoding.UTF8.GetBytes(username);
            MD5 md5 = new MD5CryptoServiceProvider();
            string hashParams = BitConverter.ToString(md5.ComputeHash(bytesofLink));

            return hashParams;
        }

        private string GetResetEmailBody(string userName)
        {
            string link = "http://www.springsolution.net/Account/ResetPassword/?username=" + userName + "&reset=" + HashResetParams(userName);

            string mailBody = "<p>Hi " + userName + "," + "</p>";
            mailBody += "<p>You just requested a new password for your Spring Solution Ltd. account.</p>";
            mailBody += "---------------------------------------------------------------------------</br>";
            mailBody += "For creating a new password please click on the following link:</br>";
            mailBody += "<a href='" + link + "'>" + link + "</a></br>";
            mailBody += "---------------------------------------------------------------------------</br>";
            mailBody += "<p>Please keep your password safe to prevent unathorized access.</p>";
            mailBody += "---------------------</br>";
            mailBody += "Thanks for with us.</br>";
            mailBody += "keep it up.</br>";
            mailBody += "---------------------</br>";
            mailBody += @"<p>http://www.springsolution.net/</p>";
            mailBody += @"<p>&copy; " + DateTime.Now.Year + " - Spring Solution | Spring Solution ltd. - Bangladesh</p>";

            return mailBody;
        }

        #endregion

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
