using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("Sach")]
    public class Sach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSach { get; set; }

        [MaxLength(50)]
        public string MaNhanDienSach { get; set; }

        [StringLength(500)]
        public string TenSach { get; set; }

        public decimal? Gia { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaSachEn { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaSachVi { get; set; }

        public int? MaNhomTuoi { get; set; }

        public int? MaTheLoai { get; set; }

        public int? MaTacGia { get; set; }

        public int? MaNhaXuatBan { get; set; }

        [MaxLength(500)]
        public string ThongTinAnhSach { get; set; }

        public int? SoLuongTrang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayXuatBan { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanDauXuatBan { get; set; }

        [MaxLength(500)]
        public string DiaChiXuatBan { get; set; }

        [MaxLength(50)]
        [Display(Name = "ISBN")]
        public string MaTieuChuanSach { get; set; }

        [MaxLength(250)]
        public string KichThuocSach { get; set; }

        [MaxLength(500)]
        public string TinhTrangSach { get; set; }

        public bool? TinhTrangMuonSach { get; set; }

        [Required]
        public bool? TrangThai { get; set; }

        public int? MaLoaiSach { get; set; }


        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanCapNhatCuoi { get; set; }

        public int? QTVCapNhat { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Description")]
        public string NoiDungSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string TuKhoaSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Title Seo")]
        public string TieuDeSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Path Seo")]
        public string DuongDanSeo { get; set; }

        public virtual NhomTuoi NhomTuoi { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual ICollection<HinhAnhSach> HinhAnhSach { get; set; }

        public virtual ICollection<ChiTietDatSach> ChiTietDatSach { get; set; }

        public virtual ICollection<VideoSach> VideoSach { get; set; }

        public virtual LoaiSach LoaiSach { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        public virtual ICollection<DanhGia> DanhGia { get; set; }
    }
}
