using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("PhanHoi")]
    public class PhanHoi
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhanHoi { get; set; }

        [Display(Name = "Data")]
        public DateTime? NgayGui { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        [Display(Name = "Name")]
        public string TenNguoiGui { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(500)]
        [Display(Name = "Content")]
        public string NoiDungPhanHoi { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

        public int? MaNguoiDung { get; set; }

        public DateTime? NgayXuLy { get; set; }

        [StringLength(500)]
        public string NoiDungXuLy { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
