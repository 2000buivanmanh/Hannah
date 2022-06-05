using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("VideoSach")]
    public class VideoSach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaVideo { get; set; }

        public int? MaSach { get; set; }

        [MaxLength(500)]
        public string TenVideo { get; set; }

        public int? LoaiVideo { get; set; }

        [MaxLength(500)]
        public string TieuDeDanhGia { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDungDanhGia { get; set; }

        [MaxLength(250)]
        public string EmailDanhGia { get; set; }

        public DateTime? NgayGuiDanhGia { get; set; }

        public bool? TinhTrangDanhGia { get; set; }

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

        public virtual Sach Sach { get; set; }
    }
}
