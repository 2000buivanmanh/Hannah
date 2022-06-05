using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelTheLoaiSach")]
    public class ExcelTheLoaiSach
    {

        [StringLength(250)]
        [Display(Name = "Category name")]
        public string TenTheLoai { get; set; }
        [StringLength(10)]
        [Display(Name = "Category class")]
        [Column("HangMuc", TypeName = "nchar")]
        public string HangMuc { get; set; }

        [StringLength(250)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [StringLength(10)]
        [Display(Name = "Book class name")]
        public string MaNhanDienHangMucSach { get; set; }

        [StringLength(250)]
        [Display(Name = "Icon")]
        public string Icon { get; set; }
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
        [Display(Name = "Notify")]
        public string ThongBao { get; set; }
        [Display(Name = "#")]
        public int? Stt { get; set; }
    }
}
