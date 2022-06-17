using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("BaiViet")]
    public class BaiViet : DaDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaBaiViet { get; set; }

        [StringLength(500)]
        [Display(Name = "Post Name")]
        public string TenBaiViet { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Short description")]
        public string MoTaNgan { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        public string NoiDungBaiViet { get; set; }


        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
        public int SoLuotXem { get; set; }
    }
}
