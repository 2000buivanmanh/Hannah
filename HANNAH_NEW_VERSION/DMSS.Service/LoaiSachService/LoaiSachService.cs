using ClosedXML.Excel;
using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class LoaiSachService : ILoaiSachService
    {
        private readonly IBaseRepository<LoaiSach> _baseRepository;
        public LoaiSachService(IBaseRepository<LoaiSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<LoaiSach> DanhSachLoaiSach()
        {
            return _baseRepository.GetAll();
        }
        public List<LoaiSach> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaLoaiSach));
        }
        public LoaiSach LayLoaiSachTheoMa(int maLoaiSach)
        {
            return _baseRepository.Table.SingleOrDefault(s => s.MaLoaiSach == maLoaiSach);
        }

        public string SuaLoaiSach(LoaiSach loaiSach)
        {
            try
            {
                var suaLoaiSach = _baseRepository.Table.SingleOrDefault(s => s.MaLoaiSach == loaiSach.MaLoaiSach);
                suaLoaiSach.TenLoaiSach = loaiSach.TenLoaiSach;
                suaLoaiSach.MoTa = loaiSach.MoTa;
                suaLoaiSach.TrangThai = loaiSach.TrangThai;
                suaLoaiSach.LanCapNhatCuoi = DateTime.Now;
                suaLoaiSach.QTVCapNhat = loaiSach.QTVCapNhat;
                suaLoaiSach.NoiDungSeo = loaiSach.NoiDungSeo;
                suaLoaiSach.TuKhoaSeo = loaiSach.TuKhoaSeo;
                suaLoaiSach.TieuDeSeo = loaiSach.TieuDeSeo;
                suaLoaiSach.DuongDanSeo = loaiSach.DuongDanSeo;
                _baseRepository.Update(suaLoaiSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ThemLoaiSach(LoaiSach loaiSach)
        {
            try
            {
                loaiSach.NgayTao = DateTime.Now;
                loaiSach.TrangThai = false;
                _baseRepository.Insert(loaiSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string XoaLoaiSach(List<LoaiSach> data)
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

        public string ThemExcel(List<LoaiSach> data)
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
