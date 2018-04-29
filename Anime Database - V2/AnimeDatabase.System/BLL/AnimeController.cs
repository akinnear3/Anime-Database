using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using AnimeDatabase.System.DAL;
using AnimeDatabase.System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace AnimeDatabase.System.BLL
{
    [DataObject]
    public class AnimeController
    {
        //return all items
        public List<AnimeDatabase_Anime> AnimeDatabase_ListAll_Anime()
        {
            using (var context = new ADContext())
            {
                return context.Anime.ToList();
            }
        }
        //return items with restriction
        [DataObjectMethod(DataObjectMethodType.Select)]
        ///<summary>
        ///
        /// </summary>
        /// <param name="Name">is the partial name of the search</param>
        /// <param name="RatingID">is the rating number that is used in the search</param>
        /// <returns>the list of matching anime</returns>
        public List<AnimeDatabase_Anime> AnimeDatabase_Specific_Anime(int RatingID, string Name)
        {
            using (var context = new ADContext())
            {
                IEnumerable<AnimeDatabase_Anime> results = context.Database.SqlQuery<AnimeDatabase_Anime>("SearchAnimeByRatingAndName @RatingID, @Name",
                    new SqlParameter("RatingID", RatingID), new SqlParameter("Name", Name));
                return results.ToList();
            }
        }

       
        //add new
        public Tuple<string, int> AnimeDatabase_Add_Anime(AnimeDatabase_Anime newAnime)
        {
            using (var context = new ADContext())
            {
                var newID = context.Anime.Add(newAnime);
                context.SaveChanges();
                return new Tuple<string, int>(newAnime.AnimeName, newID.AnimeID);
            }
        }

        //update
        public Tuple<string, int> AnimeDatabase_Update_Anime(AnimeDatabase_Anime newAnime)
        {
            using (var context = new ADContext())
            {
                context.Entry(newAnime).State = EntityState.Modified;
                return new Tuple<string, int>(newAnime.AnimeName, context.SaveChanges());
            }
        }

        //delete
        public Tuple<string, int> AnimeDatabase_Delete_Anime(int AnimeID)
        {
            using (var context = new ADContext())
            {
                var anime = context.Anime.Find(AnimeID);
                if(anime == null)
                {
                    throw new Exception("Anime Not Found. Please select another anime.");
                }
                context.Anime.Remove(anime);
                return new Tuple<string, int>(anime.AnimeName, context.SaveChanges());
            }
        }
    }
}
