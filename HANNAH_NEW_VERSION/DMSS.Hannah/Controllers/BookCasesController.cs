using DATA.Models;
using DMSS.ViewModals.BookCase;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Controllers
{
    public class BookCasesController : Controller
    {
        private BookCaseViewModal KeSach = new BookCaseViewModal();

        private readonly ISachService _sachService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IChiNhanhService _chiNhanhService;
        private readonly IDatSachService _datSachService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IChiTietDatSachService _chiTietDatSachService;
        private readonly ICaiDatService _caiDatService;
        public BookCasesController(
                            ISachService sachSerivce,
                            IAuthenticationService authenticationService,
                            IChiNhanhService chiNhanhService,
                            IDatSachService datSachService,
                            INguoiDungService nguoiDungService,
                            IChiTietDatSachService chiTietDatSachService,
                            ICaiDatService caiDatService)
        {
            _sachService = sachSerivce;
            _authenticationService = authenticationService;
            _chiNhanhService = chiNhanhService;
            _datSachService = datSachService;
            _nguoiDungService = nguoiDungService;
            _chiTietDatSachService = chiTietDatSachService;
            _caiDatService = caiDatService;
        }

        public ActionResult BookCase()
        {

            if (Session["GioHang"] != null)
            {
                var dsChiNhanh = _chiNhanhService.DanhSachDiaChi();
                var dieuKhoan = _caiDatService.DanhSachCaiDat().FirstOrDefault();
                KeSach.NguoiDung = _authenticationService.GetAuthenticatedUser();
                KeSach = Session["GioHang"] as BookCaseViewModal;
                KeSach.ThoiGianNhan = DateTime.Now;
                KeSach.ThoiGianTra = DateTime.Now.AddDays(7);
                KeSach.DanhSachChiNhanh = dsChiNhanh;
                KeSach.DieuKhoan = dieuKhoan.DieuKhoan;
                return View(KeSach);
            }
            return View();
        }
        public ActionResult _BookCase()
        {
            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
                return PartialView(KeSach);
            }
            return PartialView();
        }

        public ActionResult _BookCaseHeader()
        {
            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
            }
            else
            {
                KeSach.DanhSachSach = new List<Sach>();
            }
            return PartialView(KeSach);
        }

        [HttpPost]
        public JsonResult DatSach(DatSach datSach)
        {
            KeSach = Session["GioHang"] as BookCaseViewModal;
            KeSach.NguoiDung.DiemTichLuy = (int?)(KeSach.NguoiDung.DiemTichLuy - KeSach.TongDiem);
            _nguoiDungService.CapNhatNguoiDung(KeSach.NguoiDung);
            datSach.MaHoaDonSach = KeSach.NguoiDung.MaNguoiDung.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
            datSach.MaNguoiDung = KeSach.NguoiDung.MaNguoiDung;
            datSach.TinhTrangDonHang = TinhTrangDonHang.DangCho;
            datSach.NgayDat = DateTime.Now;
            var result = _datSachService.DatSach(datSach);


            List<ChiTietDatSach> chiTietDatSach = new List<ChiTietDatSach>();
            foreach (var item in KeSach.DanhSachSach)
            {
                var chiTiet = new ChiTietDatSach();
                chiTiet.MaDonSach = datSach.MaDonSach;
                chiTiet.MaSach = item.MaSach;
                chiTiet.SoLuong = 1;
                chiTietDatSach.Add(chiTiet);
            }
            var serultCTDS = _chiTietDatSachService.ThemChiTietDatSach(chiTietDatSach);
            Session["GioHang"] = null;
            if (result && serultCTDS)
            {
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddIntoBookCase(int maSach)
        {
            var Sach = _sachService.LayMaSach(maSach);
            if (!CheckAllowAddToBookCase(Sach))
                return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);

            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
            }
            else
            {
                KeSach.NguoiDung = _authenticationService.GetAuthenticatedUser();
                KeSach.DanhSachSach = new List<Sach>();
            }

            var diemThua = KeSach.NguoiDung.DiemTichLuy - KeSach.TongDiem;
            if (diemThua < Sach.Gia)
            {
                return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
            }
            KeSach.DanhSachSach.Add(Sach);
            KeSach.TongDiem = KeSach.DanhSachSach.Sum(s => s.Gia).Value;
            Session["GioHang"] = KeSach;
            return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveOfBookCase(int maSach)
        {
            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
                var sach = KeSach.DanhSachSach.Find(s => s.MaSach == maSach);
                KeSach.DanhSachSach.Remove(sach);
                if (KeSach.DanhSachSach.Count() == 0)
                {
                    Session["GioHang"] = null;
                    return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
                }

            }
            KeSach.TongDiem = KeSach.DanhSachSach.Sum(s => s.Gia).Value;
            Session["GioHang"] = KeSach;
            return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveBookAll()
        {
            if (Session["GioHang"] != null)
            {
                KeSach.DanhSachSach = new List<Sach>();
            }
            KeSach.TongDiem = KeSach.DanhSachSach.Sum(s => s.Gia).Value;
            Session["GioHang"] = KeSach;
            Session["GioHang"] = null;
            return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
        }


        bool CheckAllowAddToBookCase(Sach sach)
        {
            if (sach.TinhTrangMuonSach == true)
            {
                return false;
            }
            if (Session["GioHang"] != null)
            {
                var gioHang = Session["GioHang"] as BookCaseViewModal;
                if (gioHang.DanhSachSach.Any(s => s.MaSach == sach.MaSach))
                    return false;
                var nguoiDung = _authenticationService.GetAuthenticatedUser();

                if (!User.Identity.IsAuthenticated)
                {
                    return true;
                }
                if (gioHang.TongDiem >= nguoiDung.DiemTichLuy)
                    return false;
            }

            return true;
        }



    }
}