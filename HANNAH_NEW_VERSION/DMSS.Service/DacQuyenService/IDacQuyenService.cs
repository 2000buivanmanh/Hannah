using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IDacQuyenService
    {
        List<DacQuyen> DanhSachDacQuyen();
        string ThemDacQuyen(DacQuyen dacQuyen);
        string SuaDacQuyen(DacQuyen dacQuyen);
        string XoaDacQuyen(List<DacQuyen> data);
        List<DacQuyen> LayDanhSachMa(int[] data);
        DacQuyen LayDacQuyenTheoMa(int maDacQuyen);
        string ThemExcel(List<DacQuyen> data);
    }
}
