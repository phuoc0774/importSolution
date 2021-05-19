using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.model
{

    public class SoTramBTS : IModel
    {
        [NotMapped]
        public object _id
        {
            get { return this.id; }
            set { this.id = (int)(long) value; }
        }

        [Key]
        public int id { get; set; }
        public int? thang { get; set; }
        public int? nam { get; set; }
        public string loai_tram { get; set; }
        public string sitename { get; set; }
        public string sitename_vnp { get; set; }

        //public decimal? long_y { get; set; }
        //public decimal? lat_x { get; set; }

        public string long_y { get; set; }
        public string lat_x { get; set; }

        public DateTime? ngayphatsong { get; set; }
        public DateTime? ngaynhapks { get; set; }
        public string sonha { get; set; }
        public string tenap_vn { get; set; }
        public string tenduong_vn { get; set; }
        public string tenphuong_vn { get; set; }
        public string tenquan_vn { get; set; }
        public int? trang_thai { get; set; }

        //
        public DateTime? ngay_cn { get; set; }
        public int? nguoi_cn { get; set; }
    }


    public class SoTramBTSarr
    {
        public SoTramBTS[] arrs { get; set; }
    }
}
