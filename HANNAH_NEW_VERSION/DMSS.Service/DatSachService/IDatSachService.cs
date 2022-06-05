using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IDatSachService
    {
        List<DatSach> LayDonHang(int[] data);
        bool HuyDon(List<DatSach> data);
        bool DatSach(DatSach datSach);
        List<DatSach> DanhSachDatSach();


    }
}
