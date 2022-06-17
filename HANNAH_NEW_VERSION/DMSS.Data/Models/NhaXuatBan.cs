using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("NhaXuatBan")]
    public class NhaXuatBan :DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhaXuatBan { get; set; }

        [StringLength(250)]
        [Display(Name = "Imprint")]
        public string TenNhaXuatBan { get; set; }

        [StringLength(500)]
        [Column("ThongTin", TypeName = "nchar")]
        [Display(Name = "Information")]
        public string ThongTinNXB { get; set; }
        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
    }
}
