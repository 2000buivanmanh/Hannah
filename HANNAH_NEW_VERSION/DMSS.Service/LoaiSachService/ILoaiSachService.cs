using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ILoaiSachService
    {
        List<LoaiSach> DanhSachLoaiSach();
        string ThemLoaiSach(LoaiSach loaiSach);
        string SuaLoaiSach(LoaiSach loaiSach);
        LoaiSach LayLoaiSachTheoMa(int maLoaiSach);
        LoaiSach LayLoaiSachTheoTen(string tenLoaiSach);
        string XoaLoaiSach(List<LoaiSach> data);
        List<LoaiSach> LayDanhSachMa(int[] data);
        string ThemExcel(List<LoaiSach> data);
    }
}
