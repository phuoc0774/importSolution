using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelClassLibrary.area.auth
{
    public class Role
    {
        //[Key]
        //public int roleid { get; set; }
        //public int rolename { get; set; }
        public const string Admin = "1";
        public const string User = "2";
    }
}
