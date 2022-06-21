using DATA.Models;
using DATA.Repository;
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
        private readonly IBaseRepository<NhomTuoi> _nhomTuoiRepository;
        private readonly IBaseRepository<TheLoai> _theLoaiRepository;
        private readonly IBaseRepository<LoaiSach> _loaiSachRepository;
        private readonly IBaseRepository<Sach> _SachRepository;
        private readonly IBaseRepository<HangMucSach> _hangMucSachRepository;

        public BookController(ICaiDatService caiDatService,
                             INguoiDungService nguoiDungSerivce,
                             IDacQuyenService dacQuyenService,
                             ISachService sachSerivce,
                             IDanhGiaService danhGiaSerivce,
                             IHinhAnhSachService hinhAnhSachService,
                             IVideoSachService videoSachService,
                             IAuthenticationService authenticationService,
                             IBaseRepository<NhomTuoi> nhomTuoiRepository,
                             IBaseRepository<TheLoai> theLoaiRepository,
                             IBaseRepository<LoaiSach> loaiSachRepository,
                             IBaseRepository<Sach> SachRepository,
                             IBaseRepository<HangMucSach> hangMucSachRepository)
        {
            _caiDatService = caiDatService;
            _nguoiDungSerivce = nguoiDungSerivce;
            _dacQuyenService = dacQuyenService;
            _danhGiaService = danhGiaSerivce;
            _sachService = sachSerivce;
            _hinhAnhSachService = hinhAnhSachService;
            _videoSachService = videoSachService;
            _authenticationService = authenticationService;
            _nhomTuoiRepository = nhomTuoiRepository;
            _theLoaiRepository = theLoaiRepository;
            _loaiSachRepository = loaiSachRepository;
            _SachRepository = SachRepository;
            _hangMucSachRepository = hangMucSachRepository;
        }
     
        public ActionResult BookDetail(int id)
        {
            ViewBag.NguoiDung = _authenticationService.GetAuthenticatedUser();
            return View(_sachService.LayMaSach(id));
        }

        public ActionResult _ListReviewBook(int id)
        {

            return View(_danhGiaService.LayTheoMaSach(id));
        }
        public ActionResult BookPreview()
        {
            var preview = _videoSachService.DanhSachVideoSach();
            return View(preview);
        }
        public ActionResult BookPreviewDetail(int id)
        {
            var previewDetail = _videoSachService.LayMaVideoPreview(id);
            return View(previewDetail); 
        }
        private IEnumerable<Sach> DuLieuSach(string keyword, int? nhomTuoi, int? theLoai, int? loaiSach, int? Status, string maHangMucSach)
        {
            Boolean duocMuonSach = true;
            if (Status == TinhTrangSach.Avaliable)
            {
                duocMuonSach = true;
            }
            else if (Status == TinhTrangSach.NotAvaliable)
            {
                duocMuonSach = false;
            }

            var duLieuSach = _SachRepository.GetAll(s => s.TrangThai != TinhTrang.Activating)
                          .Where(s => s.TenSach.ToLower().Contains(keyword.ToLower()) || s.TacGia.TenTacGia.Contains(keyword) || keyword == "" || keyword == null)
                          .Where(s => s.MaNhomTuoi == nhomTuoi || nhomTuoi == null || nhomTuoi == 0)
                          .Where(s => s.MaTheLoai == theLoai || theLoai == null || theLoai == 0)
                          .Where(s => s.MaLoaiSach == loaiSach || loaiSach == null || loaiSach == 0)
                          .Where(s => s.TinhTrangMuonSach == duocMuonSach || Status == null || Status == 0)
                          .Where(s => s.TheLoai.MaNhanDienHangMucSach == maHangMucSach || maHangMucSach == null || maHangMucSach == "").ToList();

            return duLieuSach;
        }
        public ActionResult AllBook(int? page, string keyword, int? nhomTuoi, int? theLoai, int? loaiSach, int? Status, string maHangMucSach)
        {
            ViewBag.Category = theLoai;
            ViewBag.Agegroup = nhomTuoi;
            ViewBag.Type = loaiSach;
            ViewBag.Status = Status;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            ViewBag.ListRating = _danhGiaService.DanhSachDanhGia();
            ViewBag.ListAge = _nhomTuoiRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);
            ViewBag.ListCategory = _theLoaiRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);
            ViewBag.ListType = _loaiSachRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);

            Boolean duocMuonSach = true;
            if (Status == TinhTrangSach.Avaliable)
            {
                duocMuonSach = true;
            }
            else if (Status == TinhTrangSach.NotAvaliable)
            {
                duocMuonSach = false;
            }
            var duLieuSach = DuLieuSach(keyword, nhomTuoi, theLoai, loaiSach, Status, maHangMucSach);
            var ListBook = duLieuSach.OrderByDescending(s => s.LanCapNhatCuoi).ToPagedList(pageNumber, pageSize);
            ViewBag.TotalRecord = duLieuSach.Where(s => s.TinhTrangMuonSach == duocMuonSach || Status == null || Status == 0).ToList().Count();
            ViewBag.keyword = keyword;
            return View(duLieuSach);
        }


        public ActionResult _BookListPage(int? page, string keyword, int? nhomTuoi, int? theLoai, int? loaiSach, int? Status, string maHangMucSach)
        {
            ViewBag.Category = theLoai;
            ViewBag.Agegroup = nhomTuoi;
            ViewBag.Type = loaiSach;
            ViewBag.Status = Status;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            ViewBag.ListRating = _danhGiaService.DanhSachDanhGia();
            ViewBag.ListAge = _nhomTuoiRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);
            ViewBag.ListCategory = _theLoaiRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);
            ViewBag.ListType = _loaiSachRepository.GetAll(s => s.TrangThai != TinhTrang.Activating);

            ViewBag.keyword = "Of " + keyword;
            if (maHangMucSach != null && maHangMucSach != "")
            {
                var hangMuc = _hangMucSachRepository.GetAll(s => s.MaNhanDienHangMucSach == maHangMucSach).FirstOrDefault();
                ViewBag.keyword = "Of " + hangMuc.TenHangMuc;
            }
            ViewBag.keywordhl = keyword;
            Boolean duocMuonSach = true;
            if (Status == TinhTrangSach.Avaliable)
            {
                duocMuonSach = true;
            }
            else if (Status == TinhTrangSach.NotAvaliable)
            {
                duocMuonSach = false;
            }
            var duLieuSach = DuLieuSach(keyword, nhomTuoi, theLoai, loaiSach, Status, maHangMucSach);
            var ListBook = duLieuSach.OrderByDescending(s => s.LanCapNhatCuoi).ToPagedList(pageNumber, pageSize);
            ViewBag.TotalRecord = duLieuSach.Where(s => s.TinhTrangMuonSach == duocMuonSach || Status == null || Status == 0).ToList().Count();
            return PartialView(ListBook);
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