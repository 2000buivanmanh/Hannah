using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DATA.Models
{
    [Table("CaiDat")]
    public class CaiDat
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCaiDat { get; set; }

        [StringLength(10)]
        public string SDTLienHe { get; set; }

        [StringLength(500)]
        public string DiaChiCuaHang { get; set; }

        [StringLength(250)]
        public string EmailCuaHang { get; set; }

        [StringLength(250)]
        public string FaceBook { get; set; }

        [StringLength(250)]
        public string Youtube { get; set; }

        [StringLength(250)]
        public string Twitter { get; set; }

        [StringLength(250)]
        public string Zalo { get; set; }

        [StringLength(500)]
        public string Slogan { get; set; }

        [StringLength(500)]
        public string EmailGoiMail { get; set; }

        [StringLength(500)]
        public string MatKhauEmail { get; set; }

        [StringLength(500)]
        public string Logo { get; set; }

        public string BanDo { get; set; }

        public string MaNhungBanDo { get; set; }

        [StringLength(500)]
        public string TieuDeWebSite { get; set; }

        public string MoTaNgan { get; set; }

        public string GioiThieuWebSite { get; set; }

        public string ChanWebSite { get; set; }

        public string GioiThieuHinhAnhWebsite { get; set; }

        public int? DiemMacDinh { get; set; }

        public string DieuKhoan { get; set; }

        public int? DiemTichLuy { get; set; }

        public int? DiemDanhGia { get; set; }

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

        public int? SoLanGuiMa { get; set; }

        public int? SoLanXacNhanSai { get; set; }

        [StringLength(500)]
        public string IconWebSite { get; set; }

        [NotMapped]
        public HttpPostedFileBase HinhAnh { get; set; }

        public int ThoiHanMaXacNhan { get; set; }

        public int ThoiGianHetHanTaiKhoan { get; set; }
    }
}
