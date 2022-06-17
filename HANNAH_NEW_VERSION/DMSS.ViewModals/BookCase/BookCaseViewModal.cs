
using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.BookCase
{
    public class BookCaseViewModal
    {
        public NguoiDung NguoiDung { get; set; }
        public List<Sach> DanhSachSach { get; set; }
        public decimal TongDiem { get; set; }
        public List<ChiNhanh> DanhSachChiNhanh { get; set; }
        public DateTime ThoiGianNhan { get; set; }
        public DateTime ThoiGianTra { get; set; }

        public class NgayVuaChon
        {
            string NgayNhanVuaChon { get; set; }
            string NgayNhanVuaTra { get; set; }

        }

        public string DieuKhoan { get; set; }
    }
}
