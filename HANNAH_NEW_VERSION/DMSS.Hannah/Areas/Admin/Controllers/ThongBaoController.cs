
using DATA.Models;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    public class ThongBaoController : Controller
    {
        // GET: Admin/ThongBao
        private readonly IPhanHoiService _phanHoiService;
        private readonly IThongBaoService _thongBaoService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDanhGiaService _danhGiaService;
        public ThongBaoController(IPhanHoiService phanHoiService, IDanhGiaService danhGiaService,
        INguoiDungService nguoiDungService,
                                IThongBaoService thongBaoService)
        {
            _phanHoiService = phanHoiService;
            _thongBaoService = thongBaoService;
            _nguoiDungService = nguoiDungService;
            _danhGiaService = danhGiaService;
        }
        public ActionResult _ThongBaoAdmin()
        {
            var dsThongBao = _thongBaoService.DanhMucThongBao();
            List<PhanHoi> dsPhanHoi = new List<PhanHoi>();
            foreach (var item in dsThongBao)
            {
                if (item.MaPhanHoi != 0 && item.DaXem == false)
                {
                    var phanHoi = _phanHoiService.LayPhanHoiTheoMa(item.MaPhanHoi);
                    dsPhanHoi.Add(phanHoi);
                }
            }
            List<NguoiDung> dsNguoiDung = new List<NguoiDung>();
            foreach (var item in dsThongBao)
            {
                if (item.MaNguoiDung != 0 && item.DaXem == false)
                {
                    var nguoiDung = _nguoiDungService.LayMaNGuoiDung(item.MaNguoiDung);
                    dsNguoiDung.Add(nguoiDung);
                }
            }
            List<DanhGia> dsDanhGia = new List<DanhGia>();
            foreach (var item in dsThongBao)
            {
                if (item.MaPhanHoi != 0 && item.DaXem == false)
                {
                    var phanHoi = _phanHoiService.LayPhanHoiTheoMa(item.MaPhanHoi);
                    dsPhanHoi.Add(phanHoi);
                }
            }
            ViewBag.dsNguoiDung = dsNguoiDung;
            ViewBag.dsPhanHoi = dsPhanHoi;
            return PartialView();
        }
    }
}