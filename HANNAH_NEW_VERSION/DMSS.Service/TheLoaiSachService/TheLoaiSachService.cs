using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class TheLoaiSachService : ITheLoaiSachService
    {
        private readonly IBaseRepository<TheLoai> _baseRepository;
        public TheLoaiSachService(IBaseRepository<TheLoai> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<TheLoai> DanhSachTheLoaiSach()
        {
            return _baseRepository.GetAll();
        }

        public List<TheLoai> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaTheLoai));
        }

        public TheLoai LayTheLoaiTheoMa(int maTheLoai)
        {
            return _baseRepository.Table.SingleOrDefault(s => s.MaTheLoai == maTheLoai);
        }

        public string SuaTheLoai(TheLoai theLoai)
        {
            try
            {
                var suaTheLoai = _baseRepository.Table.SingleOrDefault(s => s.MaTheLoai == theLoai.MaTheLoai);
                suaTheLoai.TenTheLoai = theLoai.TenTheLoai;
                suaTheLoai.HangMuc = theLoai.HangMuc;
                suaTheLoai.Icon = theLoai.Icon;
                suaTheLoai.TrangThai = theLoai.TrangThai;
                suaTheLoai.MoTa = theLoai.MoTa;
                suaTheLoai.MaNhanDienHangMucSach = theLoai.MaNhanDienHangMucSach;
                suaTheLoai.NoiDungSeo = theLoai.NoiDungSeo;
                suaTheLoai.TuKhoaSeo = theLoai.TuKhoaSeo;
                suaTheLoai.TieuDeSeo = theLoai.TieuDeSeo;
                suaTheLoai.DuongDanSeo = theLoai.DuongDanSeo;
                suaTheLoai.LanCapNhatCuoi = DateTime.Now;
                suaTheLoai.QTVCapNhat = theLoai.QTVCapNhat;
                _baseRepository.Update(suaTheLoai);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ThemTheLoai(TheLoai theLoai)
        {
            try
            {
                theLoai.TrangThai = false;
                theLoai.NgayTao = DateTime.Now;
                _baseRepository.Insert(theLoai);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string XoaTheLoaiSach(List<TheLoai> data)
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
        public string ThemExcel(List<TheLoai> data)
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
    }
}
