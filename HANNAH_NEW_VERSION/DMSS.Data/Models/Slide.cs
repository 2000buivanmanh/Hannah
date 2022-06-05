using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("Slide")]
    public class Slide
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSlide { get; set; }

        [StringLength(500)]
        [Display(Name = "Name")]
        public string TenSlide { get; set; }

        [StringLength(500)]
        [Display(Name = "Image")]
        public string AnhSlide { get; set; }

        [StringLength(500)]
        [Display(Name = "Short Description")]
        public string MoTa { get; set; }

        [StringLength(500)]
        [Display(Name = "Title")]
        public string TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        public string NoiDung { get; set; }

        [Display(Name = "Display Title")]
        public bool? TieuDeHienThi { get; set; }

        [Display(Name = "Display Content")]
        public bool? NoiDungHienThi { get; set; }

        [Display(Name = "Display Description")]
        public bool? MoTaHienThi { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
    }
}
