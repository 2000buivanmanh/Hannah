using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IHinhAnhSachService
    {
        List<HinhAnhSach> DanhSachHinhAnhSach();
        string ThemList(List<HinhAnhSach> data);
        List<HinhAnhSach> LayListHinhTheoMa(int? id);
        string XoaHinhAnh(List<HinhAnhSach> data);
        List<HinhAnhSach> LayDanhSachMa(int[] data);
    }
}
