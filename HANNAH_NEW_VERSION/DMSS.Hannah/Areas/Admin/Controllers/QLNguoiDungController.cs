using ClassLibrary1.Helper;
using ClassLibrary1.MaHoa;
using DATA.Models;
using DMSS.ViewModals.DsExcelViewModal;
using LinqToExcel;
using SERVICE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    [Authorize]
    public class QLNguoiDungController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;
        private readonly ICaiDatService _caiDatService;
        private readonly IAuthenticationService _authenticationService;


        public QLNguoiDungController(INguoiDungService nguoiDungService,
                                   ICaiDatService caiDatService,
                                   IAuthenticationService authenticationService)
        {
            _nguoiDungService = nguoiDungService;
            _caiDatService = caiDatService;
            _authenticationService = authenticationService;
        }

        public ActionResult DanhSachNguoiDung()
        {
            return View();
        }
        public ActionResult _DanhSachNguoiDung()
        {
            var nguoiDung = _nguoiDungService.DanhSachNguoiDung();
            return PartialView(nguoiDung);
        }
        public ActionResult _ThemNguoiDung()
        {
            return PartialView();
        }
        public ActionResult _ThongTinAdmin()
        {
            return PartialView(_authenticationService.GetAuthenticatedUser());
        }

        public ActionResult _ChiTietAdmin()
        {
            var thongTin = _authenticationService.GetAuthenticatedUser();
            return PartialView(thongTin);
        }
        public ActionResult DangXuat()
        {
            _authenticationService.DangXuat();
            return Redirect("/");
        }
        public ActionResult _ChiTietNguoiDung(int? Id)
        {
            return PartialView(_nguoiDungService.LayMaNGuoiDung(Id.Value));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult CapNhatAdmin(int? Id, NguoiDung nguoiDung)
        {

            if (!string.IsNullOrEmpty(nguoiDung.MatKhau))
            {
                nguoiDung.MatKhau = MaHoaMD5.MaHoa(nguoiDung.MatKhau);
            }
            var result = _nguoiDungService.CapNhatAdmin(nguoiDung);
                if (result == string.Empty)
                    return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public JsonResult CapNhatTinhTrang(int Id)
        {
            var nguoiDung = _nguoiDungService.LayMaNGuoiDung(Id);
            if (nguoiDung.TinhTrangNguoiDung == TinhTrangNguoiDung.BiKhoa)
                nguoiDung.TinhTrangNguoiDung = TinhTrangNguoiDung.DangHoatDong;
            else
                nguoiDung.TinhTrangNguoiDung = TinhTrangNguoiDung.BiKhoa;
            var result = _nguoiDungService.TinhTrangNguoiDung(nguoiDung);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DuyetTrangThai(int Id)
        {
            var nguoiDung = _nguoiDungService.LayMaNGuoiDung(Id);
            nguoiDung.TrangThai = TrangThaiTaiKhoan.DaDuyet;
            var result = _nguoiDungService.DuyetTrangThai(nguoiDung);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Updated }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ThemNguoiDung(NguoiDung nguoiDung)
        {
            if (_nguoiDungService.KiemTraTonTaiEmail(nguoiDung.EmailNguoiDung) == false)
            {
                return Json(new { status = KiemTraTonTai.DaTonTai, message = "Email already exists!" }, JsonRequestBehavior.AllowGet);
            }
            if (_nguoiDungService.KiemTraTonTaiTenNguoiDung(nguoiDung.TenDangNhap) == false)
            {
                return Json(new { status = KiemTraTonTai.DaTonTai, message = "Username already exists!" }, JsonRequestBehavior.AllowGet);
            }
            var caiDat = _caiDatService.LayThongTinWeb();
            string Patch = Server.MapPath("~/Content/Images/NguoiDung/" + nguoiDung.TenDangNhap) + ".png";
            nguoiDung.AnhDaiDien = "/Content/Images/NguoiDung/" + nguoiDung.TenDangNhap + ".png";
            nguoiDung.DiemTichLuy = caiDat.DiemTichLuy;
            nguoiDung.CongKhaiThongTin = false;
            nguoiDung.MatKhau = MaHoaMD5.MaHoa("12345");
            nguoiDung.TrangThai = 2;
            nguoiDung.TinhTrangNguoiDung = true;
            nguoiDung.VaiTro = 0;
            nguoiDung.NgayTao = DateTime.Now;
            nguoiDung.NgayHetHan = DateTime.Now.AddMonths(caiDat.ThoiGianHetHanTaiKhoan);


            //gen avatar
            var HoTenAr = nguoiDung.HoTen.Split(new char[0]);
            string FName = "";
            string LName = "";
            if (HoTenAr.Length > 0)
            {
                FName = HoTenAr[0][0].ToString();
                LName = HoTenAr[HoTenAr.Length - 1][0].ToString();
            }
            else
            {
                FName = nguoiDung.HoTen[0].ToString();
                LName = nguoiDung.HoTen[1].ToString();
            }
            ImageHelper.GenAvatar(FName, LName, Patch);
            _nguoiDungService.ThemNguoiDung(nguoiDung);

            return Json(new { status = KiemTraDangKy.DangKyThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
        }

        List<ExcelNguoiDung> DsThatbai = new List<ExcelNguoiDung>();
        List<ExcelNguoiDung> DsThanhCong = new List<ExcelNguoiDung>();
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
                        var dataList = from kh in excelFile.Worksheet<ExcelNguoiDung>(sheetName) select kh;
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
                                    ExcelNguoiDung st = new ExcelNguoiDung();
                                    st.Stt = i;
                                    st.TenDangNhap = kh.TenDangNhap;
                                    st.HoTen = kh.HoTen;
                                    st.EmailNguoiDung = kh.EmailNguoiDung;
                                    if (string.IsNullOrWhiteSpace(st.TenDangNhap) || string.IsNullOrWhiteSpace(st.HoTen) || string.IsNullOrWhiteSpace(st.EmailNguoiDung))
                                    {
                                        st.ThongBao = "Information cannot be blank !";
                                        DsThatbai.Add(st);
                                    }
                                    else
                                    {
                                        if (_nguoiDungService.KiemTraTonTaiEmail(st.EmailNguoiDung) == false)
                                        {
                                            st.ThongBao = "Email already exists!";
                                            DsThatbai.Add(st);
                                        }
                                        else
                                        {
                                            if (_nguoiDungService.KiemTraTonTaiTenNguoiDung(st.TenDangNhap) == false)
                                            {
                                                st.ThongBao = "Username already exists!";
                                                DsThatbai.Add(st);
                                            }
                                            else
                                            {
                                                if (validMail(st.EmailNguoiDung) == false)
                                                {
                                                    st.ThongBao = "Invalid email !";
                                                    DsThatbai.Add(st);
                                                }
                                                else
                                                {
                                                    st.ThongBao = "New data added";
                                                    DsThanhCong.Add(st);
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
                List<NguoiDung> listNguoiDung = new List<NguoiDung>();
                DsThanhCong = (List<ExcelNguoiDung>)Session["DsThanhCong"];
                if (DsThanhCong.Count != 0)
                {
                    foreach (var item in DsThanhCong)
                    {
                        NguoiDung nguoiDung = new NguoiDung();
                        nguoiDung.TenDangNhap = item.TenDangNhap;
                        nguoiDung.HoTen = item.HoTen;
                        nguoiDung.EmailNguoiDung = item.EmailNguoiDung;
                        var caiDat = _caiDatService.LayThongTinWeb();
                        string Patch = Server.MapPath("~/Content/Images/NguoiDung/" + nguoiDung.TenDangNhap) + ".png";
                        nguoiDung.AnhDaiDien = "/Content/Images/NguoiDung/" + nguoiDung.TenDangNhap + ".png";
                        nguoiDung.DiemTichLuy = caiDat.DiemTichLuy;
                        nguoiDung.CongKhaiThongTin = false;
                        nguoiDung.MatKhau = MaHoaMD5.MaHoa("12345");
                        nguoiDung.TrangThai = 2;
                        nguoiDung.TinhTrangNguoiDung = true;
                        nguoiDung.VaiTro = 0;
                        nguoiDung.NgayTao = DateTime.Now;
                        nguoiDung.NgayHetHan = DateTime.Now.AddMonths(caiDat.ThoiGianHetHanTaiKhoan);


                        //gen avatar
                        var HoTenAr = nguoiDung.HoTen.Split(new char[0]);
                        string FName = "";
                        string LName = "";
                        if (HoTenAr.Length > 0)
                        {
                            FName = HoTenAr[0][0].ToString();
                            LName = HoTenAr[HoTenAr.Length - 1][0].ToString();
                        }
                        else
                        {
                            FName = nguoiDung.HoTen[0].ToString();
                            LName = nguoiDung.HoTen[1].ToString();
                        }
                        ImageHelper.GenAvatar(FName, LName, Patch);
                        listNguoiDung.Add(nguoiDung);
                    }
                    var result = _nguoiDungService.ThemExcel(listNguoiDung);
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

        public JsonResult ResetPass(int[] data)
        {
            List<NguoiDung> listNguoiDung = new List<NguoiDung>();
            var dsNguoiDung = _nguoiDungService.LayDanhSachMa(data);
            foreach(var item in dsNguoiDung)
            {
                var nguoiDung = _nguoiDungService.LayMaNGuoiDung(item.MaNguoiDung);
                nguoiDung.MatKhau = MaHoaMD5.MaHoa("12345");
                listNguoiDung.Add(nguoiDung);
            }

            var result = _nguoiDungService.UpdateList(listNguoiDung);
            if (result == string.Empty)
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }

        private bool validMail(string address)
        {
            EmailAddressAttribute e = new EmailAddressAttribute();
            if (e.IsValid(address))
                return true;
            else
                return false;
        }

    }
}