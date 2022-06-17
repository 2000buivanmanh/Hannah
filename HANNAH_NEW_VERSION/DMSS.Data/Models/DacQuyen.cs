using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("DacQuyen")]
    public class DacQuyen : DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDacQuyen { get; set; }

        [MaxLength(500)]
        [Display(Name = "Special Name")]
        public string TenDacQuyen { get; set; }
        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

       
        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        public string NoiDung { get; set; }

        [MaxLength(500)]
        public string Icon { get; set; }
    }

}
