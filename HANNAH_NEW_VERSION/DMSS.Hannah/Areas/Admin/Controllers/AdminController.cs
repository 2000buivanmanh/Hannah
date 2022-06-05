using HANNAH_NEW_VERSION.Configs;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [AuthorizeUser(PhanQuyen.Admin)]
    public class AdminController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AdminController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        // GET: Admin/Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}