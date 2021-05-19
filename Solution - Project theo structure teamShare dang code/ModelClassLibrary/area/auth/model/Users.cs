using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.area.model
{
    public class Users : IModel
    {
        [NotMapped]
        public object _id
        {
            get { return this.usid; }
            set { int i = (int)(long)value;  usid = i; }
        }

        [Key]
        public int? usid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int role { get; set; }
    }
    
}
