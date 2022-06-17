using ClassLibrary1.MailHelper;
using DATA.Models;
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
    public class PhanHoiAdminController : Controller
    {
        private readonly IPhanHoiService _phanHoiService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICaiDatService _caiDatService;
        public PhanHoiAdminController(IPhanHoiService phanHoiService,
                                      IAuthenticationService authenticationService,
                                       ICaiDatService caiDatService)
        {
            _phanHoiService = phanHoiService;
            _authenticationService = authenticationService;
            _caiDatService = caiDatService;
        }
        public ActionResult DanhSachPhanHoi()
        {
            return View();
        }
        public ActionResult _DanhSachPhanHoi()
        {
            var phanHoi = _phanHoiService.DanhSachPhanHoi();
            return PartialView(phanHoi);
        }
        public ActionResult _ChiTietPhanHoi(int? Id)
        {
            return PartialView(_phanHoiService.LayPhanHoiTheoMa(Id.Value));
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult TraLoiPhanHoi(PhanHoi phanHoi)
        {
            var nguoiDung = _authenticationService.GetAuthenticatedUser();
            GuiMailPhanHoi(phanHoi.TenNguoiGui, phanHoi.Email, phanHoi.SDT, phanHoi.NoiDungXuLy);
            phanHoi.MaNguoiDung = nguoiDung.MaNguoiDung;
            if (phanHoi.TrangThai == TinhTrang.IsBlocked)
                phanHoi.TrangThai = TinhTrang.Activating;

            var result = _phanHoiService.ChiTietPhanHoi(phanHoi);

            if (result == string.Empty){
               
                return Json(new { status = TrangThai.ThanhCong, message = Message.Success }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = TrangThai.ThatBai, message = result }, JsonRequestBehavior.AllowGet);
        }
        public void GuiMailPhanHoi(string tenNguoiGui, string Email, string SDT, string noiDung)
        {
            var caiDat = _caiDatService.LayThongTinWeb();
            String strPathAndQuery = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string noiDungMail = System.IO.File.ReadAllText(Server.MapPath("~/Common/GuiPhanHoi.html"));
            noiDungMail = noiDungMail.Replace("{{TenWeb}}", strUrl);
            noiDungMail = noiDungMail.Replace("{{DuongDan}}", strUrl);
            noiDungMail = noiDungMail.Replace("{{NoiDung}}", noiDung);
            noiDungMail = noiDungMail.Replace("{{TenNguoiGui}}", tenNguoiGui);
            noiDungMail = noiDungMail.Replace("{{SDTNguoiGui}}", SDT);
            noiDungMail = noiDungMail.Replace("{{EmailNguoiGui}}", Email);
            noiDungMail = noiDungMail.Replace("{{HinhAnh}}", caiDat.Logo);
            noiDungMail = noiDungMail.Replace("{{TieuDe}}", "Liên hệ");
            noiDungMail = noiDungMail.Replace("{{SDT}}", caiDat.SDTLienHe);
            noiDungMail = noiDungMail.Replace("{{Email}}", caiDat.EmailGoiMail);
            noiDungMail = noiDungMail.Replace("{{DiaChi}}", caiDat.DiaChiCuaHang);
            new GuiMail().SendMailPhanHoi(caiDat.EmailGoiMail, caiDat.MatKhauEmail, "Phản Hồi", Email, "Nội dung phản hồi " + " Từ " + strUrl, noiDungMail);
            new GuiMail().SendMailPhanHoi(caiDat.EmailGoiMail, caiDat.MatKhauEmail, "Phản Hồi", caiDat.EmailGoiMail, "Nội dung phản hồi " + " Từ " + strUrl, noiDungMail);
        }
    }
}