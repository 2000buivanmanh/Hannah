using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    public class DaDung
    {
        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LanCapNhatCuoi { get; set; }

        public int? QTVCapNhat { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Description")]
        public string NoiDungSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string TuKhoaSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Title Seo")]
        public string TieuDeSeo { get; set; }

        [StringLength(500)]
        [Display(Name = "Path Seo")]
        public string DuongDanSeo { get; set; }
    }
}
