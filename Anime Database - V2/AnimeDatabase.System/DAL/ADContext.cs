using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity; 
using AnimeDatabase.System.Data;

namespace AnimeDatabase.System.DAL
{
    internal class ADContext :DbContext
    {
        public ADContext() : base("ADContext")
        {

        }
        public DbSet<AnimeDatabase_Anime> Anime { get; set; }
        public DbSet<AnimeDatabase_Description> Description { get; set; }

    }
}
