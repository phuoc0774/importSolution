using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelClassLibrary.area.auth.roles
{
    public class PermissionTranslations
    {
        [Key]
        public int transid { get; set; }
        public int perid { get; set; }
        public string languages { get; set; }
        public string pername { get; set; }
    }
}
