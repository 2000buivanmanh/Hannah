using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class NhomTuoiService : INhomTuoiService 
    {
        private readonly IBaseRepository<NhomTuoi> _baseRepository;
        public NhomTuoiService(IBaseRepository<NhomTuoi> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<NhomTuoi> DanhSachNhomTuoi()
        {
            return _baseRepository.GetAll();
        }
        public List<NhomTuoi> LayDanhSachMa(int[] data)
        {
            return _baseRepository.GetAll(s => data.Contains(s.MaNhomTuoi));
        }
        public NhomTuoi LayNhomTuoiTheoMa(int maNhomTuoi)
        {
            return _baseRepository.GetById( maNhomTuoi);
        }
        public string SuaNhomTuoi(NhomTuoi nhomTuoi)
        {
            try
            {
                var suaNhomTuoi = _baseRepository.GetById( nhomTuoi.MaNhomTuoi);
                suaNhomTuoi.DoTuoiMin = nhomTuoi.DoTuoiMin;
                suaNhomTuoi.DoTuoiMax = nhomTuoi.DoTuoiMax;
                suaNhomTuoi.MoTa = nhomTuoi.MoTa;
                suaNhomTuoi.TrangThai = nhomTuoi.TrangThai;
                suaNhomTuoi.LanCapNhatCuoi = DateTime.Now;
                suaNhomTuoi.QTVCapNhat = nhomTuoi.QTVCapNhat;
                suaNhomTuoi.NoiDungSeo = nhomTuoi.NoiDungSeo;
                suaNhomTuoi.TuKhoaSeo = nhomTuoi.TuKhoaSeo;
                suaNhomTuoi.TieuDeSeo = nhomTuoi.TieuDeSeo;
                suaNhomTuoi.DuongDanSeo = nhomTuoi.DuongDanSeo;
                _baseRepository.Update(suaNhomTuoi);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ThemNhomTuoi(NhomTuoi nhomTuoi)
        {
            try
            {
                nhomTuoi.NgayTao = DateTime.Now;
                nhomTuoi.TrangThai = false;
                _baseRepository.Insert(nhomTuoi);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string XoaNhomTuoi(List<NhomTuoi> data)
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
        public string ThemExcel(List<NhomTuoi> data)
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

        public NhomTuoi KiemTraDoTuoi(int? doTuoiMin, int? doTuoiMax)
        {
            return  _baseRepository.GetAll(s => (s.DoTuoiMin == doTuoiMin && s.DoTuoiMax == doTuoiMax)).FirstOrDefault();

        }
    }
}
