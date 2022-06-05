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
    public class BaiVietController : Controller
    {
        // GET: Admin/BaiViet
        private readonly IBaiVietService _baiVietService;
        private readonly IAuthenticationService _authenticationService;
        public BaiVietController(IBaiVietService baiVietService,
                                     IAuthenticationService authenticationService)
        {
            _baiVietService = baiVietService;
            _authenticationService = authenticationService;
        }
        public ActionResult DanhSachBaiViet()
        {
            return View();
        }
        public ActionResult _DanhSachBaiViet()
        {
            var baiViet = _baiVietService.DanhSachBaiViet();
            return PartialView(baiViet);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaBaiViet(int? Id, BaiViet baiViet)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                baiViet.NguoiDang = nguoiDung.MaNguoiDung;
                var result = _baiVietService.ThemBaiViet(baiViet);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                baiViet.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _baiVietService.SuaBaiViet(baiViet);
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
            var baiViet = _baiVietService.LayBaiVietTheoMa(Id);
            baiViet.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (baiViet.TrangThai == TinhTrang.Activating)
                baiViet.TrangThai = TinhTrang.IsBlocked;
            else
                baiViet.TrangThai = TinhTrang.Activating;
            var result = _baiVietService.SuaBaiViet(baiViet);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ThemOrSuaBaiViet(int? Id)
        {
            if (Id == null)
            {
                var baiViet = new BaiViet();
                return PartialView(baiViet);
            }
            else
                return PartialView(_baiVietService.LayBaiVietTheoMa(Id.Value));
        }

        [HttpPost]
        public JsonResult XoaBaiViet(int[] data)
        {
            var baiViet = _baiVietService.LayDanhSachMa(data);
            var result = _baiVietService.XoaBaiViet(baiViet);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
    }
}