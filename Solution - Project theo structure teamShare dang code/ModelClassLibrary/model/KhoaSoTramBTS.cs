using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.model
{
    public class KhoaSoTramBTS : IModel
    {
        [NotMapped]
        public object _id
        {
            get { return this.id; }
            set { this.id = (int)(long)value; }
        }

        [Key]
        public int id { get; set; }
        public int? thang { get; set; }
        public int? nam { get; set; }
        public int? trang_thai { get; set; }

        //
        public DateTime? ngay_cn { get; set; }
        public int? nguoi_cn { get; set; }
    }
}
