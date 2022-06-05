using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IPhanHoiService
    {
        List<PhanHoi> DanhSachPhanHoi();
        bool GuiPhanHoi(PhanHoi phanHoi);
        PhanHoi LayPhanHoiTheoMa(int maPhanHoi);
        string ChiTietPhanHoi(PhanHoi phanHoi);

    }
}
