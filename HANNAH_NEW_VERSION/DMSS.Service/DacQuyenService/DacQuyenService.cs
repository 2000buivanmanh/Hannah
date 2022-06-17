using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class DacQuyenService : IDacQuyenService
    {
       private readonly IBaseRepository<DacQuyen> _baseRepository;
        public DacQuyenService(IBaseRepository<DacQuyen> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<DacQuyen> DanhSachDacQuyen()
        {
            return _baseRepository.GetAll();
        }

        public DacQuyen LayDacQuyenTheoMa(int maDacQuyen)
        {
            return _baseRepository.GetById( maDacQuyen);
        }
        public List<DacQuyen> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaDacQuyen));
        }
        public string SuaDacQuyen(DacQuyen dacQuyen)
        {
            try
            {
                var suaDacQuyen = _baseRepository.GetById( dacQuyen.MaDacQuyen);
                suaDacQuyen.TenDacQuyen = dacQuyen.TenDacQuyen;
                suaDacQuyen.NoiDung = dacQuyen.NoiDung;
                suaDacQuyen.Icon = dacQuyen.Icon;
                suaDacQuyen.TrangThai = dacQuyen.TrangThai;
                suaDacQuyen.NoiDungSeo = dacQuyen.NoiDungSeo;
                suaDacQuyen.TuKhoaSeo = dacQuyen.TuKhoaSeo;
                suaDacQuyen.TieuDeSeo = dacQuyen.TieuDeSeo;
                suaDacQuyen.DuongDanSeo = dacQuyen.DuongDanSeo;
                suaDacQuyen.LanCapNhatCuoi = DateTime.Now;
                suaDacQuyen.QTVCapNhat = dacQuyen.QTVCapNhat;
                _baseRepository.Update(suaDacQuyen);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ThemDacQuyen(DacQuyen DacQuyen)
        {
            try
            {
                DacQuyen.TrangThai = false;
                DacQuyen.NgayTao = DateTime.Now;
                _baseRepository.Insert(DacQuyen);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string XoaDacQuyen(List<DacQuyen> data)
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
        public string ThemExcel(List<DacQuyen> data)
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
