using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class DatSachService : IDatSachService
    {
        private readonly IBaseRepository<DatSach> _baseRepository;

        public DatSachService(IBaseRepository<DatSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<DatSach> DanhSachDatSach()
        {
            return _baseRepository.GetAll();
        }
        public bool DatSach(DatSach datSach)
        {
            try
            {
                _baseRepository.Insert(datSach);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool HuyDon(List<DatSach> data)
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

        public bool CapNhatDonDatSach(DatSach datSach)
        {
            try
            {
               
                _baseRepository.Update(datSach);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<DatSach> LayDonHang(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaDonSach));
        }

        public DatSach LayDonDatSachTheoMaDonSach(int maDonSach)
        {
            return _baseRepository.GetById(maDonSach);
        }
    }
}
