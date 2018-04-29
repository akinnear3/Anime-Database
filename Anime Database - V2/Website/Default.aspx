<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Welcome to Anime Database</h1>
        <p class="lead">this is a web form style search for records of anime and can be searched by several means</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Contents of web site</h2>
            <p>
               the web site is devided into 3 main sections
            </p>
            <ul>
                <li>1- the entirety of the current database, with the ability to select an existing item to delete or edit it.</li>
                <li>2- a section to add new anime</li>
                <li>3- a section to add & edit Ratings/Description for what can be applied to the anime</li>
            </ul>
        </div>
    </div>
</asp:Content>
