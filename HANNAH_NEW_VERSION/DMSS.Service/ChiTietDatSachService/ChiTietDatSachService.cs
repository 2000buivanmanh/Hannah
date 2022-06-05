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
    }
}
