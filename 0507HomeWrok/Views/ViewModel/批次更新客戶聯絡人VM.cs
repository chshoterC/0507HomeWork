using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _0507HomeWrok.Views.ViewModel
{
    public class 批次更新客戶聯絡人VM
    {
        public int Id { get; set; }

        [Required]
        public string 姓名 { get; set; }

        [Required]
        public string 職稱 { get; set; }

        [Required]
        public string 手機 { get; set; }

        [Required]
        public string 電話 { get; set; }
    }
}