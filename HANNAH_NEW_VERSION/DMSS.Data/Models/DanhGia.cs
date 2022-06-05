using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDanhGia { get; set; }

        public int? MaNguoiDung { get; set; }

        [StringLength(500)]
        public string NoiDungDanhGia { get; set; }

        public DateTime? NgayDanhGia { get; set; }

        public int? DiemDanhGia { get; set; }

        public int? MaSach { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
