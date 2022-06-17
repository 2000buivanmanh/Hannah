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
    public class Sach : DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSach { get; set; }

        [MaxLength(50)]
        [Display(Name = "Book ID")]
        public string MaNhanDienSach { get; set; }

        [StringLength(500)]
        [Display(Name = "Book Name")]
        public string TenSach { get; set; }
        [Display(Name = "Price")]
        public decimal? Gia { get; set; }
        [Column( TypeName = "ntext")]
        [Display(Name = "Book Description En")]
        public string MoTaSachEn { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name = "Book Description Vi")]
        
        public string MoTaSachVi { get; set; }

        public int? MaNhomTuoi { get; set; }

        public int? MaTheLoai { get; set; }
        public int? MaDiaChi { get; set; }

        public int? MaTacGia { get; set; }

        public int? MaNhaXuatBan { get; set; }

        [MaxLength(500)]
        [Display(Name = "Book Pictures")]
        public string ThongTinAnhSach { get; set; }
        [Display(Name = "Number of Pages")]
        public int? SoLuongTrang { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Publication Date" )]
        public DateTime? NgayXuatBan { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "First Published")]
        public DateTime? LanDauXuatBan { get; set; }

        [MaxLength(50)]
        [Display(Name = "ISBN")]
        public string MaTieuChuanSach { get; set; }

        [MaxLength(250)]
        [Display(Name = "Book Size")]
        public string KichThuocSach { get; set; }

        [Display(Name = "Book Status")]
        public int? TinhTrangSach { get; set; }
        public bool? TinhTrangMuonSach { get; set; }
        [Display(Name = "Status")]
        [Required]

        public bool? TrangThai { get; set; }

        public int? MaLoaiSach { get; set; }

        public virtual NhomTuoi NhomTuoi { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual ICollection<HinhAnhSach> HinhAnhSach { get; set; }

        public virtual ICollection<ChiTietDatSach> ChiTietDatSach { get; set; }

        public virtual ICollection<VideoSach> VideoSach { get; set; }

        public virtual LoaiSach LoaiSach { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }
        public virtual DiaChiXuatBan DiaChiXuatBan { get; set; }

        public virtual ICollection<DanhGia> DanhGia { get; set; }
    }
}
