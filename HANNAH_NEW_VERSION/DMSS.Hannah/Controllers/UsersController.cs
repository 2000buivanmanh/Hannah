using ClassLibrary1.Helper;
using ClassLibrary1.MaHoa;
using ClassLibrary1.MailHelper;
using ClassLibrary1.Ramdom;
using DATA.Models;
using DMSS.ViewModals.BookCase;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Controllers
{
    public class UsersController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;
        private readonly ICaiDatService _caiDatService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDatSachService _datSachService;
        private readonly IChiTietDatSachService _chiTietDatSachService;
        private readonly ISachService _sachService;


        public UsersController(INguoiDungService nguoiDungService,
                                   ICaiDatService caiDatService,
                                   IAuthenticationService authenticationService,
                                   IDatSachService datSachService,
                                   IChiTietDatSachService chiTietDatSachService,
                                   ISachService sachService)
        {
            _nguoiDungService = nguoiDungService;
            _caiDatService = caiDatService;
            _authenticationService = authenticationService;
            _datSachService = datSachService;
            _chiTietDatSachService = chiTietDatSachService;
            _sachService = sachService;
        }

        public ActionResult _Login()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult _UserInformation()
        {
            return PartialView(_authenticationService.GetAuthenticatedUser());
        }

        public ActionResult _Register()
        {
            return PartialView();
        }
        public ActionResult _LostPassword()
        {
            return PartialView();
        }
        public ActionResult _ConfirmAccount()
        {
            return PartialView();
        }
        public ActionResult WaitAccount()
        {
            return View();
        }
        [Authorize]
        public ActionResult _Profile()
        {
            var thongTin = _authenticationService.GetAuthenticatedUser();
            return PartialView(thongTin);
        }

        public ActionResult BookLoanHistori()
        {
            var thongTin = _authenticationService.GetAuthenticatedUser();
            var datSach = _datSachService.DanhSachDatSach().Where(s => s.MaNguoiDung == thongTin.MaNguoiDung).ToList();
            return View(datSach);
        }

        [HttpPost]
        public JsonResult HuyDon(int maDonHang)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            var donHang = _datSachService.LayDonDatSachTheoMaDonSach(maDonHang);
            var chitietdatsach = _chiTietDatSachService.LayChiTietDatSachTheoMa(maDonHang);
            if (donHang.TinhTrangDonHang == TinhTrangDonHang.DangDat)
            {
                foreach (var item in chitietdatsach)
                {
                    var sach = _sachService.LayMaSach((int)item.MaSach);
                    nguoiDung.DiemTichLuy = (int?)(sach.Gia + nguoiDung.DiemTichLuy);
                }
                donHang.TinhTrangDonHang = TinhTrangDonHang.DaHuy;
                _nguoiDungService.CapNhatNguoiDung(nguoiDung);
            }
            var result = _datSachService.CapNhatDonDatSach(donHang);
            if (result)
            {
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = TrangThai.ThatBai }, JsonRequestBehavior.AllowGet);
            }
        } 


        [HttpGet]
        public JsonResult KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            var nguoiDung = _nguoiDungService.KiemTraTenDangNhap(tenDangNhap);
            if (nguoiDung != null)
            {
                matKhau = MaHoaMD5.MaHoa(matKhau);
                var thongTinCaiDat = _caiDatService.LayThongTinWeb();
                if (nguoiDung.MatKhau == matKhau)
                {
                    if (nguoiDung.VaiTro == PhanQuyen.Admin)
                    {
                        _authenticationService.DangNhap(nguoiDung, true);
                        return Json(new { status = KiemTraLogin.DangNhapThanhCong, isAdmin = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (nguoiDung.TrangThai == TrangThaiTaiKhoan.DaXacNhanMail || nguoiDung.TrangThai == TrangThaiTaiKhoan.DaDuyet)
                        {
                            if (nguoiDung.TinhTrangNguoiDung == TinhTrangNguoiDung.BiKhoa || nguoiDung.NgayHetHan < DateTime.Now)
                            {
                                return Json(new { status = KiemTraLogin.DangnhapThatBai, message = Message.TaiKhoanBiKhoa }, JsonRequestBehavior.AllowGet);
                            }
                            else if (nguoiDung.TrangThai == TrangThaiTaiKhoan.DaDuyet)
                            {
                                nguoiDung.NgayHetHan = DateTime.Now.AddMonths(thongTinCaiDat.ThoiGianHetHanTaiKhoan);
                                nguoiDung.LanHoatDongCuoi = DateTime.Now;
                                _nguoiDungService.CapNhatNguoiDung(nguoiDung);
                                _authenticationService.DangNhap(nguoiDung, true);
                                return Json(new { status = KiemTraLogin.DangNhapThanhCong, isAdmin = false }, JsonRequestBehavior.AllowGet);
                            }

                            return Json(new { status = KiemTraLogin.ChuaDuyetMail }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (nguoiDung.SoLanGuiMa >= thongTinCaiDat.SoLanGuiMa)
                            {
                                return Json(new { status = TrangThai.ThatBai, capcha = false, message = Message.QuaSoLanGuiMa }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                nguoiDung.Capcha = RandomCapcha.RandomString(6);
                                nguoiDung.SoLanGuiMa += 1;
                                nguoiDung.ThoiHanCapcha = DateTime.Now.AddMinutes(thongTinCaiDat.ThoiHanMaXacNhan);
                                GuiMailMaXacNhan(nguoiDung.HoTen, nguoiDung.Capcha, nguoiDung.EmailNguoiDung);
                                _nguoiDungService.CapNhatNguoiDung(nguoiDung);
                                return Json(new { status = KiemTraLogin.ChuaXacNhanMail }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }
                else
                {
                    return Json(new { status = KiemTraLogin.DangnhapThatBai, message = Message.SaiMatKhau }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = KiemTraLogin.DangnhapThatBai, message = Message.SaiTaiKhoan }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult LayLaiMatKhau(string tenDangNhap)
        {
            var nguoiDung = _nguoiDungService.KiemTraTenDangNhap(tenDangNhap);
            if (nguoiDung == null)
                return Json(new { status = TrangThai.ThatBai, message = Message.Failure }, JsonRequestBehavior.AllowGet);
            string matkhau = RandomCapcha.RandomString(6);
            string matKhauMoi = MaHoaMD5.MaHoa(matkhau);
            nguoiDung.MatKhau = matKhauMoi;
            _nguoiDungService.CapNhatNguoiDung(nguoiDung);
            GuiMailMaXacNhan(nguoiDung.HoTen, matkhau, nguoiDung.EmailNguoiDung);
            return Json(new { status = TrangThai.ThanhCong, message = Message.Success}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult XacNhanMail(string tenDangNhap, string maXacNhan)
        {
            var thongTinCaiDat = _caiDatService.LayThongTinWeb();
            var nguoiDung = _nguoiDungService.KiemTraTenDangNhap(tenDangNhap);
            if (nguoiDung.TrangThai == TrangThaiTaiKhoan.ChuaXacNhanMail)
            {
                if (nguoiDung.Capcha == maXacNhan)
                {
                    nguoiDung.NgayHetHan = DateTime.Now.AddMonths(thongTinCaiDat.ThoiGianHetHanTaiKhoan);
                    nguoiDung.LanHoatDongCuoi = DateTime.Now;
                    nguoiDung.TrangThai = TrangThaiTaiKhoan.DaXacNhanMail;
                    _nguoiDungService.CapNhatNguoiDung(nguoiDung);
                    return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = TrangThai.ThatBai, message = Message.SaiMaCapCha }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = TrangThai.ThatBai, message = Message.LoiTaiLaiTrang }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GuiMaXacNhan(string tenDangNhap)
        {
            var nguoiDung = _nguoiDungService.KiemTraTenDangNhap(tenDangNhap);
            var thongTinWeb = _caiDatService.LayThongTinWeb();
            if (nguoiDung.SoLanGuiMa >= thongTinWeb.SoLanGuiMa)
            {
                return Json(new { status = TrangThai.ThatBai, message = Message.QuaSoLanGuiMa }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                nguoiDung.SoLanGuiMa += 1;
                nguoiDung.ThoiHanCapcha = DateTime.Now.AddMinutes(thongTinWeb.ThoiHanMaXacNhan);
                nguoiDung.Capcha = RandomCapcha.RandomString(6);
                GuiMailMaXacNhan(nguoiDung.HoTen, nguoiDung.Capcha, nguoiDung.EmailNguoiDung);
                _nguoiDungService.CapNhatNguoiDung(nguoiDung);
                return Json(new { status = TrangThai.ThanhCong }, JsonRequestBehavior.AllowGet);
            }
        }
        public void GuiMailMaXacNhan(string HoTen, string Capcha, string EmailNguoiDung)
        {
            var thongTinWeb = _caiDatService.LayThongTinWeb();
            string noiDungMail = System.IO.File.ReadAllText(Server.MapPath("~/Common/GuiMaCapCha.html"));
            noiDungMail = noiDungMail.Replace("{{HoTen}}", HoTen);
            noiDungMail = noiDungMail.Replace("{{Capcha}}", Capcha);
            new GuiMail().SendMailMaCapCha(thongTinWeb.EmailGoiMail, thongTinWeb.MatKhauEmail, EmailNguoiDung, noiDungMail);
        }
        private BookCaseViewModal KeSach = new BookCaseViewModal();
        public ActionResult DangXuat()
        {
            _authenticationService.DangXuat();
            return Redirect("/");
        }
        [HttpPost]
        public int KiemTraTonTaiEmail(string email)
        {
            if (_nguoiDungService.KiemTraTonTaiEmail(email))
                return 1;
            return 0;
        }
        [HttpPost]
        public int KiemTraTonTaiTenNguoiDung(string username)
        {
            if (_nguoiDungService.KiemTraTonTaiTenNguoiDung(username))
                return 1;
            return 0;
        }
        [HttpPost]
        public JsonResult ThemNguoiDung(NguoiDung nguoiDung, string matKhau)
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
            nguoiDung.MatKhau = MaHoaMD5.MaHoa(matKhau);
            nguoiDung.TrangThai = 0;
            nguoiDung.TinhTrangNguoiDung = true;
            nguoiDung.VaiTro = 1;
            nguoiDung.NgayTao = DateTime.Now;
            nguoiDung.NgayHetHan = DateTime.Now.AddMonths(caiDat.ThoiGianHetHanTaiKhoan);
            nguoiDung.Capcha = RandomCapcha.RandomString(6);
            nguoiDung.ThoiHanCapcha = DateTime.Now.AddMinutes(caiDat.ThoiHanMaXacNhan);
            nguoiDung.SoLanGuiMa = 0;

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

            //gửi mail mã xác nhận
            GuiMailMaXacNhan(nguoiDung.HoTen, nguoiDung.Capcha, nguoiDung.EmailNguoiDung);

            return Json(new { status = KiemTraDangKy.DangKyThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadAvatar(HttpPostedFileBase uploadedImage)
        {
            var user = _authenticationService.GetAuthenticatedUser();
            if (uploadedImage != null)
            {
                string FileExtension = System.IO.Path.GetExtension(uploadedImage.FileName);
                string[] validFileTypes = { ".jpg", ".png", ".jpge" };

                if (validFileTypes.Contains(FileExtension))
                {
                    var MaxSize = ConfigHelper.GetConFig("MaxSizeUpload") != "" ? Int32.Parse(ConfigHelper.GetConFig("MaxSizeUpload")) : 5242880;
                    if (uploadedImage.ContentLength < MaxSize)
                    {
                        string FileName = user.TenDangNhap.Trim() + ".png";
                        string SavePatch = Server.MapPath("~/Content/Images/NguoiDung");//Path
                        var result = ImageHelper.UploadImage(uploadedImage, SavePatch, FileName);
                        if (result == string.Empty)
                            return Json(new { status = TrangThai.ThanhCong, message = Message.UploadFileThanhCong }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string maxSize = (MaxSize / 1024).ToString();
                        return Json(new { status = TrangThai.ThatBai, message = string.Format(Message.FileTooBig, maxSize) }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { status = TrangThai.ThatBai, message = Message.FileNotSupported }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = TrangThai.ThatBai, message = Message.FileNotFound }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CapNhatNguoiDung(NguoiDung nguoiDung)
        {
            var thongTin = _authenticationService.GetAuthenticatedUser();
            if (!string.IsNullOrEmpty(nguoiDung.MatKhau))
            {
                thongTin.MatKhau = MaHoaMD5.MaHoa(nguoiDung.MatKhau);
            }
            thongTin.HoTen = nguoiDung.HoTen;
            thongTin.DiaChi = nguoiDung.DiaChi;
            thongTin.NgaySinh = nguoiDung.NgaySinh;
            thongTin.GioiThieu = nguoiDung.GioiThieu;
            thongTin.CongKhaiThongTin = nguoiDung.CongKhaiThongTin;
            var result = _nguoiDungService.CapNhatNguoiDung(thongTin);
            if (result == string.Empty)
                return Json(new
                {
                    status = TrangThai.ThanhCong,
                    message = Message.Updated,
                    alerticon = AlertIcon.Success
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new
                {
                    status = TrangThai.ThatBai,
                    message = result,
                    alerticon = AlertIcon.Error
                }, JsonRequestBehavior.AllowGet);
        }
    }
}