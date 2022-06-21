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
    public class NhomTuoiController : Controller
    {
        private readonly IBaseRepository<NhomTuoi> _baseRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly INhomTuoiService _nhomTuoiService;

        public NhomTuoiController(INhomTuoiService nhomTuoiService, IBaseRepository<NhomTuoi> baseRepository,
                                    IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
            _nhomTuoiService = nhomTuoiService;
        }
        public ActionResult DanhSachNhomTuoi()
        {
            return View();
        }
        public ActionResult _DanhSachNhomTuoi()
        {
            var nhomTuoi = _nhomTuoiService.DanhSachNhomTuoi();
            return PartialView(nhomTuoi);
        }
        public ActionResult _ThemOrSuaNhomTuoi(int? Id)
        {
            if (Id == null)
            {
                var nhomTuoi = new NhomTuoi();
                return PartialView(nhomTuoi);
            }
            else
                return PartialView(_nhomTuoiService.LayNhomTuoiTheoMa(Id.Value));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ThemHoacSuaNhomTuoi(int? Id, NhomTuoi nhomTuoi)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            if (_nhomTuoiService.KiemTraDoTuoi(nhomTuoi.DoTuoiMin , nhomTuoi.DoTuoiMax) != null)
            {
                return Json(new { status = KiemTraTonTai.DaTonTai, message = "Age group already exists!" }, JsonRequestBehavior.AllowGet);
            }

            if (Id == 0 || Id == null)
            {
                nhomTuoi.NguoiTao = nguoiDung.MaNguoiDung;
                var result = _nhomTuoiService.ThemNhomTuoi(nhomTuoi);
                var idNhomTuoi = _baseRepository.GetAll(p => p.MaNhomTuoi > 0).Max(p => p.MaNhomTuoi);
                if (result == string.Empty)

                    return Json(new { status = TrangThai.ThanhCong, message = Message.Success, idNhomTuoi }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                nhomTuoi.QTVCapNhat = nguoiDung.MaNguoiDung;
                var result = _nhomTuoiService.SuaNhomTuoi(nhomTuoi);
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
            var nhomTuoi = _nhomTuoiService.LayNhomTuoiTheoMa(Id);
                nhomTuoi.QTVCapNhat = nguoiDung.MaNguoiDung;
            if (nhomTuoi.TrangThai == TinhTrang.Activating)
                nhomTuoi.TrangThai = TinhTrang.IsBlocked;
            else
                nhomTuoi.TrangThai = TinhTrang.Activating;
            var result = _nhomTuoiService.SuaNhomTuoi(nhomTuoi);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult XoaNhomTuoi(int[] data)
        {
            var nhomTuoi = _nhomTuoiService.LayDanhSachMa(data);
            var result = _nhomTuoiService.XoaNhomTuoi(nhomTuoi);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThanhCong, message = result }, JsonRequestBehavior.AllowGet);
        }
        List<ExcelNhomTuoi> DsThatbai = new List<ExcelNhomTuoi>();
        List<ExcelNhomTuoi> DsThanhCong = new List<ExcelNhomTuoi>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelNhomTuoi>(sheetName) select kh;
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
                                ExcelNhomTuoi st = new ExcelNhomTuoi();
                                    st.Stt = i;
                                    st.DoTuoiMin =  kh.DoTuoiMin;
                                    st.DoTuoiMax =  kh.DoTuoiMax;
                                    st.Mota = kh.Mota;
                                    st.NoiDungSeo = kh.NoiDungSeo;
                                    st.TuKhoaSeo = kh.TuKhoaSeo;
                                    st.TieuDeSeo = kh.TieuDeSeo;
                                    st.DuongDanSeo = kh.DuongDanSeo;
                                    if (string.IsNullOrWhiteSpace(st.DoTuoiMin) || string.IsNullOrWhiteSpace(st.DoTuoiMax)) {
                                        st.ThongBao = "Information cannot be blank!";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (IsNumber(st.DoTuoiMin) == false || IsNumber(st.DoTuoiMax) == false)
                                        {
                                            st.ThongBao = "Age must enter number !";
                                            DsThatbai.Add(st);
                                        }
                                        else
                                        {
                                            if (Int32.Parse(st.DoTuoiMin) < 0 || Int32.Parse(st.DoTuoiMax) < 0)
                                            {
                                                st.ThongBao = "Age cannot enter a negative number !";
                                                DsThatbai.Add(st);
                                            }
                                            else
                                            {
                                                if (Int32.Parse(st.DoTuoiMin) > Int32.Parse(st.DoTuoiMax))
                                                {
                                                    st.ThongBao = "The minimum age must not be greater than the maximum age !";
                                                    DsThatbai.Add(st);
                                                }
                                                else
                                                {
                                                    if (_nhomTuoiService.KiemTraDoTuoi(Int32.Parse(st.DoTuoiMin), Int32.Parse(st.DoTuoiMax)) != null)
                                                    {
                                                        st.ThongBao = "Age group already exists!";
                                                        DsThatbai.Add(st);
                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrWhiteSpace(st.DuongDanSeo))
                                                        {
                                                            st.DuongDanSeo = AdminHelper.ToSeoUrl(st.DoTuoiMin + " " + st.DoTuoiMax + " " + "years old");
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
                List<NhomTuoi> listNhomTuoi = new List<NhomTuoi>();
                DsThanhCong = (List<ExcelNhomTuoi>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {
                    foreach (var item in DsThanhCong)
                    {
                        NhomTuoi NhomTuoi = new NhomTuoi();
                        NhomTuoi.DoTuoiMin = int.Parse(item.DoTuoiMin);
                        NhomTuoi.DoTuoiMax = int.Parse(item.DoTuoiMax);
                        NhomTuoi.NoiDungSeo = item.NoiDungSeo;
                        NhomTuoi.TuKhoaSeo = item.TuKhoaSeo;
                        NhomTuoi.TieuDeSeo = item.TieuDeSeo;
                        NhomTuoi.DuongDanSeo = item.DuongDanSeo;
                        NhomTuoi.NgayTao = DateTime.Now;
                        NhomTuoi.TrangThai = false;
                        listNhomTuoi.Add(NhomTuoi);


                    }
                    var result = _nhomTuoiService.ThemExcel(listNhomTuoi);
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