using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class PhanHoiService : IPhanHoiService
    {
        private readonly IBaseRepository<PhanHoi> _baseRepository;
        public PhanHoiService(IBaseRepository<PhanHoi> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public string ChiTietPhanHoi(PhanHoi phanHoi)
        {
            try
            {
                var chiTietPhanHoi = _baseRepository.Table.SingleOrDefault(s => s.MaPhanHoi == phanHoi.MaPhanHoi);
                chiTietPhanHoi.TenNguoiGui = phanHoi.TenNguoiGui;
                chiTietPhanHoi.Email = phanHoi.Email;
                chiTietPhanHoi.SDT = phanHoi.SDT;
                chiTietPhanHoi.NoiDungPhanHoi = phanHoi.NoiDungPhanHoi;
                chiTietPhanHoi.MaNguoiDung = phanHoi.MaNguoiDung;
                chiTietPhanHoi.TrangThai = phanHoi.TrangThai;
                chiTietPhanHoi.NgayXuLy = DateTime.Now;
                chiTietPhanHoi.NoiDungXuLy = phanHoi.NoiDungXuLy;
                _baseRepository.Update(chiTietPhanHoi);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<PhanHoi> DanhSachPhanHoi()
        {
            return _baseRepository.GetAll();
        }
        public bool GuiPhanHoi(PhanHoi phanHoi)
        {
            try
            {
                _baseRepository.Insert(phanHoi);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public PhanHoi LayPhanHoiTheoMa(int maPhanHoi)
        {
            return _baseRepository.Table.SingleOrDefault(s => s.MaPhanHoi == maPhanHoi);
        }
    }
}
