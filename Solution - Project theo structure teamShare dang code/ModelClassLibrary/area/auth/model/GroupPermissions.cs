using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.area.auth.roles
{
    public class GroupPermissions
    {
        public int groupid { get; set; }
        public int perid { get; set; }
        public DateTime createday { get; set; }
    }
}
