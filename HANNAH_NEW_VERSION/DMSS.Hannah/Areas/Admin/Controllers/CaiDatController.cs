using DATA.Models;
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
    public class CaiDatController : Controller
    {
        // GET: Admin/CaiDat
        private readonly ICaiDatService _caiDatService;
        private readonly IAuthenticationService _authenticationService;
        public CaiDatController(ICaiDatService caiDatService, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _caiDatService = caiDatService;
        }
        public ActionResult ThongTinCaiDat()
        {
            return View(_caiDatService.LayThongTinWeb());
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult CapNhatWebsite(CaiDat caiDat)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            caiDat.QTVCapNhat = nguoiDung.MaNguoiDung;
            var result = _caiDatService.CapNhatThongTinWeb(caiDat);
            if (result)
            {
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new {  status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
        }
    }
}