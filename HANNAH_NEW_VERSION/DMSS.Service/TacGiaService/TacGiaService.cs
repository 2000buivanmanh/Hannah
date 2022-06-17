using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class TacGiaService : ITacGiaService
    {
        private readonly IBaseRepository<TacGia> _baseRepository;
        public TacGiaService(IBaseRepository<TacGia> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<TacGia> DanhSachTacGia()
        {
            return _baseRepository.GetAll();
        }

        public TacGia LayTacGiaTheoMa(int maTacGia)
        {
            return _baseRepository.GetById(maTacGia);
        }
        public List<TacGia> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaTacGia));
        }
        public string SuaTacGia(TacGia TacGia)
        {
            try
            {
                var suaNhanXuatBan = _baseRepository.GetById( TacGia.MaTacGia);
                suaNhanXuatBan.TenTacGia = TacGia.TenTacGia;
                suaNhanXuatBan.ThongTinTacGia = TacGia.ThongTinTacGia;
                suaNhanXuatBan.TrangThai = TacGia.TrangThai;
                suaNhanXuatBan.LanCapNhatCuoi = DateTime.Now;
                suaNhanXuatBan.QTVCapNhat = TacGia.QTVCapNhat;
                suaNhanXuatBan.NoiDungSeo = TacGia.NoiDungSeo;
                suaNhanXuatBan.TuKhoaSeo = TacGia.TuKhoaSeo;
                suaNhanXuatBan.TieuDeSeo = TacGia.TieuDeSeo;
                suaNhanXuatBan.DuongDanSeo = TacGia.DuongDanSeo;
                _baseRepository.Update(suaNhanXuatBan);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ThemTacGia(TacGia TacGia)
        {
            try
            {
                TacGia.NgayTao = DateTime.Now;
                TacGia.TrangThai = false;
                _baseRepository.Insert(TacGia);
               
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string XoaTacGia(List<TacGia> data)
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
        public string ThemExcel(List<TacGia> data)
        {
            try
            {
                _baseRepository.Insert(data);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public TacGia LayTacGiaTheoTen(string tenTacGia)
        {
            return _baseRepository.GetAll(s => s.TenTacGia == tenTacGia).FirstOrDefault();
        }
    }
}
