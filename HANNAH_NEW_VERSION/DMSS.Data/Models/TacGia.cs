using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("TacGia")]
    public class TacGia :DaDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTacGia { get; set; }

        [Required]
        [Display(Name = "Author name")]
        [MaxLength(250)]
        public string TenTacGia { get; set; }

        [Display(Name = "Infomation")]
        [MaxLength(500)]
        public string ThongTinTacGia { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
    }
}
