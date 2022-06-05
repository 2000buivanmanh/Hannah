using DATA.Models;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Controllers
{
    public class BookController : Controller
    {
        private readonly ISachService _sachService;
        private readonly IDanhGiaService _danhGiaService;
        private readonly ICaiDatService _caiDatService;
        private readonly INguoiDungService _nguoiDungSerivce;
        private readonly IDacQuyenService _dacQuyenService;
        private readonly IHinhAnhSachService _hinhAnhSachService;
        private readonly IVideoSachService _videoSachService;
        private readonly IAuthenticationService _authenticationService;

        public BookController(ICaiDatService caiDatService,
                             INguoiDungService nguoiDungSerivce,
                             IDacQuyenService dacQuyenService,
                             ISachService sachSerivce,
                             IDanhGiaService danhGiaSerivce,
                             IHinhAnhSachService hinhAnhSachService,
                             IVideoSachService videoSachService,
                             IAuthenticationService authenticationService)
        {
            _caiDatService = caiDatService;
            _nguoiDungSerivce = nguoiDungSerivce;
            _dacQuyenService = dacQuyenService;
            _danhGiaService = danhGiaSerivce;
            _sachService = sachSerivce;
            _hinhAnhSachService = hinhAnhSachService;
            _videoSachService = videoSachService;
            _authenticationService = authenticationService;
        }
     
        public ActionResult BookDetail(int id)
        {
            ViewBag.ListNguoiDung = _nguoiDungSerivce.DanhSachNguoiDung();
            return View(_sachService.LayMaSach(id));
        }

        public ActionResult _ListReviewBook(int id)
        {

            return View(_danhGiaService.LayTheoMaSach(id));
        }
        public ActionResult BookPreview()
        {
            return View();
        }

      

        [Authorize]
        [HttpPost]
        public JsonResult ThemDanhGia(DanhGia danhGia)
        {
            danhGia.MaNguoiDung = _authenticationService.GetAuthenticatedUser().MaNguoiDung;
            danhGia.NgayDanhGia = DateTime.Now;
            var result = _danhGiaService.ThemDanhGia(danhGia);
            if (result)
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public JsonResult XoaDanhGia(int[] id)
        {
            var danhGia = _danhGiaService.LayDacQuyenTheoMa(id);
            var result = _danhGiaService.XoaDanhGia(danhGia);
            if (result)
                return Json(new { status = TrangThai.ThanhCong}, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DatSach(DatSach datSach)
        {
            return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
        }
    }
}