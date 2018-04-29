using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeDatabase.System.Data
{
    [Table("Anime")]
    public class AnimeDatabase_Anime
    {
        [Key]
        public int AnimeID { get; set; }
        public string AnimeName { get; set; }
        public int? TotalNumberOfEpisodes { get; set; }
        public int? CurrentEpisode { get; set; }
        public int? NumberOfSeasons { get; set; }
        public int AnimeDescriptionID { get; set; }
    }
}
