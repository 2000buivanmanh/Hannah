using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("ThongBao")]
    public class ThongBao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MaThongBao { get; set; }

        public int MaNguoiDung { get; set; }
        public int MaDonSach { get; set; }

        public int MaDanhGia { get; set; }

        public int MaPhanHoi { get; set; }

        public string LoaiThongBao { get; set; }

        public bool? DaXem { get; set; }

        public DateTime? NgayHien { get; set; }
    }
}
