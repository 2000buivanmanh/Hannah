using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class HinhAnhSachService : IHinhAnhSachService
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

        public List<HinhAnhSach> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains((Int32)s.MaSach));
        }

        public List<HinhAnhSach> LayListHinhTheoMa(int? id)
        {
            return  _baseRepository.GetAll(s => s.MaSach == id).ToList();
        }

        public string ThemList(List<HinhAnhSach> data)
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
        public string XoaHinhAnh(List<HinhAnhSach> data)
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
    }
}
