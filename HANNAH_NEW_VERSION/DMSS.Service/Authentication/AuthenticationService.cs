using DATA.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using static DATA.Constant.Constant;

namespace SERVICE
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly TimeSpan _thoiGianHetHan;
        private readonly INguoiDungService _nguoiDungSerivce;
        private readonly HttpContextBase _httpContext;
        private NguoiDung _cachedT;
        public AuthenticationService(INguoiDungService nguoiDungSerivce, HttpContextBase httpContext)
        {
            _thoiGianHetHan = FormsAuthentication.Timeout;
            _nguoiDungSerivce = nguoiDungSerivce;
            _httpContext = httpContext;
        }

        protected virtual NguoiDung GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("None ticket");
            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
                return null;
            return _nguoiDungSerivce.KiemTraTenDangNhap(username);
        }



        public void DangNhap(NguoiDung nguoiDung, bool createPersistentCookie)
        {
            bool isPersistent = false;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                nguoiDung.TenDangNhap,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                isPersistent,
                nguoiDung.TenDangNhap,
                FormsAuthentication.FormsCookiePath
                );

            var encryTicket = FormsAuthentication.Encrypt(ticket);
            _httpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryTicket));

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            _httpContext.Response.Cookies.Add(cookie);
            _cachedT = nguoiDung;
        }

        public void DangXuat()
        {
            _cachedT = null;
            FormsAuthentication.SignOut();
            List<string> authenticatedUsers = new List<string>();
            if (HttpRuntime.Cache[typeof(NguoiDung).Name] != null)
                authenticatedUsers = (List<string>)HttpRuntime.Cache[typeof(NguoiDung).Name];
            if (authenticatedUsers.Contains(_httpContext.User.Identity.Name))
            {
                authenticatedUsers.Remove(_httpContext.User.Identity.Name);
                HttpRuntime.Cache[typeof(NguoiDung).Name] = authenticatedUsers;
            }
        }
        public NguoiDung GetAuthenticatedUser()
        {
            if (_cachedT != null)
                return _cachedT;

            if (_httpContext?.Request == null || !_httpContext.Request.IsAuthenticated || !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }
            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var account = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (account != null && account.TinhTrangNguoiDung == TinhTrangNguoiDung.DangHoatDong)
                _cachedT = account;

            List<string> authenticatedUsers = new List<string>();
            if (HttpRuntime.Cache[typeof(NguoiDung).Name] != null)
                authenticatedUsers = (List<string>)HttpRuntime.Cache[typeof(NguoiDung).Name];
            if (!authenticatedUsers.Contains(_cachedT.TenDangNhap))
            {
                authenticatedUsers.Add(_cachedT.TenDangNhap);
                HttpRuntime.Cache[typeof(NguoiDung).Name] = authenticatedUsers;
            }

            return _cachedT;
        }
       
    }
}

