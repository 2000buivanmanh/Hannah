using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
   public interface ICaiDatService
    {
        CaiDat LayThongTinWeb();
        bool CapNhatThongTinWeb(CaiDat caiDat);
        List<CaiDat> DanhSachCaiDat();
        CaiDat LayMaCaiDat(int maCaiDat);
    }
}
