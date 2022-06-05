using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("DatSach")]
    public class DatSach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDonSach { get; set; }

        [MaxLength(100)]
        public string MaHoaDonSach { get; set; }

        public int? MaNguoiDung { get; set; }

        public int? TinhTrangDonHang { get; set; }

        [MaxLength(500)]
        public string GhiChu { get; set; }

        public DateTime? NgayDat { get; set; }

        public DateTime? NgayTiepNhan { get; set; }

        public DateTime? NgayGui { get; set; }

        public DateTime? NgayNhan { get; set; }

        public DateTime? NgayTra { get; set; }

        [MaxLength(500)]
        public string DiaChiNhan { get; set; }

        [MaxLength(500)]
        public string DiaChiTra { get; set; }

        public int? NguoiGiaiQuyet { get; set; }

        public virtual ICollection<ChiTietDatSach> ChiTietDatSach { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
