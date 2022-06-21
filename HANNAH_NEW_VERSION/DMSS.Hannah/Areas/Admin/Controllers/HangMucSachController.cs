using ClassLibrary1.Helper;
using DATA.Models;
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
    public class HangMucSachController : Controller
    {
        // GET: Admin/HangMucSach
        private readonly IHangMucSachService _hangMucSachService;
        private readonly IAuthenticationService _authenticationService;
        public HangMucSachController(IHangMucSachService hangMucSachService,
                                     IAuthenticationService authenticationService)
        {
            _hangMucSachService = hangMucSachService;
            _authenticationService = authenticationService;
        }
        public ActionResult DanhSachHangMucSach()
        {
            return View();
        }
        public ActionResult _DanhSachHangMucSach()
        {
            var hangMucSach = _hangMucSachService.DanhSachHangMuc();
            return PartialView(hangMucSach);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaHangMuc(int? Id, HangMucSach hangMucSach)
        {

            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                if (_hangMucSachService.KiemTraTonTaiMaNhanDienHangMucSach(hangMucSach.MaNhanDienHangMucSach) != null)
                {
                    return Json(new { status = KiemTraTonTai.DaTonTai, message = "List of books already exists!" }, JsonRequestBehavior.AllowGet);
                }
                hangMucSach.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _hangMucSachService.ThemHangMucSach(hangMucSach);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                hangMucSach.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _hangMucSachService.CapNhatHangMucSach(hangMucSach);
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
            var hangMucSach = _hangMucSachService.LayHangMucSachTheoMa(Id);
            hangMucSach.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (hangMucSach.TrangThai == TinhTrang.Activating)
                hangMucSach.TrangThai = TinhTrang.IsBlocked;
            else
                hangMucSach.TrangThai = TinhTrang.Activating;
            var result = _hangMucSachService.CapNhatHangMucSach(hangMucSach);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ThemOrSuaHangMuc(int? Id)
        {
            if (Id == null)
            {
                var hangMucSach = new HangMucSach();
                return PartialView(hangMucSach);
            }
            else
                return PartialView(_hangMucSachService.LayHangMucSachTheoMa(Id.Value));
        }

        [HttpPost]
        public JsonResult XoaHangMuc(int[] data)
        {
            var hangMucSach = _hangMucSachService.LayDanhSachMa(data);
            var result = _hangMucSachService.XoaHangMucSach(hangMucSach);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int KiemTraTonTaiMaNhanDienHangMucSach(string bookcatalogcode)
        {
            if (_hangMucSachService.KiemTraTonTaiMaNhanDienHangMucSach(bookcatalogcode)==null)
                return 1;
            return 0;
        }


        List<ExcelHangMucSach> DsThatbai = new List<ExcelHangMucSach>();
        List<ExcelHangMucSach> DsThanhCong = new List<ExcelHangMucSach>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelHangMucSach>(sheetName) select kh;
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
                                    ExcelHangMucSach st = new ExcelHangMucSach();
                                    st.Stt = i;
                                    st.TenHangMuc = kh.TenHangMuc;
                                    st.MaNhanDienHangMucSach = kh.MaNhanDienHangMucSach;
                                    st.Mota = kh.Mota;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.MaNhanDienHangMucSach) || string.IsNullOrWhiteSpace(st.TenHangMuc))
                                    {
                                        st.ThongBao = "Information cannot be blank!";
                                        DsThatbai.Add(st);
                                    }
                                    else{
                                        if (KiemTraTonTaiMaNhanDienHangMucSach(st.MaNhanDienHangMucSach) == 1)
                                        {
                                            st.ThongBao = "This Book Catalog Code is taken already !";
                                            DsThatbai.Add(st);
                                        }
                                        else
                                        {

                                            if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                            {
                                                st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenHangMuc);
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
                List<HangMucSach> listHangMucSach = new List<HangMucSach>();
                DsThanhCong = (List<ExcelHangMucSach>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {
                        foreach (var item in DsThanhCong)
                    {
                        HangMucSach HangMucSach = new HangMucSach();
                        HangMucSach.TenHangMuc = item.TenHangMuc;
                        HangMucSach.MaNhanDienHangMucSach = item.MaNhanDienHangMucSach;
                        HangMucSach.MoTa = item.Mota;
                        HangMucSach.NoiDungSeo = item.NoiDungSeo;
                        HangMucSach.TuKhoaSeo = item.TuKhoaSeo;
                        HangMucSach.TieuDeSeo = item.TieuDeSeo;
                        HangMucSach.DuongDanSeo = item.DuongDanSeo;
                        HangMucSach.NgayTao = DateTime.Now;
                        HangMucSach.NguoiTao = nguoiDung.MaNguoiDung;
                        HangMucSach.TrangThai = false;
                        listHangMucSach.Add(HangMucSach);
                    }
                    var result = _hangMucSachService.ThemExcel(listHangMucSach);
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