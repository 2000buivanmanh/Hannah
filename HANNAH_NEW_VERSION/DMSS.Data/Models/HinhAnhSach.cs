using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("HinhAnhSach")]
    public class HinhAnhSach
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHinhAnhSach { get; set; }

        public int? MaSach { get; set; }

        [MaxLength(500)]
        public string TenAnh { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
