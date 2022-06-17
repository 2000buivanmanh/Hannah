using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class DiaChiXuatBanService : IDiaChiXuatBanService
    {
        private readonly IBaseRepository<DiaChiXuatBan> _baseRepository;
        public DiaChiXuatBanService(IBaseRepository<DiaChiXuatBan> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<DiaChiXuatBan> DanhSachDiaChiXuatBan()
        {
            return _baseRepository.GetAll();
        }
        public List<DiaChiXuatBan> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaDiaChi));
        }
        public DiaChiXuatBan LayDiaChiXuatBanTheoMa(int maDiaChi)
        {
            return _baseRepository.GetById( maDiaChi);
        }

        public string SuaDiaChiXuatBan(DiaChiXuatBan diaChiXuatBan)
        {
            try
            {
                var suaDiaChiXuatBan = _baseRepository.GetById( diaChiXuatBan.MaDiaChi);
                suaDiaChiXuatBan.TenDiaChi = diaChiXuatBan.TenDiaChi;
                suaDiaChiXuatBan.ChiTietDiaChi = diaChiXuatBan.ChiTietDiaChi;
                suaDiaChiXuatBan.LanCapNhatCuoi = DateTime.Now;
                suaDiaChiXuatBan.QTVCapNhat = diaChiXuatBan.QTVCapNhat;
                suaDiaChiXuatBan.NoiDungSeo = diaChiXuatBan.NoiDungSeo;
                suaDiaChiXuatBan.TuKhoaSeo = diaChiXuatBan.TuKhoaSeo;
                suaDiaChiXuatBan.TieuDeSeo = diaChiXuatBan.TieuDeSeo;
                suaDiaChiXuatBan.DuongDanSeo = diaChiXuatBan.DuongDanSeo;
                _baseRepository.Update(suaDiaChiXuatBan);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ThemDiaChiXuatBan(DiaChiXuatBan diaChiXuatBan)
        {
            try
            {
                diaChiXuatBan.NgayTao = DateTime.Now;
                _baseRepository.Insert(diaChiXuatBan);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string XoaDiaChiXuatBan(List<DiaChiXuatBan> data)
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

        public string ThemExcel(List<DiaChiXuatBan> data)
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

        public DiaChiXuatBan LayDiaChiXuatBanTheoTen(string tenDiaChiXuatBan)
        {
            return _baseRepository.GetAll(s => s.TenDiaChi == tenDiaChiXuatBan).FirstOrDefault();
        }
    }
}
