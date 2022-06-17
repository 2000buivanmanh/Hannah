using DATA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IDiaChiXuatBanService
    {
        List<DiaChiXuatBan> DanhSachDiaChiXuatBan();
        string ThemDiaChiXuatBan(DiaChiXuatBan diaChiXuatBan);
        string SuaDiaChiXuatBan(DiaChiXuatBan diaChiXuatBan);
        DiaChiXuatBan LayDiaChiXuatBanTheoMa(int maDiaChiXuatBan);
        DiaChiXuatBan LayDiaChiXuatBanTheoTen(string tenDiaChiXuatBan);
        string XoaDiaChiXuatBan(List<DiaChiXuatBan> data);
        List<DiaChiXuatBan> LayDanhSachMa(int[] data);
        string ThemExcel(List<DiaChiXuatBan> data);
    }
}
