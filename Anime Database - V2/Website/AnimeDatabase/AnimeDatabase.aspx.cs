using AnimeDatabase.System.BLL;
using AnimeDatabase.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnimeDatabase_AnimeDatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        { 
            #region togle button efffects in gridview...
            foreach (GridViewRow row in Anime_GridView.Rows)
            {
                //this finds the control...
                var AnimeIDControl = (TextBox)row.FindControl("AnimeID");
                var AnimeNameControl = (TextBox)row.FindControl("AnimeName");
                var CurrentEpisodeControl = (TextBox)row.FindControl("CurrentEpisode");
                var NumberOfEpisodesControl = (TextBox)row.FindControl("EpisodeCount");
                var NumberOfSeasonsControl = (TextBox)row.FindControl("NumberOfSeasons");
                var DescriptionIDControl = (TextBox)row.FindControl("DescriptionID");
                var DescriptionTextBoxControl = (TextBox)row.FindControl("DescriptionTextBox");
                var DescriptionDropDownControl = (DropDownList)row.FindControl("DescriptionDropDownList");

                var EditButtonControl = (Button)row.FindControl("EditButton");
                var UpdateButtonControl = (Button)row.FindControl("UpdateButton");
                var DeleteButtonControl = (Button)row.FindControl("DeleteButton");
                var CancelButtonControl = (Button)row.FindControl("CancelButton");

                //stop editing...
                AnimeNameControl.ReadOnly = true;
                CurrentEpisodeControl.ReadOnly = true;
                NumberOfEpisodesControl.ReadOnly = true;
                NumberOfSeasonsControl.ReadOnly = true;

                //show / hide buttons and feilds...
                DescriptionTextBoxControl.Visible = true;
                DescriptionDropDownControl.Visible = false;

                EditButtonControl.Visible = true;
                UpdateButtonControl.Visible = false;
                DeleteButtonControl.Visible = true;
                CancelButtonControl.Visible = false;
            }
            #endregion
        }
        else
        {
            BindAnime();
        }
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
    protected void BindAnime()
    {
        try
        {
            AnimeController sysmgr = new AnimeController();
            List<AnimeDatabase_Anime> info = sysmgr.AnimeDatabase_Specific_Anime(int.Parse(RatingParameter.SelectedValue), TextParameter.Text);
            info.Sort((x, y) => x.AnimeName.CompareTo(y.AnimeName));
            Anime_GridView.DataSource = info;
            Anime_GridView.DataBind();

        }
        catch (Exception ex)
        {
            Message.Text = GetInnerException(ex).ToString();
        }

        try
        {
            DescriptionController sysmgr = new DescriptionController();
            List<AnimeDatabase.System.Data.AnimeDatabase_Description> ListOfDescriptions = sysmgr.ListAll_Descriptions();

            //configure Description textbox to show description insted of ID...
            foreach (GridViewRow row in Anime_GridView.Rows)
            {
                var DescriptionIDControl = (TextBox)row.FindControl("DescriptionID");
                var DescriptionTextBoxControl = (TextBox)row.FindControl("DescriptionTextBox");

                bool found = false;
                for (int i = 0; i < ListOfDescriptions.Count() && found == false; i++)
                {
                    if (DescriptionIDControl.Text == ListOfDescriptions[i].DescriptionID.ToString())
                    {
                        DescriptionTextBoxControl.Text = ListOfDescriptions[i].description;
                        found = true;
                    }
                }
            }
            
        }
        catch(Exception ex)
        {
            Message.Text = GetInnerException(ex).ToString();
        }
        
    }
    
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(TextParameter.Text) && RatingParameter.SelectedIndex == 0)
        {
            Message.Text = "all anime shown";
        }
        BindAnime();
        if (Anime_GridView.Columns.Count == 0)
        {
            Message.Text = "No anime Found";
        }

    }
    protected void AddAnimeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AnimeDatabase/AddAnime.aspx");

    }
    protected void ManageRatingsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AnimeDatabase/Description.aspx");

    }

    protected void Anime_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Anime_GridView.PageIndex = e.NewPageIndex;
        BindAnime();

    }

    protected void Anime_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Find Controls
        if (!string.IsNullOrEmpty(e.CommandName) && e.CommandName.ToUpper() != "PAGE")
        {
            //collect the row number / index number
            int index = Convert.ToInt32(e.CommandArgument);

            //this retreives the row itself...
            GridViewRow row = Anime_GridView.Rows[index];

            //this finds the control...
            var AnimeIDControl = (TextBox)row.FindControl("AnimeID");
            var AnimeNameControl = (TextBox)row.FindControl("AnimeName");
            var CurrentEpisodeControl = (TextBox)row.FindControl("CurrentEpisode");
            var NumberOfEpisodesControl = (TextBox)row.FindControl("EpisodeCount");
            var NumberOfSeasonsControl = (TextBox)row.FindControl("NumberOfSeasons");
            var DescriptionIDControl = (TextBox)row.FindControl("DescriptionID");
            var DescriptionTextBoxControl = (TextBox)row.FindControl("DescriptionTextBox");
            var DescriptionDropDownControl = (DropDownList)row.FindControl("DescriptionDropDownList");

            var EditButtonControl = (Button)row.FindControl("EditButton");
            var UpdateButtonControl = (Button)row.FindControl("UpdateButton");
            var DeleteButtonControl = (Button)row.FindControl("DeleteButton");
            var CancelButtonControl = (Button)row.FindControl("CancelButton");
            #endregion

            //allows editing to take place
            if (e.CommandName == "Edit Anime")
            {
                #region Edit Anime
                //allow editing...
                AnimeNameControl.ReadOnly = false;
                CurrentEpisodeControl.ReadOnly = false;
                NumberOfEpisodesControl.ReadOnly = false;
                NumberOfSeasonsControl.ReadOnly = false;

                //show / hide buttons and feilds...
                DescriptionTextBoxControl.Visible = false;
                DescriptionDropDownControl.Visible = true;

                EditButtonControl.Visible = false;
                UpdateButtonControl.Visible = true;
                DeleteButtonControl.Visible = false;
                CancelButtonControl.Visible = true;

                //setup drop down list...
                BindDescriptionsDDL_In_WebGrid(sender, e);
                #endregion

            }
            //delete the anime
            else if (e.CommandName == "Delete Anime")
            {
                #region Delete Anime

                if (string.IsNullOrEmpty(AnimeIDControl.Text))
                {
                    Message.Text = "Anime No Longer in existence, please select another anime.";
                    BindAnime();
                }
                try
                {
                    AnimeController sysmgr = new AnimeController();
                    var results = sysmgr.AnimeDatabase_Delete_Anime(int.Parse(AnimeIDControl.Text));
                    if (results.Item2 == 0)
                    {
                        Message.Text = "Delete Failed";
                    }
                    else
                    {
                        Message.Text = "the anime '" + results.Item1 + "' has been sucessfuly deleted";
                    }
                    BindAnime();

                }
                catch (Exception ex)
                {
                    Message.Text = GetInnerException(ex).ToString();
                }
                #endregion
            }
            //updates the anime
            else if (e.CommandName == "Save Changes")
            {
                #region Save changes / update Anime
                //stop editing...
                AnimeNameControl.ReadOnly = true;
                CurrentEpisodeControl.ReadOnly = true;
                NumberOfEpisodesControl.ReadOnly = true;
                NumberOfSeasonsControl.ReadOnly = true;

                //show / hide buttons and feilds...
                DescriptionTextBoxControl.Visible = true;
                DescriptionDropDownControl.Visible = false;

                EditButtonControl.Visible = true;
                UpdateButtonControl.Visible = false;
                DeleteButtonControl.Visible = true;
                CancelButtonControl.Visible = false;

                try
                {
                    AnimeDatabase_Anime UpdatedAnime = new AnimeDatabase_Anime();

                    #region Load Anime Data
                    UpdatedAnime.AnimeID = int.Parse(AnimeIDControl.Text);
                    UpdatedAnime.AnimeName = AnimeNameControl.Text;
                    UpdatedAnime.CurrentEpisode = string.IsNullOrEmpty(CurrentEpisodeControl.Text) ? 0 : int.Parse(CurrentEpisodeControl.Text);
                    UpdatedAnime.TotalNumberOfEpisodes = string.IsNullOrEmpty(NumberOfEpisodesControl.Text) ? 0 : int.Parse(NumberOfEpisodesControl.Text);
                    UpdatedAnime.NumberOfSeasons = string.IsNullOrEmpty(NumberOfSeasonsControl.Text) ? 0 : int.Parse(NumberOfSeasonsControl.Text);
                    UpdatedAnime.AnimeDescriptionID = int.Parse(DescriptionDropDownControl.SelectedValue);

                    #endregion

                    AnimeController sysmgr = new AnimeController();
                    var results = sysmgr.AnimeDatabase_Update_Anime(UpdatedAnime);
                    if (results.Item2 == 0)
                    {
                        Message.Text = "Update failed";
                    }
                    else
                    {
                        Message.Text = "the anime '" + results.Item1 + "' has been sucessfuly updated";
                    }
                    BindAnime();
                }
                catch (Exception ex)
                {
                    Message.Text = GetInnerException(ex).ToString();
                }
                #endregion
            }
            //resets the anime to before the changes
            else if (e.CommandName == "Cancel Changes")
            {
                #region cancel changes
                //allow editing...
                AnimeNameControl.ReadOnly = true;
                CurrentEpisodeControl.ReadOnly = true;
                NumberOfEpisodesControl.ReadOnly = true;
                NumberOfSeasonsControl.ReadOnly = true;

                //show / hide buttons and feilds...
                DescriptionTextBoxControl.Visible = true;
                DescriptionDropDownControl.Visible = false;

                EditButtonControl.Visible = true;
                UpdateButtonControl.Visible = false;
                DeleteButtonControl.Visible = true;
                CancelButtonControl.Visible = false;

                //reload data
                BindAnime();
                #endregion
            }

        }

    }
    
    protected void BindDescriptionsDDL_In_WebGrid(object sender, GridViewCommandEventArgs e)
    {
         // collect the row number / index number
        int index = Convert.ToInt32(e.CommandArgument);

        //this retreives the row itself...
        GridViewRow row = Anime_GridView.Rows[index];

        var DescriptionDropDownControl = (DropDownList)row.FindControl("DescriptionDropDownList");
        var DescriptionIDControl = (TextBox)row.FindControl("DescriptionID");
        try
        {
            DescriptionController sysmgr = new DescriptionController();
            List<AnimeDatabase.System.Data.AnimeDatabase_Description> info = sysmgr.FindAll_Descriptions();
            DescriptionDropDownControl.DataSource = info;
            DescriptionDropDownControl.DataTextField = "description";
            DescriptionDropDownControl.DataValueField = "DescriptionID";
            DescriptionDropDownControl.SelectedValue = DescriptionIDControl.Text;
            DescriptionDropDownControl.DataBind();
        }
        catch(Exception ex)
        {
            Message.Text = GetInnerException(ex).ToString();
        }
    }
}//end of class