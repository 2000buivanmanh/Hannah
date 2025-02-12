﻿using ClassLibrary1.Helper;
using DATA.Models;
using DATA.Repository;
using DMSS.ViewModals.DsExcelViewModal;
using HANNAH_NEW_VERSION.Configs;
using LinqToExcel;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [AuthorizeUser(PhanQuyen.Admin)]
    public class TheLoaiSachController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IBaseRepository<TheLoai> _baseRepository;
        private readonly ITheLoaiSachService _theLoaiSachService;
        private readonly IHangMucSachService _hangMucSachService;
        public TheLoaiSachController(ITheLoaiSachService theLoaiSachService, IHangMucSachService hangMucSachSerivce,
                                        IBaseRepository<TheLoai> baseRepository, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _baseRepository = baseRepository;
            _theLoaiSachService = theLoaiSachService;
            _hangMucSachService = hangMucSachSerivce;
        }
        // GET: Admin/TheLoaiSach
        public ActionResult DanhSachTheLoaiSach()
        {
            return View();
        }
        public ActionResult _DanhSachTheLoaiSach()
        {
            var theLoaiSach = _theLoaiSachService.DanhSachTheLoaiSach();
            return PartialView(theLoaiSach);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaTheLoai(int? Id, TheLoai theloai)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
                {
                theloai.NguoiTao = nguoiDung.MaNguoiDung;
                if (theloai.Icon == null)
                {
                    theloai.Icon = "/Areas/Admin/Content/dist/images/Update-icon.jpg";
                }
                var result = _theLoaiSachService.ThemTheLoai(theloai);
                var idTheLoai = _baseRepository.GetAll(p => p.MaTheLoai > 0).Max(p => p.MaTheLoai);
                if (result == string.Empty)
                        return Json(new { status = 1, message = Message.Success, idTheLoai }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                theloai.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _theLoaiSachService.SuaTheLoai(theloai);
                    if (result == string.Empty)
                        return Json(new { status = 1, message = Message.Updated }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = 0, message = result }, JsonRequestBehavior.AllowGet);
                }

        }
        [HttpPost]
        public JsonResult CapNhatTinhTrang(int Id)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var theloai = _theLoaiSachService.LayTheLoaiTheoMa(Id);
                theloai.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (theloai.TrangThai == TinhTrang.Activating)
                    theloai.TrangThai = TinhTrang.IsBlocked;
                else
                    theloai.TrangThai = TinhTrang.Activating;
                var result = _theLoaiSachService.SuaTheLoai(theloai);
                if (result == string.Empty)
                    return Json(new { status = 1, message = Message.Updated }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = 0, message = result}, JsonRequestBehavior.AllowGet);
   
        }


        public ActionResult _ThemOrSuaTheLoai(int? Id)
        {
            ViewBag.HangMucSach = _hangMucSachService.DanhSachHangMuc();
            if(Id == null)
            {
                var theloai = new TheLoai();
                return PartialView(theloai);
            }else
                return PartialView(_theLoaiSachService.LayTheLoaiTheoMa(Id.Value));

        }  

        [HttpPost]
        public JsonResult XoaTheLoai(int[] data)
        {
                var theLoai = _theLoaiSachService.DanhSachTheLoaiSach().Where(s => data.Contains(s.MaTheLoai)).ToList();
                var result = _theLoaiSachService.XoaTheLoaiSach(theLoai);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }

        List<ExcelTheLoaiSach> DsThatbai = new List<ExcelTheLoaiSach>();
        List<ExcelTheLoaiSach> DsThanhCong = new List<ExcelTheLoaiSach>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelTheLoaiSach>(sheetName) select kh;
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
                                    ExcelTheLoaiSach st = new ExcelTheLoaiSach();
                                    st.Stt = i;
                                    st.TenTheLoai = kh.TenTheLoai;
                                    st.HangMuc = kh.HangMuc;
                                    st.NganSach = kh.NganSach;
                                    st.KeSach = kh.KeSach;
                                    st.MaNhanDienHangMucSach = kh.MaNhanDienHangMucSach;
                                    st.Icon = kh.Icon;
                                    st.MoTa = kh.MoTa;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.NganSach) || string.IsNullOrWhiteSpace(st.KeSach) || string.IsNullOrWhiteSpace(st.HangMuc) || string.IsNullOrWhiteSpace(st.TenTheLoai) || string.IsNullOrWhiteSpace(st.MaNhanDienHangMucSach) )
                                    {
                                        st.ThongBao = "Information cannot be blank!";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (IsNumber(st.NganSach) == false || IsNumber(st.KeSach) == false)
                                        {
                                            st.ThongBao = "Book Shelf and Book Partition must enter number !";
                                            DsThatbai.Add(st);
                                        }
                                        else
                                        {
                                            if (st.HangMuc.Length > 10)
                                            {
                                                st.ThongBao = "Category field only accepts 10 digits !";
                                                DsThatbai.Add(st);
                                            }
                                            else
                                            {
                                                if (_hangMucSachService.KiemTraTonTaiMaNhanDienHangMucSach(st.MaNhanDienHangMucSach) == null)
                                                {
                                                    st.ThongBao = "This book catalog code does not exist !";
                                                    DsThatbai.Add(st);
                                                }
                                                else
                                                {
                                                    if (KiemTraHinhAnh(st.Icon) == 0)
                                                    {
                                                        if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                                        {
                                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenTheLoai);
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
                                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenTheLoai);
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
                List<TheLoai> listTheLoaiSach = new List<TheLoai>();
                DsThanhCong = (List<ExcelTheLoaiSach>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {

                    foreach (var item in DsThanhCong)
                    {
                        TheLoai TheLoaiSach = new TheLoai();
                        TheLoaiSach.TenTheLoai = item.TenTheLoai;
                        TheLoaiSach.HangMuc = item.HangMuc;
                        TheLoaiSach.MoTa = item.MoTa;
                        TheLoaiSach.MaNhanDienHangMucSach = item.MaNhanDienHangMucSach;
                        if (TheLoaiSach.Icon == null)
                        {
                            TheLoaiSach.Icon = "/Areas/Admin/Content/dist/images/Update-icon.jpg";
                        }
                        else
                        {
                            TheLoaiSach.Icon = item.Icon;
                        }
                        TheLoaiSach.NganSach = int.Parse(item.NganSach);
                        TheLoaiSach.KeSach = int.Parse(item.KeSach) ;
                        TheLoaiSach.NoiDungSeo = item.NoiDungSeo;
                        TheLoaiSach.TuKhoaSeo = item.TuKhoaSeo;
                        TheLoaiSach.TieuDeSeo = item.TieuDeSeo;
                        TheLoaiSach.DuongDanSeo = item.DuongDanSeo;
                        TheLoaiSach.NgayTao = DateTime.Now;
                        TheLoaiSach.NguoiTao = nguoiDung.MaNguoiDung;
                        TheLoaiSach.TrangThai = false;
                        listTheLoaiSach.Add(TheLoaiSach);
                    }
                    var result = _theLoaiSachService.ThemExcel(listTheLoaiSach);
                    if (result == string.Empty)
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

        public int KiemTraHinhAnh(string img)
        {
            string path = Path.Combine(Server.MapPath("~/Content/Images/Userupload/images/"), img);
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                return 1;
            else
                return 0;
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
    }
}