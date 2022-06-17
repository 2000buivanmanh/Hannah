using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
  public class ExcelSach : ViewModalDaDung
    {
        [StringLength(500)]
        [Display(Name = "Book Name")]
        public string TenSach { get; set; }
        [Display(Name = "Price")]
        public string Gia { get; set; }

        [Display(Name = "Book Description En")]
        public string MoTaSachEn { get; set; }

        [Display(Name = "Book Description Vi")]
        public string MoTaSachVi { get; set; }
        [MaxLength(500)]
        [Display(Name = "Book Pictures")]
        public string ThongTinAnhSach { get; set; }
        [Display(Name = "Number of Pages")]
        public int? SoLuongTrang { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime? NgayXuatBan { get; set; }

        [Display(Name = "First Published")]
        public DateTime? LanDauXuatBan { get; set; }

        [MaxLength(50)]
        [Display(Name = "ISBN")]
        public string MaTieuChuanSach { get; set; }

        [MaxLength(250)]
        [Display(Name = "Book Size")]
        public string KichThuocSach { get; set; }
        [StringLength(250)]
        [Display(Name = "Category name")]
        public string TenTheLoai { get; set; }
        [StringLength(250)]
        [Display(Name = "Imprint")]
        public string TenNhaXuatBan { get; set; }
        [StringLength(250)]
        [Display(Name = "Name of the book")]
        public string TenLoaiSach { get; set; }

        [StringLength(250)]
        [Display(Name = "Age group")]
        public string NhomTuoi { get; set; }
        [Display(Name = "Author name")]
        [MaxLength(250)]
        public string TenTacGia { get; set; }
        [StringLength(500)]
        [Display(Name = "Publishing Address")]
        public string TenDiaChiXB { set; get; }
        [MaxLength(500)]
        [Display(Name = "Picture Book List")]
        public string DanhSachAnh { get; set; }
    }
}
