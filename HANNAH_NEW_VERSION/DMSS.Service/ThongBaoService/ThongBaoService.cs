using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly IBaseRepository<ThongBao> _baseRepository;
        public ThongBaoService(IBaseRepository<ThongBao> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<ThongBao> DanhMucThongBao()
        {
            return _baseRepository.GetAll();
        }

        public bool GuiThongBao(ThongBao thongBao)
        {
            try
            {
                _baseRepository.Insert(thongBao);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
