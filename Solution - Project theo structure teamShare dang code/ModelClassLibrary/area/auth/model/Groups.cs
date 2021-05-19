using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.area.auth.roles
{
    public class Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int groupid { get; set; }
        public string groupname { get; set; }
        public Boolean active { get; set; }
        public string  note { get; set; }

    }
}
