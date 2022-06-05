using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IDanhGiaService
    {
        List<DanhGia> DanhSachDanhGia();
        bool ThemDanhGia(DanhGia danhGia);
        bool XoaDanhGia(List<DanhGia> data);
        List<DanhGia> LayDacQuyenTheoMa(int[] data);
        List<DanhGia> LayTheoMaSach(int maSach);
    }
}
