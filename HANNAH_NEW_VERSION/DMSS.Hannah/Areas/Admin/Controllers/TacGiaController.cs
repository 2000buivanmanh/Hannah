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
    public class TacGiaController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITacGiaService _tacGiaService;
        private readonly IBaseRepository<TacGia> _baseRepository;

        public TacGiaController(ITacGiaService tacGiaService, IBaseRepository<TacGia> baseRepository,
                                    IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _tacGiaService = tacGiaService;
            _baseRepository = baseRepository;
        }
        public ActionResult DanhSachTacGia()
        {
            return View();
        }
        public ActionResult _DanhSachTacGia()
        {
            var TacGia = _tacGiaService.DanhSachTacGia();
            return PartialView(TacGia);
        }
        public ActionResult _ThemOrSuaTacGia(int? Id)
        {
            if (Id == null)
            {
                var tacGia = new TacGia();
                return PartialView(tacGia);
            }
            else
                return PartialView(_tacGiaService.LayTacGiaTheoMa(Id.Value));

        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaTacGia(int? Id, TacGia tacGia)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                tacGia.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _tacGiaService.ThemTacGia(tacGia);
                var idTacGia = _baseRepository.GetAll(p => p.MaTacGia > 0).Max(p => p.MaTacGia);
                
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success , idTacGia }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tacGia.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _tacGiaService.SuaTacGia(tacGia);
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
            var tacGia = _tacGiaService.LayTacGiaTheoMa(Id);
            tacGia.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (tacGia.TrangThai == TinhTrang.Activating)
                tacGia.TrangThai = TinhTrang.IsBlocked;
            else
                tacGia.TrangThai = TinhTrang.Activating;
            var result = _tacGiaService.SuaTacGia(tacGia);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult XoaTacGia(int[] data)
        {
            var tacGia = _tacGiaService.LayDanhSachMa(data);
            var result = _tacGiaService.XoaTacGia(tacGia);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
        List<ExcelTacGia> DsThatbai = new List<ExcelTacGia>();
        List<ExcelTacGia> DsThanhCong = new List<ExcelTacGia>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelTacGia>(sheetName) select kh;
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
                                    ExcelTacGia st = new ExcelTacGia();
                                    st.Stt = i;
                                    st.TenTacGia = kh.TenTacGia;
                                    st.ThongTinTacGia = kh.ThongTinTacGia;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.TenTacGia))
                                    {
                                        st.ThongBao = "Information cannot be blank !";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {

                                        if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                        {
                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenTacGia);
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
                    return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            }

            else
                return Json(new { status = TrangThai.ThatBai, message = Message.Failure }, JsonRequestBehavior.AllowGet);

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
                List<TacGia> listTacGia = new List<TacGia>();
                DsThanhCong = (List<ExcelTacGia>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {
                        foreach (var item in DsThanhCong)
                    {
                        TacGia tacGia = new TacGia();
                        tacGia.TenTacGia = item.TenTacGia;
                        tacGia.ThongTinTacGia = item.ThongTinTacGia;
                        tacGia.NoiDungSeo = item.NoiDungSeo;
                        tacGia.TuKhoaSeo = item.TuKhoaSeo;
                        tacGia.TieuDeSeo = item.TieuDeSeo;
                        tacGia.DuongDanSeo = item.DuongDanSeo;
                        tacGia.NgayTao = DateTime.Now;
                        tacGia.NguoiTao = nguoiDung.MaNguoiDung;
                        tacGia.TrangThai = false;
                        listTacGia.Add(tacGia);
                    }
                    var result = _tacGiaService.ThemExcel(listTacGia);
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

    }
}