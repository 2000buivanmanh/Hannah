using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class ChiTietDatSachService : IChiTietDatSachService
    {
        private readonly IBaseRepository<ChiTietDatSach> _baseRepository;

        public ChiTietDatSachService(IBaseRepository<ChiTietDatSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<ChiTietDatSach> DanhSachChiTietDatSach()
        {
            return _baseRepository.GetAll();
        }
        public bool ThemChiTietDatSach(List<ChiTietDatSach> chiTietDatSach)
        {
            try
            {
                _baseRepository.Insert(chiTietDatSach);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<ChiTietDatSach> LayChiTietDatSachTheoMa(int maDonSach)
        {
            return _baseRepository.GetAll(s => s.MaDonSach == maDonSach);
        }
        public List<ChiTietDatSach> LayListChiTietTheoMaSach(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains((Int32)s.MaSach));
        }
        public ChiTietDatSach LayChiTietDatSachTheoMaSach(int maSach)
        {
            return _baseRepository.GetAll(s => s.MaSach == maSach).OrderByDescending(s => s.NgayDat).Take(1).FirstOrDefault();
        }

        public List<ChiTietDatSach> LayListChiTietTheoMaSach(int maSach)
        {
            var a = _baseRepository.GetAll(s => s.MaSach == maSach).ToList();
            return a;
        }

    }
}
