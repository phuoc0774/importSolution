using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.area.auth.model
{
    public class Roles : IModel
    {
        [NotMapped]
        public object _id
        {
            get { return this.roleid; }
            set { roleid = (int) value; } 
        }

        [Key]
        public int roleid { get; set; }
        public string rolename { get; set; }
    }
}
