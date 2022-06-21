using DATA.Models;
using DMSS.ViewModals.BookCase;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static DATA.Constant.Constant;
using static DMSS.ViewModals.BookCase.BookCaseViewModal;

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

        public ActionResult BookLoanHistoriDetail(int id)
        {
            return View(_chiTietDatSachService.LayChiTietDatSachTheoMa(id));
        }

        public ActionResult _BookLoanAppointment(int Id)
        {
            var chiTietDatSach = _chiTietDatSachService.LayListChiTietTheoMaSach(Id);
            List<DatSach> ListDatSach = new List<DatSach>();
            foreach (var chiTiet in chiTietDatSach)
            {
                var ds = _datSachService.LayDonDatSachTheoMaDonSach((int)chiTiet.MaDonSach);
                ListDatSach.Add(ds);
            }

            List<List<string>> ngayBans = new List<List<string>>();
            List<string> nb = new List<string>();
            List<List<string>> ngayChuaDuyets = new List<List<string>>();

            foreach (var item in ListDatSach)
            {
                if (item.TinhTrangDonHang == TinhTrangDonHang.DaDuyet)
                {
                    nb.Add(item.NgayNhan.Value.Date.ToString("MM-dd-yyyy"));
                    nb.Add(item.NgayTra.Value.Date.ToString("MM-dd-yyyy"));
                    ngayBans.Add(nb);
                }
                if (item.TinhTrangDonHang == TinhTrangDonHang.DangDat)
                {
                    List<string> ncd = new List<string>();
                    ncd.Add(item.NgayNhan.Value.Date.ToString("MM-dd-yyyy"));
                    ncd.Add(item.NgayTra.Value.Date.ToString("MM-dd-yyyy"));
                    ngayChuaDuyets.Add(ncd);
                } 

            }

            List<string> ngayVuaChon = new List<string>();
            List<List<string>> ListngayVuaChon = new List<List<string>>();

            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
                ngayVuaChon.Add(KeSach.ThoiGianNhan.Date.ToString("MM-dd-yyyy"));
                ngayVuaChon.Add(KeSach.ThoiGianTra.Date.ToString("MM-dd-yyyy"));
                ListngayVuaChon.Add(ngayVuaChon);
                ViewBag.NgayVuaChon = ListngayVuaChon.ToArray();
            }

            ViewBag.NgayChuaDuyet = ngayChuaDuyets.ToArray();
            ViewBag.NgayBan = ngayBans.ToArray();
            ViewBag.MaSach = Id;
            return PartialView();
        }

        public ActionResult BookCase()
        {

            if (Session["GioHang"] != null)
            {
                var dsChiNhanh = _chiNhanhService.DanhSachDiaChi();
                var dieuKhoan = _caiDatService.DanhSachCaiDat().FirstOrDefault();
                KeSach.NguoiDung = _authenticationService.GetAuthenticatedUser();
                KeSach = Session["GioHang"] as BookCaseViewModal;
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
        public JsonResult DatSach(DatSach datSach, DateTime ngayNhan, DateTime ngayTra,string gioNhan, string gioTra)
        {
            KeSach = Session["GioHang"] as BookCaseViewModal;
            KeSach.NguoiDung.DiemTichLuy = (int?)(KeSach.NguoiDung.DiemTichLuy - KeSach.TongDiem);
            _nguoiDungService.CapNhatNguoiDung(KeSach.NguoiDung);
            datSach.MaHoaDonSach = KeSach.NguoiDung.MaNguoiDung.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
            datSach.MaNguoiDung = KeSach.NguoiDung.MaNguoiDung;
            datSach.TinhTrangDonHang = TinhTrangDonHang.DangDat;
            datSach.NgayNhan = DateTime.Parse(ngayNhan.Date.ToString("dd/MM/yyyy") + " " + gioNhan);
            datSach.NgayTra = DateTime.Parse(ngayTra.Date.ToString("dd/MM/yyyy") + " " + gioTra);
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

            if (result && serultCTDS)
            {
                Session["GioHang"] = null;
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddIntoBookCase(int maSach, DateTime ngayNhan, DateTime ngayTra)
        {
            var Sach = _sachService.LayMaSach(maSach);
            var DatSach = _datSachService.DanhSachDatSach();
            var ChiTietDatSach = _chiTietDatSachService.LayListChiTietTheoMaSach(maSach);
            List<DatSach> ListDatSach = new List<DatSach>();
            foreach (var chiTiet in ChiTietDatSach)
            {
                var ds = _datSachService.LayDonDatSachTheoMaDonSach((int)chiTiet.MaDonSach);
                ListDatSach.Add(ds);
            }
            var datesBan = new List<DateTime>();
            var datesChuaDuyet = new List<DateTime>();
            var start = DateTime.Now;
            var end = DateTime.Now;
            var startChuaDuyet = DateTime.Now;
            var endChuaDuyet = DateTime.Now;
            foreach (var dat in ListDatSach)
            {
                if (dat.TinhTrangDonHang == TinhTrangDonHang.DaDuyet)
                {
                    start = dat.NgayNhan.Value.Date;
                    end = dat.NgayTra.Value.Date;
                }
                if (dat.TinhTrangDonHang == TinhTrangDonHang.DangDat)
                {
                    startChuaDuyet = dat.NgayNhan.Value.Date;
                    endChuaDuyet = dat.NgayTra.Value.Date;
                }
            }
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                datesBan.Add(dt);
            }
            for (var dt = startChuaDuyet; dt <= endChuaDuyet; dt = dt.AddDays(1))
            {
                datesChuaDuyet.Add(dt);
            }
            bool thoiGianBan = datesBan.Any(item => item == ngayNhan || item == ngayTra);
            bool thoiGianChuaDuyet = datesChuaDuyet.Any(item => item == ngayNhan || item == ngayTra);
            if (thoiGianBan)
            {
                return Json(new { status = TinhTrangDonHang.DaDuyet}, JsonRequestBehavior.AllowGet);
            }
            if (!CheckAllowAddToBookCase(Sach))
                return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);

            if (Session["GioHang"] != null)
            {
                KeSach = Session["GioHang"] as BookCaseViewModal;
                if (KeSach.ThoiGianNhan.Date == ngayNhan.Date && KeSach.DanhSachSach.Any(s=>s.MaSach == maSach))
                {
                    return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
                }
                if (KeSach.DanhSachSach.Any(s => s.MaSach == maSach))
                {
                    KeSach.ThoiGianNhan = ngayNhan;
                    KeSach.ThoiGianTra = ngayTra;
                    Session["GioHang"] = KeSach;
                    return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
                }
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
            if (thoiGianChuaDuyet)
            {
                KeSach.DanhSachSach.Add(Sach);
                KeSach.ThoiGianNhan = ngayNhan;
                KeSach.ThoiGianTra = ngayTra;
                KeSach.TongDiem = KeSach.DanhSachSach.Sum(s => s.Gia).Value;
                Session["GioHang"] = KeSach;
                return Json(new { status = TinhTrangDonHang.DangDat }, JsonRequestBehavior.AllowGet);
            }
            KeSach.DanhSachSach.Add(Sach);
            KeSach.ThoiGianNhan = ngayNhan;
            KeSach.ThoiGianTra = ngayTra;
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
                    return true;
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