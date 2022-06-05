using ClassLibrary1.Helper;
using DATA.Models;
using DMSS.ViewModals.DsExcelViewModal;
using LinqToExcel;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [Authorize]
    public class DacQuyenController : Controller
    {
        private readonly IDacQuyenService _dacQuyenService;
        private readonly IAuthenticationService _authenticationService;
        public DacQuyenController(IDacQuyenService dacQuyenService,          
                                     IAuthenticationService authenticationService)
        {
            _dacQuyenService = dacQuyenService;
            _authenticationService = authenticationService;
        }
        // GET: Admin/DacQuyen
        public ActionResult DanhSachDacQuyen()
        {
            return View();
        }
        public ActionResult _DanhSachDacQuyen()
        {
            var DacQuyen = _dacQuyenService.DanhSachDacQuyen();
            return PartialView(DacQuyen);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaDacQuyen(int? Id, DacQuyen dacQuyen)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                dacQuyen.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _dacQuyenService.ThemDacQuyen(dacQuyen);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dacQuyen.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _dacQuyenService.SuaDacQuyen(dacQuyen);
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CapNhatTinhTrang(int Id)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var dacQuyen = _dacQuyenService.LayDacQuyenTheoMa(Id);
            dacQuyen.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (dacQuyen.TrangThai == TinhTrang.Activating)
                dacQuyen.TrangThai = TinhTrang.IsBlocked;
            else
                dacQuyen.TrangThai = TinhTrang.Activating;
            var result = _dacQuyenService.SuaDacQuyen(dacQuyen);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ThemOrSuaDacQuyen(int? Id)
        {            if (Id == null)
            {
                var dacQuyen = new DacQuyen();
                return PartialView(dacQuyen);
            }
            else
                return PartialView(_dacQuyenService.LayDacQuyenTheoMa(Id.Value));
        }

        [HttpPost]
        public JsonResult XoaDacQuyen(int[] data)
        {
            var dacQuyen = _dacQuyenService.LayDanhSachMa(data);
            var result = _dacQuyenService.XoaDacQuyen(dacQuyen);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
    }
}