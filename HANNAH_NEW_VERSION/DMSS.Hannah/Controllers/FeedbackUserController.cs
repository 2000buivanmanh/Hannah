using ClassLibrary1.MailHelper;
using DATA.Models;
using SERVICE;
using System;
using System.Web.Mvc;
using static DATA.Constant.Constant;

namespace HANNAH_NEW_VERSION.Controllers
{
    public class FeedbackUserController : Controller
    {
        private readonly IPhanHoiService _phanHoiService;
        private readonly IThongBaoService _thongBaoService;
        private readonly ICaiDatService _caiDatService;

        public FeedbackUserController(IPhanHoiService phanHoiService,
                                          IThongBaoService thongBaoService,
                                          ICaiDatService caiDatService)
        {
            _phanHoiService = phanHoiService;
            _thongBaoService = thongBaoService;
            _caiDatService = caiDatService;
        }
      
        public ActionResult FeedbackUser()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GuiPhanHoi(string tenNguoiGui, string Email, string SDT, string noiDung)
        {

            var ph = new PhanHoi();
            ph.TenNguoiGui = tenNguoiGui;
            ph.Email = Email;
            ph.SDT = SDT;
            ph.NoiDungPhanHoi = noiDung;
            ph.NgayGui = DateTime.Now;
            ph.TrangThai = false;
            var result = _phanHoiService.GuiPhanHoi(ph);

            var thongBao = new ThongBao();
            thongBao.MaPhanHoi = ph.MaPhanHoi;
            thongBao.NgayHien = DateTime.Now;
            thongBao.LoaiThongBao = "PHANHOI";
            thongBao.DaXem = false;
            var check = _thongBaoService.GuiThongBao(thongBao);
            GuiMailPhanHoi(ph.TenNguoiGui,ph.Email,ph.SDT, ph.NoiDungPhanHoi);
            if (result && check)
            {
                return Json(new { status = TrangThai.ThanhCong , message = Message.Success , alerticon = AlertIcon.Success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = TrangThai.ThatBai , message = Message.Failure, alerticon = AlertIcon.Error }, JsonRequestBehavior.AllowGet);
            }

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