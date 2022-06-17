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
    public class SlideController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISlideService _slideService;
        public SlideController(ISlideService slideService, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _slideService = slideService;
        }
        // GET: Admin/Slide
        public ActionResult DanhSachSlide()
        {
            return View();
        }
        public ActionResult _DanhSachSlide()
        {
            var Slide = _slideService.LayThongTinSlide();
            return PartialView(Slide);
        }
        public ActionResult _ThemOrSuaSlide(int? Id)
        {
            if (Id == null)
            {
                var Slide = new Slide();
                return PartialView(Slide);
            }
            else
                return PartialView(_slideService.LaySlideTheoMa(Id.Value));

        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaSlide(int? Id, Slide slide)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                slide.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _slideService.ThemSlide(slide);
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                slide.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _slideService.CapNhatSlide(slide);
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CapNhatTinhTrang(int Id)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var slide = _slideService.LaySlideTheoMa(Id);
            slide.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (slide.TrangThai == TinhTrang.Activating)
                slide.TrangThai = TinhTrang.IsBlocked;
            else
                slide.TrangThai = TinhTrang.Activating;
            var result = _slideService.CapNhatSlide(slide);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult XoaSlide(int[] data)
        {
            var slide = _slideService.LayDanhSachMa(data);
            var result = _slideService.XoaSlide(slide);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

    }
}