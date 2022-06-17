using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface INhaXuatBanService
    {
        List<NhaXuatBan> DanhSachNhaXuatBan();
        string ThemNhaXuatBan(NhaXuatBan nhaXuatBan);
        string SuaNhaXuatBan(NhaXuatBan nhaXuatBan);
        NhaXuatBan LayNhaXuatBanTheoMa(int maNhaXuatBan);
        NhaXuatBan LayNhaXuatBanTheoTen(string tenNhaXuatBan);
        string XoaNhaXuatBan(List<NhaXuatBan> data);
        List<NhaXuatBan> LayDanhSachMa(int[] data);
        string ThemExcel(List<NhaXuatBan> data);
    }
}
