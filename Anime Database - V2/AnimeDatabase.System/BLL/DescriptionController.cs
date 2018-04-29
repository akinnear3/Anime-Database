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
   public class DescriptionController
    {
        //search all
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<AnimeDatabase.System.Data.AnimeDatabase_Description> ListAll_Descriptions()
        {
            using (var context = new ADContext())
            {
                IEnumerable<AnimeDatabase.System.Data.AnimeDatabase_Description> results = context.Database.SqlQuery<AnimeDatabase.System.Data.AnimeDatabase_Description>("FindAllRatings");
                return results.ToList();
            }
        }
        public List<AnimeDatabase_Description> FindAll_Descriptions()
        {
            using (var context = new ADContext())
            {
                return context.Description.ToList();
            }
        }
        //add new
        public Tuple<string, int> AnimeDatabase_Add_Description(AnimeDatabase_Description newDescription)
        {
            using (var context = new ADContext())
            {
                var newID = context.Description.Add(newDescription);
                context.SaveChanges();
                return new Tuple<string, int>(newDescription.description, newID.DescriptionID);
            }
        }

        //update
        public Tuple<string, int> AnimeDatabase_Update_Description(AnimeDatabase_Description updatedDescription)
        {
            using (var context = new ADContext())
            {
                context.Entry(updatedDescription).State = EntityState.Modified;
                return new Tuple<string, int>(updatedDescription.description, context.SaveChanges());
            }
        }

        //delete
        public Tuple<string, int> AnimeDatabase_Delete_Description(int DescriptionID)
        {
            using (var context = new ADContext())
            {
                var Description = context.Description.Find(DescriptionID);
                if (Description == null)
                {
                    throw new Exception("Description Not Found. Please sselect another Description.");
                }
                context.Description.Remove(Description);
                return new Tuple<string, int>(Description.description, context.SaveChanges());
            }
        }
    }
}
