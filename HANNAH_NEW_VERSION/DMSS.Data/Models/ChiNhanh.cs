using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("ChiNhanh")]
    public class ChiNhanh
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiNhanh { get; set; }

        [StringLength(500)]
        [Display(Name = "Branch name")]
        public string TenChiNhanh { set; get; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Phone")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Contact")]
        public string NguoiLienHe { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public int? QTVCapNhat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanCapNhatCuoi { get; set; }


    }
}
