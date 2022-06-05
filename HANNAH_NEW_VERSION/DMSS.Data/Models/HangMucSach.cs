using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("HangMucSach")]
    public class HangMucSach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHangMucSach { get; set; }

        [MaxLength(50)]
        [Display(Name = "Book Class Code")]
        public string MaNhanDienHangMucSach { get; set; }

        [MaxLength(250)]
        [Display(Name = "Book Class Name")]
        public string TenHangMuc { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanCapNhatCuoi { get; set; }

        public int? QTVCapNhat { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string TuKhoaSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string NoiDungSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Title Seo")]
        public string TieuDeSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Path Seo")]
        public string DuongDanSeo { get; set; }
    }
}
