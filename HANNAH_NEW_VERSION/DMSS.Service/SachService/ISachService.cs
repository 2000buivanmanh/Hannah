using DATA.Models;
using SERVICE.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ISachService : IBaseService<Sach, Sach>
    {
        Sach LayMaSach(int maMaSach);
        List<Sach> ThongTinSach();
        string ThemSach(Sach sach);
        string SuaSach(Sach sach);
        List<Sach> LayDanhSachMa(int[] data);
        string XoaSach(List<Sach> data);
        string ThemExcel(List<Sach> data);
    }
}
