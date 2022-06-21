using DATA.Models;
using DATA.Repository;
using HANNAH_NEW_VERSION.Configs;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [AuthorizeUser(PhanQuyen.Admin)]
    public class DiaChiXuatBanController : Controller
    {
        private readonly IBaseRepository<DiaChiXuatBan> _baseRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDiaChiXuatBanService _diaChiXuatBanService;

        public DiaChiXuatBanController(IDiaChiXuatBanService diaChiXuatBanService, IBaseRepository<DiaChiXuatBan> baseRepository,
                                    IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
            _diaChiXuatBanService = diaChiXuatBanService;
        }
        public ActionResult DanhSachDiaChiXuatBan()
        {
            return View();
        }
        public ActionResult _DanhSachDiaChiXuatBan()
        {
            var diaChiXuatBan = _diaChiXuatBanService.DanhSachDiaChiXuatBan();
            return PartialView(diaChiXuatBan);
        }
        public ActionResult _ThemOrSuaDiaChiXuatBan(int? Id)
        {
            if (Id == null)
            {
                var diaChiXuatBan = new DiaChiXuatBan();
                return PartialView(diaChiXuatBan);
            }
            else
                return PartialView(_diaChiXuatBanService.LayDiaChiXuatBanTheoMa(Id.Value));

        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaDiaChiXuatBan(int? Id, DiaChiXuatBan diaChiXuatBan)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (Id == 0 || Id == null)
            {
                diaChiXuatBan.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _diaChiXuatBanService.ThemDiaChiXuatBan(diaChiXuatBan);
                var idDiaChi = _baseRepository.GetAll(p => p.MaDiaChi > 0).Max(p => p.MaDiaChi);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success , idDiaChi }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                diaChiXuatBan.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _diaChiXuatBanService.SuaDiaChiXuatBan(diaChiXuatBan);
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult XoaDiaChiXuatBan(int[] data)
        {
            var diaChiXuatBan = _diaChiXuatBanService.LayDanhSachMa(data);
            var result = _diaChiXuatBanService.XoaDiaChiXuatBan(diaChiXuatBan);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        //List<ExcelDiaChiXuatBan> DsThatbai = new List<ExcelDiaChiXuatBan>();
        //List<ExcelDiaChiXuatBan> DsThanhCong = new List<ExcelDiaChiXuatBan>();
        //public JsonResult ImportExcel(HttpPostedFileBase myExcelData)
        //{
        //    if (myExcelData != null)
        //    {
        //        try
        //        {
        //            if (myExcelData.ContentType == "application/vnd.ms-excel" || myExcelData.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //            {
        //                string FileName = "HannahExcel" + DateTime.Now.ToString("yyyyMMddHHmmss") + myExcelData.FileName;
        //                string SavePatch = Server.MapPath("~/Areas/Admin/Content/Excel/");//Path
        //                FileHelper.UploadFile(myExcelData, SavePatch, FileName);

        //                var pathToExcelFile = SavePatch + FileName;
        //                string sheetName = "Sheet1";

        //                var excelFile = new ExcelQueryFactory(pathToExcelFile);
        //                var dataList = from kh in excelFile.Worksheet<ExcelDiaChiXuatBan>(sheetName) select kh;
        //                if (dataList.Count() > 0)
        //                {
        //                    if (Session["DsThanhCong"] != null)
        //                    {
        //                        DsThanhCong.Clear();
        //                        Session["DsThanhCong"] = DsThanhCong;
        //                    }
        //                    if (Session["DsThatbai"] != null)
        //                    {
        //                        DsThatbai.Clear();
        //                        Session["DsThatbai"] = DsThatbai;
        //                    }
        //                    Session["DsThatbai"] = DsThatbai;
        //                    int i = 1;
        //                    foreach (var kh in dataList)
        //                    {
        //                        try
        //                        {
        //                            ExcelDiaChiXuatBan st = new ExcelDiaChiXuatBan();
        //                            st.Stt = i;
        //                            st.TenDiaChiXuatBan = kh.TenDiaChiXuatBan;
        //                            st.Mota = kh.Mota;
        //                            st.NoiDungSeo = kh.NoiDungSeo;
        //                            st.TuKhoaSeo = kh.TuKhoaSeo;
        //                            st.TieuDeSeo = kh.TieuDeSeo;
        //                            st.DuongDanSeo = kh.DuongDanSeo;
        //                            if (string.IsNullOrWhiteSpace(st.TenDiaChiXuatBan))
        //                            {
        //                                st.ThongBao = "Name of the book cannot be blank!";
        //                                DsThatbai.Add(st);
        //                            }
        //                            else
        //                            {
        //                                if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
        //                                {
        //                                    st.DuongDanSeo = AdminHelper.ToSeoUrl(st.TenDiaChiXuatBan);
        //                                    st.ThongBao = "New data added";
        //                                    DsThanhCong.Add(st);
        //                                }
        //                                else
        //                                {
        //                                    st.ThongBao = "New data added";
        //                                    DsThanhCong.Add(st);
        //                                }


        //                            }
        //                            Session["DsThatbai"] = DsThatbai;
        //                            Session["DsThanhCong"] = DsThanhCong;
        //                        }
        //                        catch (DbEntityValidationException ex)
        //                        {
        //                            return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //                        }
        //                        i++;
        //                    }
        //                }
        //                else
        //                {
        //                    return Json(new { status = TrangThai.ThatBai, message = Message.Success }, JsonRequestBehavior.AllowGet);
        //                }
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //        return Json(new { status = TrangThai.ThatBai, message = Message.Failure }, JsonRequestBehavior.AllowGet);

        //}
        //public ActionResult _DanhSachExcel()
        //{
        //    ViewBag.DsThanhCong = Session["DsThanhCong"];
        //    ViewBag.DsThatbai = Session["DsThatbai"];
        //    return PartialView();
        //}
        //public JsonResult AddListExcel()
        //{
        //    try
        //    {
        //        List<DiaChiXuatBan> listDiaChiXuatBan = new List<DiaChiXuatBan>();
        //        DsThanhCong = (List<ExcelDiaChiXuatBan>)Session["DsThanhCong"];
        //        if (DsThanhCong.Count != 0)
        //        {
        //            foreach (var item in DsThanhCong)
        //            {
        //                DiaChiXuatBan DiaChiXuatBan = new DiaChiXuatBan();
        //                DiaChiXuatBan.TenDiaChiXuatBan = item.TenDiaChiXuatBan;
        //                DiaChiXuatBan.MoTa = item.Mota;
        //                DiaChiXuatBan.NoiDungSeo = item.NoiDungSeo;
        //                DiaChiXuatBan.TuKhoaSeo = item.TuKhoaSeo;
        //                DiaChiXuatBan.TieuDeSeo = item.TieuDeSeo;
        //                DiaChiXuatBan.DuongDanSeo = item.DuongDanSeo;
        //                DiaChiXuatBan.NgayTao = DateTime.Now;
        //                DiaChiXuatBan.TrangThai = false;
        //                listDiaChiXuatBan.Add(DiaChiXuatBan);
        //            }
        //            var result = _DiaChiXuatBanService.ThemExcel(listDiaChiXuatBan);
        //            if (result == string.Empty)
        //                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
        //            else
        //                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(new { status = TrangThai.ThatBai, message = Message.Nodata }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { status = TrangThai.ThatBai, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


    }
}