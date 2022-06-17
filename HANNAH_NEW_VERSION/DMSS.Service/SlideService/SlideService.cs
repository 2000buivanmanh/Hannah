using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class SlideService : ISlideService
    {
        private readonly IBaseRepository<Slide> _baseRepository;

        public SlideService(IBaseRepository<Slide> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<Slide> LayThongTinSlide()
        {
            return _baseRepository.GetAll();
        }

        public string ThemSlide(Slide slide)
        {
            try
            {
                slide.NgayTao = DateTime.Now;
                _baseRepository.Insert(slide);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<Slide> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaSlide));
        }
        public Slide LaySlideTheoMa(int maSlide)
        {
            return _baseRepository.GetById( maSlide);
        }

        public string CapNhatSlide(Slide slide)
        {
            try
            {
                var capNhatslide = _baseRepository.GetById(slide.MaSlide);
                capNhatslide.TenSlide = slide.TenSlide;
                capNhatslide.AnhSlide = slide.AnhSlide;
                capNhatslide.MoTa = slide.MoTa;
                capNhatslide.NoiDung = slide.NoiDung;
                capNhatslide.TieuDe = slide.TieuDe;
                capNhatslide.TieuDeHienThi = slide.TieuDeHienThi;
                capNhatslide.MoTaHienThi = slide.MoTaHienThi;
                capNhatslide.NoiDungHienThi = slide.NoiDungHienThi;
                capNhatslide.TrangThai = slide.TrangThai;
                _baseRepository.Update(capNhatslide);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string XoaSlide(List<Slide> data)
        {
            try
            {
                _baseRepository.Remove(data);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
