using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("ChiTietDatSach")]
    public class ChiTietDatSach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietDatSach { get; set; }

        public int? MaDonSach { get; set; }

        public int? MaSach { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual DatSach DatSach { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
