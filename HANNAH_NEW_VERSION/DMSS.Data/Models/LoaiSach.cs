using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("LoaiSach")]
    public class LoaiSach :DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoaiSach { get; set; }

        [StringLength(250)]
        [Display(Name = "Name of the book")]
        public string TenLoaiSach { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
      
    }
}
