using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
     public interface IBaiVietService
    {
        List<BaiViet> DanhSachBaiViet();
        string ThemBaiViet(BaiViet baiViet);
        string SuaBaiViet(BaiViet baiViet);
        BaiViet LayBaiVietTheoMa(int maBaiViet);
        string XoaBaiViet(List<BaiViet> data);

        List<BaiViet> LayDanhSachMa(int[] data);
        string ThemExcel(List<BaiViet> data);
    }
}
