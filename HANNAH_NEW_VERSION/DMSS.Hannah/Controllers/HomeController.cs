using DATA.Models;
using DATA.Repository;
using DMSS.ViewModals.BookCase;
using DMSS.ViewModals.HomePage;
using PagedList;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICaiDatService _caiDatService;
        private readonly INguoiDungService  _nguoiDungSerivce; 
        private readonly IDacQuyenService  _dacQuyenService;
        private readonly ISlideService _slideService;
        private readonly IDanhGiaService _danhGiaService;
        private readonly ISachService _sachService;


        public HomeController(ICaiDatService caiDatService,
                              INguoiDungService nguoiDungSerivce,
                              IDacQuyenService dacQuyenService,
                              IAuthenticationService authenticationService,
                              ISlideService slideService,
                              ISachService sachSerivce,
                              IDanhGiaService danhGiaSerivce)
        {
            _caiDatService = caiDatService;
            _nguoiDungSerivce = nguoiDungSerivce;
            _dacQuyenService = dacQuyenService;
            _authenticationService = authenticationService;
            _slideService = slideService;
            _danhGiaService = danhGiaSerivce;
            _sachService = sachSerivce;
        }

        private List<Sach> getbooks(int count)
        {
            return _sachService.ThongTinSach();
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _TopHeaderLayOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.GioHang = Session["GioHang"];
                ViewBag.NguoiDung = _authenticationService.GetAuthenticatedUser();
            }
            var caiDat = _caiDatService.LayThongTinWeb();
            return PartialView(caiDat);
        }
        public ActionResult _Slider()
        {
            return PartialView(_slideService.LayThongTinSlide());
        }
        public ActionResult _Sort_About()
        {
            var setting = _caiDatService.LayThongTinWeb();
            return PartialView(setting);
        }
        public ActionResult _MenuLayOut()
        {
            return PartialView();
        }
        public ActionResult _DacQuyen()
        {
            var dacQuyen = _dacQuyenService.DanhSachDacQuyen();
            return PartialView(dacQuyen);
        }
        public ActionResult _FooterLayOut()
        {
            var setting = _caiDatService.LayThongTinWeb();
            return PartialView(setting);
        }
        public ActionResult About ()
        {
            var setting = _caiDatService.LayThongTinWeb();
            return View(setting);
        }
        public ActionResult Feedback()
        {
            return PartialView();
        }

        public ActionResult _TopRate()
        {
            var TopRated = from ListRating in _danhGiaService.DanhSachDanhGia()
                           group ListRating by ListRating.MaSach into ListGroup
                           select new
                           {
                               Book_Id = ListGroup.Key,
                               PoinCount = ListGroup.Sum(x => x.DiemDanhGia) / ListGroup.Count()

                           };
            List<XepHangDanhGia> topRatings = new List<XepHangDanhGia>();

            var tolst = TopRated.OrderByDescending(s => s.PoinCount).Take(5).ToList();
            foreach (var item in tolst)
            {
                XepHangDanhGia rt = new XepHangDanhGia();
                rt.MaSach = item.Book_Id;
                rt.SoDiem = item.PoinCount;
                topRatings.Add(rt);
            }
            ViewBag.TopRating = topRatings;
            var ListBook = _sachService.ThongTinSach().Where(s => s.TrangThai != true).OrderByDescending(s => s.LanCapNhatCuoi).ToList();
            return PartialView(ListBook);
        }

        public ActionResult _TopReader()
        {
            var ListTopReader = _nguoiDungSerivce.DanhSachNguoiDung().OrderByDescending(s => s.DiemTichLuy).Take(5).ToList();
            return PartialView(ListTopReader);
        }
        public ActionResult _ListBookHompage(int page)
        {
            int pageSize = 12;
            int skip = (page - 1) * pageSize;
            int take = page * pageSize;
            var lstBook = _sachService.GetAllPaging(s => s.TrangThai != true, s=>s.MaSach , take, skip).OrderByDescending(s => s.LanCapNhatCuoi).ToList();
            ViewBag.ListRating = _danhGiaService.DanhSachDanhGia();
            var data = new List<ListBookViewModal>();
            var CheckTime = DateTime.Now.AddDays(-15);
            foreach (var book in lstBook)
            {
                var dt = new ListBookViewModal();
                dt.Sach = book;
                if (book.LanCapNhatCuoi > CheckTime)
                    dt.IsNew = true;
                else
                    dt.IsNew = false;

                dt.AllowAdd = CheckAllowAddToBookCase(book);
                data.Add(dt);
            }
            ViewBag.ListBook = _sachService.ThongTinSach().Where(s => s.TrangThai != true).OrderByDescending(s => s.LanCapNhatCuoi).ToPagedList(1, pageSize);
            ViewBag.TotalRecord = _sachService.ThongTinSach().Where(s => s.TrangThai != true).Count();
            ViewBag.GioHang = Session["GioHang"];
            return PartialView(data);
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

        public ActionResult Preview()
        {
            return PartialView();
        }
        public class XepHangDanhGia
        {
            public int? MaSach { get; set; }
            public int? SoDiem { get; set; }
        }

        
    }
}