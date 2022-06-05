using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ITacGiaService
    {
        List<TacGia> DanhSachTacGia();
        string ThemTacGia(TacGia  tacGia);
        string SuaTacGia(TacGia tacGia);
        TacGia LayTacGiaTheoMa(int maTacGia);
        List<TacGia> LayDanhSachMa(int[] data);
        string XoaTacGia(List<TacGia> data);
        string ThemExcel(List<TacGia> data);
    }
}
