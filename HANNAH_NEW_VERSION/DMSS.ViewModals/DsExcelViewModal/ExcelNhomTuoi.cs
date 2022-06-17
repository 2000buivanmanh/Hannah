using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal 
{
    [Table("ExcelNhomTuoi")]
    public class ExcelNhomTuoi : ViewModalDaDung
    {
        [Display(Name = "Age min")]
        public string DoTuoiMin { get; set; }

        [Display(Name = "Age max")]
        public string DoTuoiMax { get; set; }
        public string Mota { get; set; }


    }
}
