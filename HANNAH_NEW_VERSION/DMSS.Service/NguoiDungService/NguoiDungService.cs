using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SERVICE
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly IBaseRepository<NguoiDung> _baseRepository;
        private readonly IBaseRepository<CaiDat> _caiDatRepository;

        public NguoiDungService(IBaseRepository<NguoiDung> baseRepository,
                                IBaseRepository<CaiDat> caiDatRepository
                               )
        {
            _baseRepository = baseRepository;
            _caiDatRepository = caiDatRepository;

        }
        public List<NguoiDung> DanhSachNguoiDung()
        {
            return _baseRepository.GetAll();
        }
        public NguoiDung LayMaNGuoiDung(int maNguoiDung)
        {
            return _baseRepository.GetById(maNguoiDung);
        }
        public List<NguoiDung> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaNguoiDung));
        }
        public NguoiDung KiemTraTenDangNhap(string tenDangNhap)
        {
            var kiemTraDangNhap = _baseRepository.GetAll(s => (s.TenDangNhap == tenDangNhap || s.EmailNguoiDung == tenDangNhap)).FirstOrDefault();
            return kiemTraDangNhap;
        }

        public NguoiDung ThongTinNguoiDung(string tenDangNhap, string matKhau)
        {
            var user = _baseRepository.GetAll(s => (s.EmailNguoiDung == tenDangNhap || s.TenDangNhap == tenDangNhap) && s.MatKhau == matKhau).FirstOrDefault();
            return user;
        }
        public string CapNhatNguoiDung(NguoiDung nguoiDung)
        {
            try
            {
                var capNhat = _baseRepository.GetById(nguoiDung.MaNguoiDung);
                capNhat.HoTen = nguoiDung.HoTen;
                capNhat.EmailNguoiDung = nguoiDung.EmailNguoiDung;
                capNhat.TenDangNhap = nguoiDung.TenDangNhap.Trim();
                capNhat.DiaChi = nguoiDung.DiaChi;
                capNhat.NgaySinh = nguoiDung.NgaySinh;
                capNhat.GioiThieu = nguoiDung.GioiThieu;
                capNhat.CongKhaiThongTin = nguoiDung.CongKhaiThongTin;
                capNhat.MatKhau = nguoiDung.MatKhau;
                _baseRepository.Update(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CapNhatAdmin(NguoiDung nguoiDung)
        {
            try
            {
                var capNhat = _baseRepository.GetById(nguoiDung.MaNguoiDung);
                capNhat.DiemTichLuy = nguoiDung.DiemTichLuy;
                capNhat.DiaChi = nguoiDung.DiaChi;
                capNhat.NgaySinh = nguoiDung.NgaySinh;
                capNhat.SoDienThoai = nguoiDung.SoDienThoai;
                capNhat.MatKhau = nguoiDung.MatKhau;
                _baseRepository.Update(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ThemNguoiDung(NguoiDung nguoiDung)
        {
            try
            {
                _baseRepository.Insert(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool KiemTraTonTaiEmail(string email)
        {
            var mail = _baseRepository.GetAll(s => s.EmailNguoiDung == email).FirstOrDefault();
            if (mail == null)
                return true;
            return false;
        }
        public bool KiemTraTonTaiTenNguoiDung(string username)
        {
            var usname = _baseRepository.GetAll(s => (s.TenDangNhap == username || s.EmailNguoiDung == username)).FirstOrDefault();
            if (usname == null)
                return true;
            return false;
        }

        public string TinhTrangNguoiDung(NguoiDung nguoiDung)
        {
            try
            {
                var capNhat = _baseRepository.GetById(nguoiDung.MaNguoiDung);
                capNhat.TinhTrangNguoiDung = nguoiDung.TinhTrangNguoiDung;
                _baseRepository.Update(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DuyetTrangThai(NguoiDung nguoiDung)
        {
            try
            {
                var capNhat = _baseRepository.GetById(nguoiDung.MaNguoiDung);
                capNhat.TrangThai = nguoiDung.TrangThai;
                _baseRepository.Update(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string CapNhatDiemTichLuy(NguoiDung nguoiDung)
        {
            try
            {
                var capNhat = _baseRepository.GetById(nguoiDung.MaNguoiDung);
                capNhat.DiemTichLuy = nguoiDung.DiemTichLuy;
                _baseRepository.Update(nguoiDung);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ThemExcel(List<NguoiDung> data)
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

        public string UpdateList(List<NguoiDung> data)
        {
            try
            {
                _baseRepository.Update(data);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
