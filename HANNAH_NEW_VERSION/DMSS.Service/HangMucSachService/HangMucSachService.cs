using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class HangMucSachService : IHangMucSachService
    {
        private readonly IBaseRepository<HangMucSach> _baseRepository;
        public HangMucSachService(IBaseRepository<HangMucSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<HangMucSach> DanhSachHangMuc()
        {
            return _baseRepository.GetAll();
        }
        public List<HangMucSach> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaHangMucSach));
        }
        public string ThemHangMucSach(HangMucSach hangMucSach)
        {
            try
            {
                hangMucSach.TrangThai = false;
                hangMucSach.NgayTao = DateTime.Now;
                hangMucSach.LanCapNhatCuoi = DateTime.Now;
                _baseRepository.Insert(hangMucSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CapNhatHangMucSach(HangMucSach hangMucSach)
        {
            try
            {
                var capNhatHangMucSach = _baseRepository.GetById(hangMucSach.MaHangMucSach);
                capNhatHangMucSach.MaNhanDienHangMucSach = hangMucSach.MaNhanDienHangMucSach;
                capNhatHangMucSach.TenHangMuc = hangMucSach.TenHangMuc;
                capNhatHangMucSach.MoTa = hangMucSach.MoTa;
                capNhatHangMucSach.NgayTao = hangMucSach.NgayTao;
                capNhatHangMucSach.LanCapNhatCuoi = DateTime.Now;
                capNhatHangMucSach.QTVCapNhat = hangMucSach.QTVCapNhat;
                capNhatHangMucSach.TuKhoaSeo = hangMucSach.TuKhoaSeo;
                capNhatHangMucSach.TieuDeSeo = hangMucSach.TieuDeSeo;
                capNhatHangMucSach.DuongDanSeo = hangMucSach.DuongDanSeo;
                capNhatHangMucSach.NoiDungSeo = hangMucSach.NoiDungSeo;
                _baseRepository.Update(capNhatHangMucSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string XoaHangMucSach(List<HangMucSach> data)
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
        public HangMucSach LayHangMucSachTheoMa(int maHangMucSach) 
        { 
            return _baseRepository.GetById( maHangMucSach); 
        }

        public string ThemExcel(List<HangMucSach> data)
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

        public HangMucSach KiemTraTonTaiMaNhanDienHangMucSach(string maNhanDienHangMucSach)
        {
            return _baseRepository.GetAll(s => (s.MaNhanDienHangMucSach == maNhanDienHangMucSach )).FirstOrDefault();

        }
    }
}
