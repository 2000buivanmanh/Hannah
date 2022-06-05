using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ITheLoaiSachService
    {
        List<TheLoai> DanhSachTheLoaiSach();
        string ThemTheLoai(TheLoai theLoai);
        string SuaTheLoai(TheLoai theLoai);
        string XoaTheLoaiSach(List<TheLoai> data);
        List<TheLoai> LayDanhSachMa(int[] data);
        string ThemExcel(List<TheLoai> data);
        TheLoai LayTheLoaiTheoMa(int maTheLoai);

    }
}
