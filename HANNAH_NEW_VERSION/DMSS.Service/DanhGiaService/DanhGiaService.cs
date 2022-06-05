using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class DanhGiaService: IDanhGiaService
    {
        private readonly IBaseRepository<DanhGia> _baseRepository;

        public DanhGiaService(IBaseRepository<DanhGia> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<DanhGia> DanhSachDanhGia()
        {
            return _baseRepository.GetAll();
        }
        public bool ThemDanhGia(DanhGia danhGia)
        {
            try
            {
                _baseRepository.Insert(danhGia);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaDanhGia(List<DanhGia> data)
        {
            try
            {
                _baseRepository.Remove(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<DanhGia> LayTheoMaSach(int maSach)
        {
            return _baseRepository.GetAll(s=> s.MaSach == maSach);
        }

        public List<DanhGia> LayDacQuyenTheoMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaDanhGia));
        }

  
    }
}
