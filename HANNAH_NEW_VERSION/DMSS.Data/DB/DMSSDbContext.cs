using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DATA.Models;

namespace DATA.DB
{
    public class DMSSDbContext : DbContext
    {
        public DMSSDbContext() : 
            base("DMSS")
        {

        }

        public virtual DbSet<Example> Example { get; set; }
        public virtual DbSet<CaiDat> CaiDat { get; set; }
        public virtual DbSet<Slide> Slide { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanh { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<PhanHoi> PhanHoi { get; set; }
        public virtual DbSet<NhomTuoi> NhomTuoi { get; set; }
        public virtual DbSet<DacQuyen> DacQuyen { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }
        public virtual DbSet<TacGia> TacGia { get; set; }
        public virtual DbSet<LoaiSach> LoaiSach { get; set; }
        public virtual DbSet<DanhGia> DanhGia { get; set; }
        public virtual DbSet<BaiViet> BaiViet { get; set; }
        public virtual DbSet<HangMucSach> HangMucSach { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public virtual DbSet<HinhAnhSach> HinhAnhSach { get; set; }
        public virtual DbSet<DatSach> DatSach { get; set; }
        public virtual DbSet<ChiTietDatSach> ChiTietDatSach { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }
        public virtual DbSet<VideoSach> VideoSach { get; set; }
        public virtual DbSet<ThongBao> ThongBao { get; set; }
        public virtual DbSet<TuKhoa> TuKhoa { get; set; }
        public virtual DbSet<DiaChiXuatBan> DiaChiXuatBan { get; set; }
    }
}
