using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IHangMucSachService
    {
        List<HangMucSach> DanhSachHangMuc();
        string ThemHangMucSach(HangMucSach hangMucSach);
        string CapNhatHangMucSach(HangMucSach hangMucSach);
        string XoaHangMucSach(List<HangMucSach> data);
        string ThemExcel(List<HangMucSach> data);
        HangMucSach LayHangMucSachTheoMa(int maHangMucSach);
        List<HangMucSach> LayDanhSachMa(int[] data);

        HangMucSach KiemTraTonTaiMaNhanDienHangMucSach(string maNhanDienHangMucSach);

    }
}
