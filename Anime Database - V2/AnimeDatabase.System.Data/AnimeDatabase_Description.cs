using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeDatabase.System.Data
{
    [Table("Description")]
    public class AnimeDatabase_Description
    {
        [Key]
        public int DescriptionID { get; set; }
        public string description { get; set; }
    }
}
