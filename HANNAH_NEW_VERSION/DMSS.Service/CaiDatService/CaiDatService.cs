using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
   public class CaiDatService : ICaiDatService
    {
        private readonly IBaseRepository<CaiDat> _baseRepository;
        public CaiDatService(IBaseRepository<CaiDat> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public CaiDat LayThongTinWeb()
        {
            return _baseRepository.GetAll().FirstOrDefault();
        }
        public List<CaiDat> DanhSachCaiDat()
        {
            return _baseRepository.GetAll();
        }

        public bool CapNhatThongTinWeb(CaiDat caiDat)
        {
            try
            {
                var capNhatCaiDat = _baseRepository.GetById(caiDat.MaCaiDat);
                capNhatCaiDat.SDTLienHe = caiDat.SDTLienHe;
                capNhatCaiDat.DiaChiCuaHang = caiDat.DiaChiCuaHang;
                capNhatCaiDat.EmailCuaHang = caiDat.EmailCuaHang;
                capNhatCaiDat.FaceBook = caiDat.FaceBook;
                capNhatCaiDat.Youtube = caiDat.Youtube;
                capNhatCaiDat.Twitter = caiDat.Twitter;
                capNhatCaiDat.Zalo = caiDat.Zalo;
                capNhatCaiDat.Slogan = caiDat.Slogan;
                capNhatCaiDat.EmailGoiMail = caiDat.EmailGoiMail;
                capNhatCaiDat.MatKhauEmail = caiDat.MatKhauEmail;
                capNhatCaiDat.Logo = caiDat.Logo;
                capNhatCaiDat.BanDo = caiDat.BanDo;
                capNhatCaiDat.TieuDeWebSite = caiDat.TieuDeWebSite;
                capNhatCaiDat.MoTaNgan = caiDat.MoTaNgan;
                capNhatCaiDat.GioiThieuHinhAnhWebsite = caiDat.GioiThieuHinhAnhWebsite;
                capNhatCaiDat.ChanWebSite = caiDat.ChanWebSite;
                capNhatCaiDat.GioiThieuHinhAnhWebsite = caiDat.GioiThieuHinhAnhWebsite;
                capNhatCaiDat.DiemMacDinh = caiDat.DiemMacDinh;
                capNhatCaiDat.DieuKhoan = caiDat.DieuKhoan;
                capNhatCaiDat.DiemTichLuy = caiDat.DiemTichLuy;
                capNhatCaiDat.DiemDanhGia = caiDat.DiemDanhGia;
                capNhatCaiDat.NoiDungSeo = caiDat.NoiDungSeo;
                capNhatCaiDat.TuKhoaSeo = caiDat.TuKhoaSeo;
                capNhatCaiDat.TieuDeSeo = caiDat.TieuDeSeo;
                capNhatCaiDat.DuongDanSeo = caiDat.DuongDanSeo;
                capNhatCaiDat.SoLanGuiMa = caiDat.SoLanGuiMa;
                capNhatCaiDat.SoLanXacNhanSai = caiDat.SoLanXacNhanSai;
                capNhatCaiDat.IconWebSite = caiDat.IconWebSite;
                capNhatCaiDat.ThoiHanMaXacNhan = caiDat.ThoiHanMaXacNhan;
                capNhatCaiDat.ThoiGianHetHanTaiKhoan = caiDat.ThoiGianHetHanTaiKhoan;
                _baseRepository.Update(capNhatCaiDat);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public CaiDat LayMaCaiDat(int maCaiDat)
        {
            return _baseRepository.Table.SingleOrDefault(s => s.MaCaiDat == maCaiDat);
        }
    }
}
