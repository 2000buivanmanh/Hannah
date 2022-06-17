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
    public class ExcelNhaXuatBan : ViewModalDaDung
    {
        [StringLength(250)]
        [Display(Name = "Imprint")]
        public string TenNhaXuatBan { get; set; }

        [StringLength(500)]
        [Column("ThongTin", TypeName = "nchar")]
        [Display(Name = "Information")]
        public string ThongTinNXB { get; set; }


    }
}
