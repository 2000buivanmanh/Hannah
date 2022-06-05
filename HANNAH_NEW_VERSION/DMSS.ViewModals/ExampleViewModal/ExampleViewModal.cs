using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.ExampleViewModal
{
    public class ExampleViewModal
    {
        [Display(Name = "Khóa chính")]
        public string KhoaChinh { get; set; }

        [Display(Name = "Thuộc tính gốc")]
        public string ThuocTinh { get; set; }

        [Display(Name = "Thuộc tính custom")]
        public string ThuocTinhThem { get; set; }
    }
}
