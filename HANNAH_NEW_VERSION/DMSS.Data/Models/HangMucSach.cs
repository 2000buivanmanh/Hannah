using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("HangMucSach")]
    public class HangMucSach : DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHangMucSach { get; set; }

        [MaxLength(50)]
        [Display(Name = "Book Class Code")]
        public string MaNhanDienHangMucSach { get; set; }

        [MaxLength(250)]
        [Display(Name = "Book Class Name")]
        public string TenHangMuc { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }


    }
}
