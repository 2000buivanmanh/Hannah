using ClassLibrary1.Helper;
using DATA.Models;
using DATA.Repository;
using DMSS.ViewModals.DsExcelViewModal;
using HANNAH_NEW_VERSION.Configs;
using LinqToExcel;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [AuthorizeUser(PhanQuyen.Admin)]
    public class LoaiSachController : Controller
    {
        private readonly IBaseRepository<LoaiSach> _baseRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoaiSachService _loaiSachService;

        public LoaiSachController(ILoaiSachService loaiSachService, IBaseRepository<LoaiSach> baseRepository,
                                    IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
            _loaiSachService = loaiSachService;
        }
        public ActionResult DanhSachLoaiSach()
        {
            return View();
        }
        public ActionResult _DanhSachLoaiSach()
        {
            var LoaiSach = _loaiSachService.DanhSachLoaiSach();
            return PartialView(LoaiSach);
        }
        public ActionResult _ThemOrSuaLoaiSach(int? Id)
        {
            if (Id == null)
            {
                var loaiSach = new LoaiSach();
                return PartialView(loaiSach);
            }
            else
                return PartialView(_loaiSachService.LayLoaiSachTheoMa(Id.Value));

        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaLoaiSach(int? Id, LoaiSach loaiSach)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                loaiSach.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _loaiSachService.ThemLoaiSach(loaiSach);
                var idLoaiSach = _baseRepository.GetAll(p => p.MaLoaiSach > 0).Max(p => p.MaLoaiSach);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success, idLoaiSach }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                loaiSach.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _loaiSachService.SuaLoaiSach(loaiSach);
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
            var LoaiSach = _loaiSachService.LayLoaiSachTheoMa(Id);
            LoaiSach.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (LoaiSach.TrangThai == TinhTrang.Activating)
                LoaiSach.TrangThai = TinhTrang.IsBlocked;
            else
                LoaiSach.TrangThai = TinhTrang.Activating;
            var result = _loaiSachService.SuaLoaiSach(LoaiSach);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult XoaLoaiSach(int[] data)
        {
            var loaiSach = _loaiSachService.LayDanhSachMa(data);
            var result = _loaiSachService.XoaLoaiSach(loaiSach);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        List<ExcelLoaiSach> DsThatbai = new List<ExcelLoaiSach>();
        List<ExcelLoaiSach> DsThanhCong = new List<ExcelLoaiSach>();
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
                    var dataList = from kh in excelFile.Worksheet<ExcelLoaiSach>(sheetName) select kh;
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
                                ExcelLoaiSach st = new ExcelLoaiSach();
                                st.Stt = i;
                                st.TenLoaiSach = kh.TenLoaiSach;
                                st.Mota = kh.Mota;
                                st.NoiDungSeo = kh.NoiDungSeo;
                                st.TuKhoaSeo = kh.TuKhoaSeo;
                                st.TieuDeSeo = kh.TieuDeSeo;
                                st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.TenLoaiSach))
                                    {
                                        st.ThongBao = "Name of the book cannot be blank!";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                        {
                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenLoaiSach);
                                            st.ThongBao = "New data added";
                                            DsThanhCong.Add(st);
                                        }
                                        else
                                        {
                                            st.ThongBao = "New data added";
                                            DsThanhCong.Add(st);
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
                    return Json(new { status = TrangThai.ThatBai , message = ex .Message}, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            }
           
            else
                return Json(new { status = TrangThai.ThatBai , message = Message.Failure }, JsonRequestBehavior.AllowGet);

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
                List<LoaiSach> listLoaiSach = new List<LoaiSach>();
                DsThanhCong = (List<ExcelLoaiSach>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {
                    foreach (var item in DsThanhCong)
                    {
                        LoaiSach loaiSach = new LoaiSach();
                        loaiSach.TenLoaiSach = item.TenLoaiSach;
                        loaiSach.MoTa = item.Mota;
                        loaiSach.NoiDungSeo = item.NoiDungSeo;
                        loaiSach.TuKhoaSeo = item.TuKhoaSeo;
                        loaiSach.TieuDeSeo = item.TieuDeSeo;
                        loaiSach.DuongDanSeo = item.DuongDanSeo;
                        loaiSach.NgayTao = DateTime.Now;
                        loaiSach.NguoiTao = nguoiDung.MaNguoiDung;
                        loaiSach.TrangThai = false;
                        listLoaiSach.Add(loaiSach);
                    }
                    var result = _loaiSachService.ThemExcel(listLoaiSach);
                    if (result == string.Empty)
                        return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }else
                return Json(new { status = TrangThai.ThatBai, message = Message.Nodata }, JsonRequestBehavior.AllowGet);
        }
            catch (Exception ex)
            {
                return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       
    }
}
