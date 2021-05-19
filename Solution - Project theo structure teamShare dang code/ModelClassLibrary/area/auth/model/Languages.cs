
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelClassLibrary.area.auth.roles
{
    public class Languages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int langid { get; set; }
        public string langname { get; set; }
        public Boolean active { get; set; }
        public string languages { get; set; }
    }
}
