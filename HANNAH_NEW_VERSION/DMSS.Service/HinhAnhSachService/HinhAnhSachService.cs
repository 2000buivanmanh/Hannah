using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class HinhAnhSachService: IHinhAnhSachService
    {
        private readonly IBaseRepository<HinhAnhSach> _baseRepository;
        public HinhAnhSachService(IBaseRepository<HinhAnhSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<HinhAnhSach> DanhSachHinhAnhSach()
        {
            return _baseRepository.GetAll();
        }

    }
}
