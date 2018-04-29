using AnimeDatabase.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnimeDatabase_Description : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

        }
        else
        {
            BindDescriptionList();
        }
        Message.Text = "";
    }
    protected void BindDescriptionList()
    {
        try
        {
            DescriptionController sysmgr = new DescriptionController();
            List<AnimeDatabase.System.Data.AnimeDatabase_Description> info = sysmgr.FindAll_Descriptions();
            info.Sort((x, y) => x.description.CompareTo(y.description));
            
            DescriptionDDL.DataSource = info;
            DescriptionDDL.DataValueField = "DescriptionID";
            DescriptionDDL.DataTextField = "description";
            DescriptionDDL.DataBind();
            DescriptionDDL.Items.Insert(0, "Select...");
            DescriptionDDL.Items.Insert(1, "Create New Description...");
        }
        catch(Exception ex)
        {
            Message.Text = GetInnerException(ex).ToString();
        }
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
        if(DescriptionDDL.SelectedIndex == 0)
        {
            Message.Text = "Invalid Selection. Please Select a Description To Manage.";
        }
        //add new
        else if(DescriptionDDL.SelectedIndex == 1)
        {
            try
            {
                DescriptionController sysmgr = new DescriptionController();
                AnimeDatabase.System.Data.AnimeDatabase_Description newDescription = new AnimeDatabase.System.Data.AnimeDatabase_Description();
                newDescription.description = DescriptionTextBox.Text;
                var results = sysmgr.AnimeDatabase_Add_Description(newDescription);
                if(results == null)
                {
                    Message.Text = "Insert Failed";
                    BindDescriptionList();
                }
                else
                {
                    Message.Text = "the description '" + results.Item1 + "' has been sucessfuly added as ID " + results.Item2;
                    BindDescriptionList();
                    DescriptionDDL.SelectedValue = results.Item2.ToString();
                }
            }
            catch(Exception ex)
            {
                Message.Text = GetInnerException(ex).ToString();
            }
        }
        //update old
        else
        {
            try
            {
                DescriptionController sysmgr = new DescriptionController();
                AnimeDatabase.System.Data.AnimeDatabase_Description updatedDescription = new AnimeDatabase.System.Data.AnimeDatabase_Description();
                updatedDescription.description = DescriptionTextBox.Text;
                updatedDescription.DescriptionID = int.Parse(DescriptionDDL.SelectedValue);
                var results = sysmgr.AnimeDatabase_Update_Description(updatedDescription);
                if (results == null || results.Item2 == 0)
                {
                    Message.Text = "update failed";
                    BindDescriptionList();
                }
                else
                {
                    Message.Text = "the description '" + results.Item1 + "' has been sucessfuly Updated";
                    BindDescriptionList();
                    DescriptionDDL.SelectedValue = updatedDescription.DescriptionID.ToString();
                }
                
            }
            catch (Exception ex)
            {
                Message.Text = GetInnerException(ex).ToString();
            }
        }
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        if (DescriptionDDL.SelectedIndex == 0 || DescriptionDDL.SelectedIndex == 1)
        {
            Message.Text = "Invalid Selection. Please Select a Description To Manage.";
        }
        //Delete Description
        else
        {
            try
            {
                DescriptionController sysmgr = new DescriptionController();
                var results = sysmgr.AnimeDatabase_Delete_Description(int.Parse(DescriptionDDL.SelectedValue));
                if (results == null || results.Item2 == 0)
                {
                    Message.Text = "update failed";
                    BindDescriptionList();
                }
                else
                {
                    Message.Text = "the description '" + results.Item1 + "' has been sucessfuly Deleted";
                    BindDescriptionList();
                    DescriptionDDL.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Message.Text = GetInnerException(ex).ToString();
            }
        }
    }
}