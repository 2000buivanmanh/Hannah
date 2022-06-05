using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class NhaXuatBanService : INhaXuatBanService
    {
        private readonly IBaseRepository<NhaXuatBan> _baseRepository;
        public NhaXuatBanService(IBaseRepository<NhaXuatBan> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<NhaXuatBan> DanhSachNhaXuatBan()
        {
            return _baseRepository.GetAll();
        }
        public List<NhaXuatBan> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaNhaXuatBan));
        }
        public NhaXuatBan LayNhaXuatBanTheoMa(int maNhaXuatBan)
        {
            return _baseRepository.Table.SingleOrDefault(s => s.MaNhaXuatBan == maNhaXuatBan);
        }

        public string SuaNhaXuatBan(NhaXuatBan nhaXuatBan)
        {
            try
            {
                var suaNhaXuatBan = _baseRepository.Table.SingleOrDefault(s => s.MaNhaXuatBan == nhaXuatBan.MaNhaXuatBan);
                suaNhaXuatBan.TenNhaXuatBan = nhaXuatBan.TenNhaXuatBan;
                suaNhaXuatBan.ThongTinNXB = nhaXuatBan.ThongTinNXB;
                suaNhaXuatBan.TrangThai = nhaXuatBan.TrangThai;
                suaNhaXuatBan.LanCapNhatCuoi = DateTime.Now;
                suaNhaXuatBan.QTVCapNhat = nhaXuatBan.QTVCapNhat;
                suaNhaXuatBan.NoiDungSeo = nhaXuatBan.NoiDungSeo;
                suaNhaXuatBan.TuKhoaSeo = nhaXuatBan.TuKhoaSeo;
                suaNhaXuatBan.TieuDeSeo = nhaXuatBan.TieuDeSeo;
                suaNhaXuatBan.DuongDanSeo = nhaXuatBan.DuongDanSeo;
                _baseRepository.Update(suaNhaXuatBan);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ThemNhaXuatBan(NhaXuatBan nhaXuatBan)
        {
            try
            {
                nhaXuatBan.NgayTao = DateTime.Now;
                nhaXuatBan.TrangThai = false;
                _baseRepository.Insert(nhaXuatBan);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string XoaNhaXuatBan(List<NhaXuatBan> data)
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
        public string ThemExcel(List<NhaXuatBan> data)
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
    }
}
