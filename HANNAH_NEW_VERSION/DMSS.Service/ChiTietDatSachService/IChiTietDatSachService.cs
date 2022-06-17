using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IChiTietDatSachService
    {
        List<ChiTietDatSach> DanhSachChiTietDatSach();
        bool ThemChiTietDatSach(List<ChiTietDatSach> chiTietDatSach);
        List<ChiTietDatSach> LayChiTietDatSachTheoMa(int maDonSach);
        ChiTietDatSach LayChiTietDatSachTheoMaSach(int maSach);
        List<ChiTietDatSach> LayListChiTietTheoMaSach(int maSach);
    }
}
