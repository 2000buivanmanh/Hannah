using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("TuKhoa")]
    public class TuKhoa
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTuKhoa { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string TenTuKhoa { get; set; }

        [StringLength(500)]
        [Display(Name = "Tag")]
        public string ChuDe { get; set; }
    }
}
