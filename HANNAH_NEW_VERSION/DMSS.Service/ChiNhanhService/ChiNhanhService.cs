using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class ChiNhanhService : IChiNhanhService
    {
        private readonly IBaseRepository<ChiNhanh> _baseRepository;
        public ChiNhanhService(IBaseRepository<ChiNhanh> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<ChiNhanh> DanhSachDiaChi()
        {
            return _baseRepository.GetAll();
        }
    }
}
