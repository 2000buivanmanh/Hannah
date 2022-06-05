using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ISlideService
    {
        List<Slide> LayThongTinSlide();
        string ThemSlide(Slide slide);
        Slide LaySlideTheoMa(int maSlide);
        string CapNhatSlide(Slide slide);
        string XoaSlide(List<Slide> data);
        List<Slide> LayDanhSachMa(int[] data);
    }
}
