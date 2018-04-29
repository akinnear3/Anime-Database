<%@ Page Title="Anime Database" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AnimeDatabase.aspx.cs" Inherits="AnimeDatabase_AnimeDatabase" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row" style="border-bottom: solid">
        <h1>Anime Database Search Engin</h1>
    </div>
    <br />
    <asp:Label ID="Message" runat="server" Text=""></asp:Label>
    <br />
    <div class="row">
        &nbsp&nbsp;&nbsp;&nbsp;
        <asp:Label ID="TextParameterLabel" runat="server" Text="Partial Name"></asp:Label>
        &nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;
        &nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;
        &nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;
        &nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;
        &nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
        <asp:Label ID="RatingParameterLabel" runat="server" Text="Rating Type"></asp:Label>

    </div>
    <div class="row">
        <asp:TextBox ID="TextParameter" runat="server" Text="" Width="300px"></asp:TextBox>&nbsp;&nbsp;
         <asp:DropDownList ID="RatingParameter" runat="server" Width="300px" Height="30px" AppendDataBoundItems="True" DataSourceID="ManageRatingsODS" DataTextField="description" DataValueField="DescriptionID">
             <asp:ListItem Value="0">All</asp:ListItem>
         </asp:DropDownList>&nbsp;&nbsp;

            &nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   
        <asp:Button ID="SearchButton" runat="server" Text="Search" Width="130px" Height="30px" OnClick="SearchButton_Click" />&nbsp;&nbsp;
        <asp:Button ID="AddAnimeButton" runat="server" Text="Add Anime" Width="130px" Height="30px" OnClick="AddAnimeButton_Click" />&nbsp;&nbsp;
        <asp:Button ID="ManageRatingsButton" runat="server" Text="Manage Ratings" Width="130px" Height="30px" OnClick="ManageRatingsButton_Click" />&nbsp;&nbsp;
    </div>
    <br />
    <br />

    <asp:GridView ID="Anime_GridView" runat="server"
        AllowPaging="True" PagerSettings-PageButtonCount="5"
        AutoGenerateColumns="False" Width="100%"
        BorderColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="Anime_GridView_PageIndexChanging"
        OnRowCommand="Anime_GridView_RowCommand" RowStyle-Height="28px" 
        RowStyle-Font-Size="Small">
        <Columns>
            <asp:TemplateField HeaderText="ID" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="AnimeID" runat="server" ReadOnly="true" Text='<%# Eval("AnimeID") %>' BorderStyle="None" Visible="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:TextBox ID="AnimeName" runat="server" ReadOnly="true" Text='<%# Eval("AnimeName") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Episode Count">
                <ItemTemplate>
                    <asp:TextBox ID="EpisodeCount" runat="server" ReadOnly="true" Text='<%# Eval("TotalNumberOfEpisodes") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Current Episode">
                <ItemTemplate>
                    <asp:TextBox ID="CurrentEpisode" runat="server" ReadOnly="true" Text='<%# Eval("CurrentEpisode") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Number of Seasons">
                <ItemTemplate>
                    <asp:TextBox ID="NumberOfSeasons" runat="server" ReadOnly="true" Text='<%# Eval("NumberOfSeasons") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description ID" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="DescriptionID" runat="server" ReadOnly="true" Text='<%# Eval("AnimeDescriptionID") %>' BorderStyle="None" Visible="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rating">
                <ItemTemplate>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" ReadOnly="true" Text='<%# Eval("AnimeDescriptionID") %>' BorderStyle="None"></asp:TextBox>
                    <asp:DropDownList ID="DescriptionDropDownList" runat="server" Visible="false"></asp:DropDownList>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <!--buttons in the grid view need to have special elements to be accesed
                        these elements include CommandName and CommandArgument
                        then access it in the grid views RowCommand-->
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="EditButton" runat="server" Text="Edit" visible="true" 
                        CommandName="Edit Anime" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        Width="80px" Height="28px"/>
                    <asp:Button ID="UpdateButton" runat="server" Text="Update" visible="false" 
                        CommandName="Save Changes" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        Width="80px" Height="28px"/>

                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" visible="true" 
                        CommandName="Delete Anime" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        Width="80px" Height="28px"/>
                    <ajaxToolkit:ConfirmButtonExtender runat="server" ConfirmText="Delete this Item?" BehaviorID="DeleteButton_ConfirmButtonExtender" TargetControlID="DeleteButton" ID="DeleteButton_ConfirmButtonExtender"></ajaxToolkit:ConfirmButtonExtender>

                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="false" 
                        CommandName="Cancel Changes" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        Width="80px" Height="28px"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<EmptyDataRowStyle HorizontalAlign="Center"></EmptyDataRowStyle>
        <EmptyDataTemplate>No Anime Found</EmptyDataTemplate>

<PagerSettings PageButtonCount="5" FirstPageText="&amp;lt;&amp;lt;First" LastPageText="&amp;gt;&amp;gt;Last" NextPageText="&lt;" PreviousPageText="&gt;"></PagerSettings>
    </asp:GridView>

    <br />
    <br />
    <!--this is the ratings find all ratings-->
    <asp:ObjectDataSource ID="ManageRatingsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAll_Descriptions" TypeName="AnimeDatabase.System.BLL.DescriptionController"></asp:ObjectDataSource>

    
</asp:Content>

