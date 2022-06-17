using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("NhomTuoi")]
    public class NhomTuoi : DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Age group")]
        public int MaNhomTuoi { get; set; }

        [Display(Name = "Age min")]
        public int? DoTuoiMin { get; set; }

        [Display(Name = "Age max")]
        public int? DoTuoiMax { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

     
    }
}
