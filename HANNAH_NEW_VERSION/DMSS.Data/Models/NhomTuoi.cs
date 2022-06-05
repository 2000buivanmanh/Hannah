﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Models
{
    [Table("NhomTuoi")]
    public class NhomTuoi
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhomTuoi { get; set; }

        [Display(Name = "Age min")]
        public int? DoTuoiMin { get; set; }

        [Display(Name = "Age max")]
        public int? DoTuoiMax { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description")]
        public string MoTa { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }

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
