using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface INhomTuoiService
    {
        List<NhomTuoi> DanhSachNhomTuoi();
        string ThemNhomTuoi(NhomTuoi nhomTuoi);
        string SuaNhomTuoi(NhomTuoi nhomTuoi);
        string XoaNhomTuoi(List<NhomTuoi> data);
        NhomTuoi LayNhomTuoiTheoMa(int maNhomTuoi);
        List<NhomTuoi> LayDanhSachMa(int[] data);
        string ThemExcel(List<NhomTuoi> data);
    }
}
