using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface INguoiDungService
    {
        List<NguoiDung> DanhSachNguoiDung();
        NguoiDung LayMaNGuoiDung(int maNguoiDung);
        NguoiDung KiemTraTenDangNhap(string tenDangNhap);
        NguoiDung ThongTinNguoiDung(string tenDangNhap, string matKhau);
        string CapNhatNguoiDung(NguoiDung nguoiDung);
        string CapNhatAdmin(NguoiDung nguoiDung);
        string CapNhatDiemTichLuy(NguoiDung nguoiDung);
        string TinhTrangNguoiDung(NguoiDung nguoiDung);
        string DuyetTrangThai(NguoiDung nguoiDung);
        string ThemNguoiDung(NguoiDung nguoiDung);
        bool KiemTraTonTaiEmail(string email);
        bool KiemTraTonTaiTenNguoiDung(string username);
        List<NguoiDung> LayDanhSachMa(int[] data);
        string ThemExcel(List<NguoiDung> data);
        string UpdateList(List<NguoiDung> data);
    }
}
