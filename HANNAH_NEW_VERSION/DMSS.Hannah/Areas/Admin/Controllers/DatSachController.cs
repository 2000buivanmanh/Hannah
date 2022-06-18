using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HANNAH_NEW_VERSION.Areas.Admin.Controllers
{
    public class DatSachController : Controller
    {
        // GET: Admin/DatSach
        private readonly IDatSachService _datSachService;

        public DatSachController(IDatSachService datSachService)
        {
            _datSachService = datSachService;
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
        public ActionResult _ChiTietDatSach(int id)
        {
            var chiTiet = _datSachService.LayDonDatSachTheoMaDonSach(id);
            return PartialView(chiTiet);
        }
    }
}