using AnimeDatabase.System.BLL;
using AnimeDatabase.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnimeDatabase_AddAnime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message.Text = "";
    }
    protected Exception GetInnerException(Exception ex)
    {
        //drill down to the inner most exception
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }
        return ex;
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if(Rating.SelectedIndex == 0)
            {
                Message.Text = "Invalid Rating. Please select a rating";
            }
            else
            {
                try
                {
                    AnimeDatabase_Anime newAnime = CreateAnime();
                    AnimeController sysmgr = new AnimeController();
                    Tuple<string, int> results = sysmgr.AnimeDatabase_Add_Anime(newAnime);
                    Message.Text = "the anime '" + results.Item1 + "' has been sucessfully added with ID " + results.Item2.ToString(); 

                }
                catch (Exception ex)
                {
                    Message.Text = GetInnerException(ex).ToString();
                }
                

            }
        }
    }

    private AnimeDatabase_Anime CreateAnime()
    {
        AnimeDatabase_Anime newAnime = new AnimeDatabase_Anime();
        newAnime.AnimeName = AnimeName.Text;
        newAnime.CurrentEpisode = string.IsNullOrEmpty(CurrentlyOn.Text)? 0 : int.Parse(CurrentlyOn.Text);
        newAnime.TotalNumberOfEpisodes = string.IsNullOrEmpty(NumberOfEpisodes.Text) ? 0 : int.Parse(NumberOfEpisodes.Text);
        newAnime.NumberOfSeasons = string.IsNullOrEmpty(NumberOfSeasons.Text) ? 0 : int.Parse(NumberOfSeasons.Text);
        newAnime.AnimeDescriptionID =  int.Parse(Rating.SelectedValue);
        return newAnime;
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        AnimeName.Text = "";
        NumberOfEpisodes.Text = "";
        CurrentlyOn.Text = "";
        NumberOfSeasons.Text = "";
        Rating.SelectedIndex = 0;
    }
}