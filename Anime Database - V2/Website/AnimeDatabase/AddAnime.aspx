<%@ Page Title="Add Anime" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddAnime.aspx.cs" Inherits="AnimeDatabase_AddAnime" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="row" style="border-bottom: solid">
        <h1>Add Anime</h1>
    </div>
    <br />

    <asp:Label ID="Message" runat="server" Text=""></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name Required" ControlToValidate="AnimeName" Display="None"></asp:RequiredFieldValidator>

    <asp:CompareValidator ID="NumberOfEpisodesCompareValidator" runat="server" ErrorMessage="Number of Episodes Must be a value >= 0" ControlToValidate="NumberOfEpisodes" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" Display="None"></asp:CompareValidator>

    <asp:CompareValidator ID="CurrentlyOnCompareValidator" runat="server" ErrorMessage="Current Episode needs to be a value >= 0" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ControlToValidate="CurrentlyOn" Display="None"></asp:CompareValidator>

    <asp:CompareValidator ID="NumberOfSeasonsCompareValidator" runat="server" ErrorMessage="Number of seasons Must be a value >= 0" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ControlToValidate="NumberOfSeasons" Display="None"></asp:CompareValidator>

    <br />

    &nbsp;&nbsp;
    <asp:Label ID="AnimeNameLabel" runat="server" Text="Name"></asp:Label>&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="NumberOfEpisodesLabel" runat="server" Text="Number of Episodes"></asp:Label>&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="CurrentlyOnLabel" runat="server" Text="Currently on Episode"></asp:Label>&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="NumberOfSeasonsLabel" runat="server" Text="Number of Seasons"></asp:Label>&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="RatingLabel" runat="server" Text="Rating"></asp:Label>&nbsp;&nbsp;
    <br />

    <asp:TextBox ID="AnimeName" runat="server" MaxLength="100"></asp:TextBox>
    <asp:TextBox ID="NumberOfEpisodes" runat="server"></asp:TextBox>
    <asp:TextBox ID="CurrentlyOn" runat="server"></asp:TextBox>
    <asp:TextBox ID="NumberOfSeasons" runat="server"></asp:TextBox>
    <asp:DropDownList ID="Rating" runat="server" AppendDataBoundItems="True" DataSourceID="ManageRatingsODS" DataTextField="description" DataValueField="DescriptionID">
             <asp:ListItem Value="0">Select...</asp:ListItem>
    </asp:DropDownList>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="SubmitButton" runat="server" Text="Add Anime" OnClick="SubmitButton_Click" /> &nbsp;&nbsp;
    <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="ClearButton_Click" CausesValidation="false" />


     <asp:ObjectDataSource ID="ManageRatingsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAll_Descriptions" TypeName="AnimeDatabase.System.BLL.DescriptionController"></asp:ObjectDataSource>
</asp:Content>


