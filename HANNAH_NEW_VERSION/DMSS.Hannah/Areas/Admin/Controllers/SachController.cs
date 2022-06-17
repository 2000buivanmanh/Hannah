using ClassLibrary1.Helper;
using DATA.Models;
using DATA.Repository;
using DMSS.ViewModals.DsExcelViewModal;
using HANNAH_NEW_VERSION.Configs;
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
    [AuthorizeUser(PhanQuyen.Admin)]
    public class SachController : Controller
    {
        // GET: Admin/Sach
        private readonly IBaseRepository<Sach> _baseRepository;
        private readonly IHinhAnhSachService _hinhAnhSachService;
        private readonly IAuthenticationService _authenticationService;
        public readonly ISachService _sachService;
        public readonly INhomTuoiService _nhomTuoiService;
        public readonly ITheLoaiSachService _theLoaiSachService;
        public readonly ITacGiaService _tacGiaService;
        public readonly ILoaiSachService _loaiSachService;
        public readonly INhaXuatBanService _nhaXuatBanService;
        public readonly IDiaChiXuatBanService _diaChiXuatBanService;
        public SachController(ISachService sachService, INhomTuoiService nhomTuoiService, ITheLoaiSachService theLoaiSachService,
                              ITacGiaService tacGiaService, ILoaiSachService loaiSachService, INhaXuatBanService nhaXuatBanService,
                                IAuthenticationService authenticationService, IDiaChiXuatBanService diaChiXuatBanService,
                                IBaseRepository<Sach> baseRepository, IHinhAnhSachService hinhAnhSachService)
        {
            _nhomTuoiService = nhomTuoiService;
            _sachService = sachService;
            _theLoaiSachService = theLoaiSachService;
            _tacGiaService = tacGiaService;
            _loaiSachService = loaiSachService;
            _nhaXuatBanService = nhaXuatBanService;
            _authenticationService = authenticationService;
            _diaChiXuatBanService = diaChiXuatBanService;
            _baseRepository = baseRepository;
            _hinhAnhSachService = hinhAnhSachService;
        }

        public ActionResult ThongTinSach()
        {
            return View();
        }
        public ActionResult _DanhSachSach()
        {
            var sach = _sachService.ThongTinSach();
            return PartialView(sach);
        }

        public ActionResult _ThemOrSuaSach(int? Id)
        {
            List<NhomTuoi> listNhomTuoi = _nhomTuoiService.DanhSachNhomTuoi();
            List<NhomTuoi> sortNhomTuoi = listNhomTuoi.OrderBy(o => o.DoTuoiMin).ToList();
            ViewBag.NhomTuoi = sortNhomTuoi;
            ViewBag.TheLoaiSach = _theLoaiSachService.DanhSachTheLoaiSach();
            ViewBag.TacGia = _tacGiaService.DanhSachTacGia();
            ViewBag.LoaiSach = _loaiSachService.DanhSachLoaiSach();
            ViewBag.NhaXuatBan = _nhaXuatBanService.DanhSachNhaXuatBan();
            ViewBag.DiaChiXuatBan = _diaChiXuatBanService.DanhSachDiaChiXuatBan();
            List<HinhAnhSach> listout = new List<HinhAnhSach>();
            foreach (var item in _hinhAnhSachService.LayListHinhTheoMa(Id))
            {
                HinhAnhSach hinhAnhSach = new HinhAnhSach();
                hinhAnhSach.TenAnh = item.TenAnh;
                listout.Add(hinhAnhSach);
            }
            ViewBag.HinhAnh = listout;

            if (Id == null || Id == 0)
            {
                var sach = new Sach();
                return PartialView(sach);
            }
            else
                return PartialView(_sachService.LayMaSach(Id.Value));
        }



        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaSach(int? Id, Sach sach)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                double stt;
                var laysstcu = sach;
                if (laysstcu.MaTheLoai == null)
                {
                    stt = 1;
                }
                else
                {
                    if (laysstcu.MaNhanDienSach == null)
                    {
                        stt = 1;
                    }
                    else
                    {
                        var a = laysstcu.MaNhanDienSach.Substring(5);
                        stt = int.Parse(a) + 1;
                    }
                }
                double sttcuoi = Math.Round(stt, 3);
                var theLoai = _theLoaiSachService.LayTheLoaiTheoMa((Int32)sach.MaTheLoai);
                string maDaiDienSach = theLoai.HangMuc + theLoai.KeSach + theLoai.NganSach + sttcuoi;
                sach.MaNhanDienSach = maDaiDienSach.Replace(" ", "");
                if (sach.ThongTinAnhSach == null)
                {
                    sach.ThongTinAnhSach = "/Areas/Admin/Content/dist/images/update.jpg";
                }
                sach.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _sachService.ThemSach(sach);
                if (result == string.Empty)

                    return Json(new { status = 1, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                sach.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _sachService.SuaSach(sach);
                if (result == string.Empty)
                    return Json(new { status = 1, message = Message.Updated }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CapNhatTrangThai(int Id)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var sach = _sachService.LayMaSach(Id);
            sach.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (sach.TrangThai == TinhTrang.Activating)
                sach.TrangThai = TinhTrang.IsBlocked;
            else
                sach.TrangThai = TinhTrang.Activating;
            var result = _sachService.SuaSach(sach);
            if (result == string.Empty)
                return Json(new { status = 1, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CapNhatTinhTrangMuonSach(int Id)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var sach = _sachService.LayMaSach(Id);
            sach.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (sach.TinhTrangMuonSach == TinhTrang.Activating)
                sach.TinhTrangMuonSach = TinhTrang.IsBlocked;
            else
                sach.TinhTrangMuonSach = TinhTrang.Activating;
            var result = _sachService.SuaSach(sach);
            if (result == string.Empty)
                return Json(new { status = 1, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult XoaSach(int[] data)
        {
            var hinhAnh = _hinhAnhSachService.LayDanhSachMa(data);
            var resultHA = _hinhAnhSachService.XoaHinhAnh(hinhAnh);
            var sach = _sachService.LayDanhSachMa(data);
            var result = _sachService.XoaSach(sach);
            if (result == string.Empty && resultHA == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListImg(string[] listImg, int id)
        {
            if (id == 0)
            {
                if (listImg != null)
                {
                    var idSach = _baseRepository.GetAll().Max(p => p.MaSach);
                    List<HinhAnhSach> listHinhAnh = new List<HinhAnhSach>();
                    foreach (var item in listImg)
                    {
                        HinhAnhSach hinhAnhSach = new HinhAnhSach();
                        hinhAnhSach.MaSach = idSach;
                        hinhAnhSach.TenAnh = item;
                        listHinhAnh.Add(hinhAnhSach);
                    }
                    var result = _hinhAnhSachService.ThemList(listHinhAnh);
                    if (result == string.Empty)
                        return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (listImg != null)
                {
                    var hinhAnh = _hinhAnhSachService.LayListHinhTheoMa(id);
                    var delete = _hinhAnhSachService.XoaHinhAnh(hinhAnh);
                    List<HinhAnhSach> listHinhAnh = new List<HinhAnhSach>();
                    foreach (var item in listImg)
                    {
                        HinhAnhSach hinhAnhSach = new HinhAnhSach();
                        hinhAnhSach.MaSach = id;
                        hinhAnhSach.TenAnh = item;
                        listHinhAnh.Add(hinhAnhSach);
                    }
                    var result = _hinhAnhSachService.ThemList(listHinhAnh);
                    if (result == string.Empty && delete == string.Empty)
                        return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var hinhAnh = _hinhAnhSachService.LayListHinhTheoMa(id);
                    var delete = _hinhAnhSachService.XoaHinhAnh(hinhAnh);
                    if (delete == string.Empty)
                        return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = TrangThai.ThatBai, message = delete }, JsonRequestBehavior.AllowGet);
                }

            }

        }

        List<ExcelSach> DsThatbai = new List<ExcelSach>();
        List<ExcelSach> DsThanhCong = new List<ExcelSach>();
        public JsonResult ImportExcel(HttpPostedFileBase myExcelData)
        {
            if (myExcelData != null)
            {
                try
                {
                    if (myExcelData.ContentType == "application/vnd.ms-excel" || myExcelData.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string FileName = "HannahExcel" + DateTime.Now.ToString("yyyyMMddHHmmss") + myExcelData.FileName;
                        string SavePatch = Server.MapPath("~/Areas/Admin/Content/Excel/");//Path
                        FileHelper.UploadFile(myExcelData, SavePatch, FileName);

                        var pathToExcelFile = SavePatch + FileName;
                        string sheetName = "Sheet1";

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var dataList = from kh in excelFile.Worksheet<ExcelSach>(sheetName) select kh;
                        if (dataList.Count() > 0)
                        {
                            if (Session["DsThanhCong"] != null)
                            {
                                DsThanhCong.Clear();
                                Session["DsThanhCong"] = DsThanhCong;
                            }
                            if (Session["DsThatbai"] != null)
                            {
                                DsThatbai.Clear();
                                Session["DsThatbai"] = DsThatbai;
                            }
                            Session["DsThatbai"] = DsThatbai;
                            int i = 1;
                            foreach (var kh in dataList)
                            {
                                try
                                {
                                    ExcelSach st = new ExcelSach();
                                    st.Stt = i;
                                    st.TenSach = kh.TenSach;
                                    st.Gia = kh.Gia;
                                    st.MoTaSachEn = kh.MoTaSachEn;
                                    st.MoTaSachVi = kh.MoTaSachVi;
                                    st.ThongTinAnhSach = kh.ThongTinAnhSach;
                                    st.DanhSachAnh = kh.DanhSachAnh;
                                    st.NhomTuoi = kh.NhomTuoi;
                                    st.TenTheLoai = kh.TenTheLoai;
                                    st.TenLoaiSach = kh.TenLoaiSach;
                                    st.TenNhaXuatBan = kh.TenNhaXuatBan;
                                    st.TenTacGia = kh.TenTacGia;
                                    st.TenDiaChiXB = kh.TenDiaChiXB;
                                    st.SoLuongTrang = kh.SoLuongTrang;
                                    st.KichThuocSach = kh.KichThuocSach;
                                    st.NgayXuatBan = kh.NgayXuatBan;
                                    st.LanDauXuatBan = kh.LanDauXuatBan;
                                    st.MaTieuChuanSach = kh.MaTieuChuanSach;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.TenNhaXuatBan) || string.IsNullOrWhiteSpace(st.TenTacGia) || string.IsNullOrWhiteSpace(st.TenTheLoai) ||
                                        string.IsNullOrWhiteSpace(st.TenDiaChiXB) || string.IsNullOrWhiteSpace(st.NhomTuoi) ||
                                        string.IsNullOrWhiteSpace(st.TenLoaiSach) || string.IsNullOrWhiteSpace(st.TenSach) || string.IsNullOrWhiteSpace(st.Gia))
                                    {
                                        st.ThongBao = "Information cannot be blank!";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (IsNumber(st.Gia) == false)
                                        {
                                            st.ThongBao = "Age must enter number !";
                                            DsThatbai.Add(st);
                                        }
                                        else
                                        {
                                            if (decimal.Parse(st.Gia) < 0)
                                            {
                                                st.ThongBao = "Price cannot enter a negative number !";
                                                DsThatbai.Add(st);
                                            }
                                            else
                                            {
                                                if (_loaiSachService.LayLoaiSachTheoTen(st.TenLoaiSach) == null)
                                                {
                                                    st.ThongBao = "The type of book does not exist !";
                                                    DsThatbai.Add(st);
                                                }
                                                else
                                                {
                                                    if (_theLoaiSachService.LayTheLoaiTheoTen(st.TenTheLoai) == null)
                                                    {
                                                        st.ThongBao = "Book genre does not exist!";
                                                        DsThatbai.Add(st);
                                                    }
                                                    else
                                                    {
                                                        if (_nhaXuatBanService.LayNhaXuatBanTheoTen(st.TenNhaXuatBan) == null)
                                                        {
                                                            st.ThongBao = "Imprint does not exist!";
                                                            DsThatbai.Add(st);
                                                        }
                                                        else
                                                        {
                                                            if (_tacGiaService.LayTacGiaTheoTen(st.TenTacGia) == null)
                                                            {
                                                                st.ThongBao = "Author does not exist!";
                                                                DsThatbai.Add(st);
                                                            }
                                                            else
                                                            {
                                                                if (_diaChiXuatBanService.LayDiaChiXuatBanTheoTen(st.TenDiaChiXB) == null)
                                                                {
                                                                    st.ThongBao = "Publishing Address does not exist!";
                                                                    DsThatbai.Add(st);
                                                                }
                                                                else
                                                                {
                                                                    if (KiemTraTuoi(st.NhomTuoi) == null)
                                                                    {
                                                                        st.ThongBao = " Invalid age group !";
                                                                        DsThatbai.Add(st);
                                                                    }
                                                                    else
                                                                    {
                                                                        if((st.NgayXuatBan !=null || st.LanDauXuatBan != null ) && (!st.NgayXuatBan.HasValue || !st.LanDauXuatBan.HasValue))
                                                                        {
                                                                            st.ThongBao = " Invalid date entered !";
                                                                            DsThatbai.Add(st);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (KiemTraHinhAnh(st.DanhSachAnh) == 0)
                                                                            {
                                                                                if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                                                                {
                                                                                    st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenSach);
                                                                                    st.ThongBao = "Image does not exist in Ckfinder - Please update after adding !";
                                                                                    DsThanhCong.Add(st);
                                                                                }
                                                                                else
                                                                                {
                                                                                    st.ThongBao = "Image does not exist in Ckfinder - Please update after adding !";
                                                                                    DsThanhCong.Add(st);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (KiemTraHinhAnh(st.ThongTinAnhSach) == 0)
                                                                                {
                                                                                    if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                                                                    {
                                                                                        st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenSach);
                                                                                        st.ThongBao = "Image does not exist in Ckfinder - Please update after adding !";
                                                                                        DsThanhCong.Add(st);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        st.ThongBao = "Image does not exist in Ckfinder - Please update after adding !";
                                                                                        DsThanhCong.Add(st);
                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                                                                    {
                                                                                        st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenSach);
                                                                                        st.ThongBao = "New data added";
                                                                                        DsThanhCong.Add(st);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        st.ThongBao = "New data added";
                                                                                        DsThanhCong.Add(st);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    Session["DsThatbai"] = DsThatbai;
                                    Session["DsThanhCong"] = DsThanhCong;
                                }
                                catch (DbEntityValidationException ex)
                                {
                                    return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
                                }
                                i++;
                            }
                        }
                        else
                        {
                            return Json(new { status = TrangThai.ThatBai, message = Message.Success }, JsonRequestBehavior.AllowGet);
                        }
                        if ((System.IO.File.Exists(pathToExcelFile)))
                        {
                            System.IO.File.Delete(pathToExcelFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            }

            else
                return Json(new { status = TrangThai.ThatBai, message = Message.FileNotFound }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _DanhSachExcel()
        {
            ViewBag.DsThanhCong = Session["DsThanhCong"];
            ViewBag.DsThatbai = Session["DsThatbai"];
            return PartialView();
        }
        public JsonResult AddListExcel()
        {
            try
            {
                var nguoiDung = _authenticationService.GetAuthenticatedUser();
            List<Sach> listSach = new List<Sach>();
            List<HinhAnhSach> hinhAnhSach = new List<HinhAnhSach>();
            DsThanhCong = (List<ExcelSach>)Session["DsThanhCong"];
            int a = _baseRepository.GetAll().Max(s=>s.MaSach);
            if (DsThanhCong.Count != 0)
            {
                foreach (var item in DsThanhCong)
                {

                    Sach sach = new Sach();
                    sach.MaTheLoai = _theLoaiSachService.LayTheLoaiTheoTen(item.TenTheLoai).MaTheLoai;
                    sach.MaLoaiSach = _loaiSachService.LayLoaiSachTheoTen(item.TenLoaiSach).MaLoaiSach;
                    sach.MaNhomTuoi = KiemTraTuoi(item.NhomTuoi).MaNhomTuoi;
                    sach.MaTacGia = _tacGiaService.LayTacGiaTheoTen(item.TenTacGia).MaTacGia;
                    sach.MaNhaXuatBan = _nhaXuatBanService.LayNhaXuatBanTheoTen(item.TenNhaXuatBan).MaNhaXuatBan;
                    sach.MaDiaChi = _diaChiXuatBanService.LayDiaChiXuatBanTheoTen(item.TenDiaChiXB).MaDiaChi;

                    double stt;
                    var laysstcu = sach;
                    if (laysstcu.MaTheLoai == null)
                    {
                        stt = 1;
                    }
                    else
                    {
                        if (laysstcu.MaNhanDienSach == null)
                        {
                            stt = 1;
                        }
                        else
                        {
                            var i = laysstcu.MaNhanDienSach.Substring(5);
                            stt = int.Parse(i) + 1;
                        }
                    }
                    double sttcuoi = Math.Round(stt, 3);
                    var theLoai = _theLoaiSachService.LayTheLoaiTheoTen(item.TenTheLoai);
                    string maDaiDienSach = theLoai.HangMuc + theLoai.KeSach + theLoai.NganSach + sttcuoi;
                    sach.MaNhanDienSach = maDaiDienSach.Replace(" ", "");
                    sach.TenSach = item.TenSach;
                    sach.Gia = decimal.Parse(item.Gia);
                    sach.MoTaSachEn = item.MoTaSachEn;
                    sach.MoTaSachVi = item.MoTaSachVi;
                    if (item.ThongTinAnhSach == null)
                    {
                        sach.ThongTinAnhSach = "/Areas/Admin/Content/dist/images/update.jpg";
                    }
                    else
                    {
                        sach.ThongTinAnhSach = "/Content/Images/Userupload/images/"+item.ThongTinAnhSach;
                    }
                    sach.SoLuongTrang = item.SoLuongTrang;
                    sach.KichThuocSach = item.KichThuocSach;
                    sach.NgayXuatBan = item.NgayXuatBan;
                    sach.LanDauXuatBan = item.LanDauXuatBan;
                    sach.MaTieuChuanSach = item.MaTieuChuanSach;
                    sach.NoiDungSeo = item.NoiDungSeo;
                    sach.TuKhoaSeo = item.TuKhoaSeo;
                    sach.TieuDeSeo = item.TieuDeSeo;
                    sach.DuongDanSeo = item.DuongDanSeo;
                    sach.NgayTao = DateTime.Now;
                    sach.NguoiTao = nguoiDung.MaNguoiDung;
                    sach.TrangThai = false;
                    listSach.Add(sach);
                    a++;
                    if (item.DanhSachAnh != null)
                    {
                        string[] arrListHinh = item.DanhSachAnh.Split(' ');
                        foreach (var listHinh in arrListHinh)
                        {
                            HinhAnhSach hinhAnh = new HinhAnhSach();
                            hinhAnh.MaSach = a;
                            hinhAnh.TenAnh = "/Content/Images/Userupload/images/" + listHinh;
                            hinhAnhSach.Add(hinhAnh);
                        }

                    }
                }
                var result =_sachService.ThemExcel(listSach);

                var result1 = _hinhAnhSachService.ThemList(hinhAnhSach);


                if (result == string.Empty && result1  == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = TrangThai.ThatBai, message = Message.Nodata }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public NhomTuoi KiemTraTuoi(string tuoi)
        {
            tuoi = tuoi.Replace(" ", "");
            string[] arrListStr = tuoi.Split('-');
            return _nhomTuoiService.KiemTraDoTuoi(Int32.Parse(arrListStr[0]), Int32.Parse(arrListStr[arrListStr.Length - 1]));
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public int KiemTraHinhAnh(string img)
        {
            if(img != null)
            {
                string[] arrListStr = img.Split(' ');
                int a = 0, b = 0;
                foreach (var item in arrListStr)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Userupload/images/"), item);
                    if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                        a++;
                    else
                        b++;
                }

                if (a == arrListStr.Length)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }
    }
}
