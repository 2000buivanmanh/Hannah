using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("DiaChiXuatBan")]
    public class DiaChiXuatBan : DaDung
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDiaChi { get; set; }

        [StringLength(500)]
        [Display(Name = "Publishing Address")]
        public string TenDiaChi { set; get; }
        [StringLength(500)]
        [Display(Name = "Detailed Address")]
        public string ChiTietDiaChi { set; get; }
    }
}
