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
    public class DatSachController : Controller
    {
        // GET: Admin/DatSach
        private readonly IDatSachService _datSachService;
        private readonly ISachService _sachService;
        private readonly IChiTietDatSachService _chiTietDatSachService;

        public DatSachController(IDatSachService datSachService, ISachService sachService,
            IChiTietDatSachService chiTietDatSachService)
        {
            _datSachService = datSachService;
            _sachService = sachService;
            _chiTietDatSachService = chiTietDatSachService;
        }
        public ActionResult QuanLyDatSach()
        {
            return View();
        }
        public ActionResult _DanhSachDatSach()
        {
            var datSach = _datSachService.DanhSachDatSach();
            return PartialView(datSach);
        }
        public ActionResult _ChiTietDatSach(int id )
        {
            List<int> data = new List<int>();
            List<int> data1 = new List<int>();
            var donSach = _datSachService.LayDonDatSachTheoMaDonSach(id);
            var chiTietSach = _chiTietDatSachService.LayChiTietDatSachTheoMa(id);
            foreach (var item in chiTietSach)
            {
                data.Add((int)item.MaSach);
            }
            var a = _chiTietDatSachService.LayListChiTietTheoMaSach(data.ToArray());
            foreach (var item in a)
            {
                data1.Add((int)item.MaDonSach);
            }
           ViewBag.TrangThai = _datSachService.LayDonHang(data1.ToArray());
            ViewBag.ChiTietSach = chiTietSach;
                
            return PartialView(donSach);
        }
    }
}