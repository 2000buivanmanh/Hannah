using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTheLoai { get; set; }

        [StringLength(250)]
        [Display(Name = "Category name")]
        public string TenTheLoai { get; set; }

        [StringLength(10)]
        [Display(Name = "Category class")]
        [Column("HangMuc", TypeName = "nchar")]
        public string HangMuc { get; set; }

        [StringLength(250)]
        [Display(Name = "Icon")]
        public string Icon { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

        [StringLength(250)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [StringLength(10)]
        [Display(Name = "Book class name")]
        public string MaNhanDienHangMucSach { get; set; }

        public int? KeSach { get; set; }

        public int? NganSach { get; set; }

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
    }
}
