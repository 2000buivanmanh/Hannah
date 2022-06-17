using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
     public class BaiVietService : IBaiVietService
    {
        private readonly IBaseRepository<BaiViet> _baseRepository;
        public BaiVietService(IBaseRepository<BaiViet> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<BaiViet> DanhSachBaiViet()
        {
            return _baseRepository.GetAll();
        }

        public BaiViet LayBaiVietTheoMa(int maBaiViet)
        {
            return _baseRepository.GetById( maBaiViet);
        }
        public List<BaiViet> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaBaiViet));
        }
        public string SuaBaiViet(BaiViet baiViet)
        {
            try
            {
                var suaBaiViet = _baseRepository.GetById( baiViet.MaBaiViet);
                suaBaiViet.TenBaiViet = baiViet.TenBaiViet;
                suaBaiViet.MoTaNgan = baiViet.MoTaNgan;
                suaBaiViet.NoiDungBaiViet = baiViet.NoiDungBaiViet;
                suaBaiViet.TrangThai = baiViet.TrangThai;
                suaBaiViet.LanCapNhatCuoi = DateTime.Now;
                suaBaiViet.QTVCapNhat = baiViet.QTVCapNhat;
                suaBaiViet.NoiDungSeo = baiViet.NoiDungSeo;
                suaBaiViet.TuKhoaSeo = baiViet.TuKhoaSeo;
                suaBaiViet.TieuDeSeo = baiViet.TieuDeSeo;
                suaBaiViet.DuongDanSeo = baiViet.DuongDanSeo;
                _baseRepository.Update(suaBaiViet);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ThemBaiViet(BaiViet baiViet)
        {
            try
            {
                baiViet.NgayTao = DateTime.Now;
                baiViet.TrangThai = false;
                _baseRepository.Insert(baiViet);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string XoaBaiViet(List<BaiViet> data)
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
        public string ThemExcel(List<BaiViet> data)
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
