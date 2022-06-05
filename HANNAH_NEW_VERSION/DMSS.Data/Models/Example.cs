using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("BangViDu")]
    public class Example
    {
        [Key]
        public string KhoaChinh { get; set; }

        public string ThuocTinh { get; set; }
    }
}
