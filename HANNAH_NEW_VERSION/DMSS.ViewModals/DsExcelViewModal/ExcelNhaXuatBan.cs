using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelNhaXuatBan")]
    public class ExcelNhaXuatBan
    {
        [StringLength(250)]
        [Display(Name = "Imprint")]
        public string TenNhaXuatBan { get; set; }

        [StringLength(500)]
        [Column("ThongTin", TypeName = "nchar")]
        [Display(Name = "Information")]
        public string ThongTinNXB { get; set; }
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
        [Display(Name = "Notify")]
        public string ThongBao { get; set; }
        [Display(Name = "#")]
        public int? Stt { get; set; }
    }
}
