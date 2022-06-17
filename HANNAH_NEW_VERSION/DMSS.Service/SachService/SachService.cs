using AutoMapper;
using DATA.Models;
using DATA.Repository;
using SERVICE.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class SachService : BaseService<Sach, Sach>, ISachService 
    {
        private readonly IBaseRepository<Sach> _baseRepository;
        private readonly IMapper _mapper;
        public SachService( IBaseRepository<Sach> baseRepository,
                            IMapper mapper) : base (baseRepository, mapper)
                            
        {
            _baseRepository = baseRepository;
            this._mapper = mapper;
        }
        public List<Sach> ThongTinSach()
        {
            return _baseRepository.GetAll();
        }
        public Sach LayMaSach(int maMaSach)
        {
            return _baseRepository.GetById(maMaSach); 
        }
        public List<Sach> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaSach));
        }
        public string XoaSach(List<Sach> data)
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
        public string ThemExcel(List<Sach> data)
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
        public string SuaSach(Sach sach)
        {
            try
            {
                var suaSach = _baseRepository.GetById( sach.MaSach);
                suaSach.TenSach = sach.TenSach;
                suaSach.Gia = sach.Gia;
                suaSach.MoTaSachVi = sach.MoTaSachVi;
                suaSach.ThongTinAnhSach = sach.ThongTinAnhSach;
                suaSach.MoTaSachEn = sach.MoTaSachEn;
                suaSach.TrangThai = sach.TrangThai;
                suaSach.TinhTrangMuonSach = sach.TinhTrangMuonSach;
                suaSach.MaTacGia = sach.MaTacGia;
                suaSach.MaTheLoai = sach.MaTheLoai;
                suaSach.MaNhomTuoi = sach.MaNhomTuoi;
                suaSach.MaNhaXuatBan = sach.MaNhaXuatBan;
                suaSach.MaLoaiSach = sach.MaLoaiSach;
                suaSach.MaDiaChi = sach.MaDiaChi;
                suaSach.NgayXuatBan = sach.NgayXuatBan;
                suaSach.LanDauXuatBan = sach.LanDauXuatBan;
                suaSach.MaTieuChuanSach = sach.MaTieuChuanSach;
                suaSach.SoLuongTrang = sach.SoLuongTrang;
                suaSach.KichThuocSach = sach.KichThuocSach;
                suaSach.LanCapNhatCuoi = DateTime.Now;
                suaSach.QTVCapNhat = sach.QTVCapNhat;
                suaSach.NoiDungSeo = sach.NoiDungSeo;
                suaSach.TuKhoaSeo = sach.TuKhoaSeo;
                suaSach.TieuDeSeo = sach.TieuDeSeo;
                suaSach.DuongDanSeo = sach.DuongDanSeo;
                _baseRepository.Update(suaSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ThemSach(Sach Sach)
        {
            try
            {
                Sach.NgayTao = DateTime.Now;
                Sach.TrangThai = false;
                _baseRepository.Insert(Sach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
