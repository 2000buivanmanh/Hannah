using ClassLibrary1.Helper;
using DATA.Models;
using DMSS.ViewModals.DsExcelViewModal;
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
    [Authorize]
    public class NhaXuatBanController : Controller
    {
        // GET: Admin/NhaXuatBan

        private readonly IAuthenticationService _authenticationService;
        private readonly INhaXuatBanService _nhaXuatBanService;

        public NhaXuatBanController(INhaXuatBanService nhaXuatBanService,
                                    IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _nhaXuatBanService = nhaXuatBanService;
        }
        public ActionResult DanhSachNhaXuatBan()
        {
            return View();
        }
        public ActionResult _DanhSachNhaXuatBan()
        {
            var nhaXuatBan = _nhaXuatBanService.DanhSachNhaXuatBan();
            return PartialView(nhaXuatBan);
        }
        public ActionResult _ThemOrSuaNhaXuatBan(int? Id)
        {
            if (Id == null)
            {
                var nhaXuatBan = new NhaXuatBan();
                return PartialView(nhaXuatBan);
            }
            else
                return PartialView(_nhaXuatBanService.LayNhaXuatBanTheoMa(Id.Value));

        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaNhaXuatBan(int? Id, NhaXuatBan nhaXuatBan)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                nhaXuatBan.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _nhaXuatBanService.ThemNhaXuatBan(nhaXuatBan);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                nhaXuatBan.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _nhaXuatBanService.SuaNhaXuatBan(nhaXuatBan);
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
            var nhaXuatBan = _nhaXuatBanService.LayNhaXuatBanTheoMa(Id);
            nhaXuatBan.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (nhaXuatBan.TrangThai == TinhTrang.Activating)
                nhaXuatBan.TrangThai = TinhTrang.IsBlocked;
            else
                nhaXuatBan.TrangThai = TinhTrang.Activating;
            var result = _nhaXuatBanService.SuaNhaXuatBan(nhaXuatBan);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult XoaNhaXuatBan(int[] data)
        {
            var nhaXuatBan = _nhaXuatBanService.LayDanhSachMa(data);
            var result = _nhaXuatBanService.XoaNhaXuatBan(nhaXuatBan);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
        List<ExcelNhaXuatBan> DsThatbai = new List<ExcelNhaXuatBan>();
        List<ExcelNhaXuatBan> DsThanhCong = new List<ExcelNhaXuatBan>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelNhaXuatBan>(sheetName) select kh;
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
                                    ExcelNhaXuatBan st = new ExcelNhaXuatBan();
                                    st.Stt = i;
                                    st.TenNhaXuatBan = kh.TenNhaXuatBan;
                                    st.ThongTinNXB = kh.ThongTinNXB;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if ( string.IsNullOrWhiteSpace(st.TenNhaXuatBan))
                                    {
                                        st.ThongBao = "Imprint cannot be blank !";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                        {
                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenNhaXuatBan);
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
                List<NhaXuatBan> listNhaXuatBan = new List<NhaXuatBan>();
                DsThanhCong = (List<ExcelNhaXuatBan>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                    {
                        foreach (var item in DsThanhCong)
                    {
                        NhaXuatBan nhaXuatBan = new NhaXuatBan();
                        nhaXuatBan.TenNhaXuatBan = item.TenNhaXuatBan;
                        nhaXuatBan.ThongTinNXB = item.ThongTinNXB;
                        nhaXuatBan.NoiDungSeo = item.NoiDungSeo;
                        nhaXuatBan.TuKhoaSeo = item.TuKhoaSeo;
                        nhaXuatBan.TieuDeSeo = item.TieuDeSeo;
                        nhaXuatBan.DuongDanSeo = item.DuongDanSeo;
                        nhaXuatBan.NgayTao = DateTime.Now;
                        nhaXuatBan.TrangThai = false;
                        listNhaXuatBan.Add(nhaXuatBan);
                    }
                    var result = _nhaXuatBanService.ThemExcel(listNhaXuatBan);
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