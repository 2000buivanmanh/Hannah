using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("BaiViet")]
    public class BaiViet
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaBaiViet { get; set; }

        [StringLength(500)]
        [Display(Name = "Post Name")]
        public string TenBaiViet { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Short description")]
        public string MoTaNgan { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        public string NoiDungBaiViet { get; set; }

        [Display(Name = "Date")]
        public DateTime? NgayDang { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

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

        public int? NguoiDang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanCapNhatCuoi { get; set; }

        public int? QTVCapNhat { get; set; }

        public int SoLuotXem { get; set; }
    }
}
