<%@ Page Title="Description / Rating" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Description.aspx.cs" Inherits="AnimeDatabase_Description" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


    <div class="row" style="border-bottom: solid">
        <h1>Manage Descriptions / Ratings</h1>
    </div>
    <br />

    <div class="row">
        <asp:Label ID="Message" runat="server" Text="Label"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

    <asp:RequiredFieldValidator ID="DescriptionRequiredFieldValidator" runat="server" ErrorMessage="description is a required field" SetFocusOnError="true" ControlToValidate="DescriptionTextBox" Display="None"></asp:RequiredFieldValidator>

    </div>    
    <br />

    <div class="row">
        <div class="col-md-2">
            &nbsp;
        </div>

        <div class="col-md-2">
            &nbsp;
            <asp:Label ID="DescriptionToManage" runat="server" Text="Description To Manage"></asp:Label>
        </div>
        <div class="col-md-2">
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="DescriptionLabel" runat="server" Text="Description"></asp:Label>
        </div>
    </div>
    

    <div class="row">
        <div class="col-md-2">
            &nbsp;
        </div>
        <div class="col-md-2">
            <asp:DropDownList ID="DescriptionDDL" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-md-2">    
            <asp:TextBox ID="DescriptionTextBox" runat="server" Text="" ></asp:TextBox>
        </div>
        <div class="col-md-2">    
            <asp:Button ID="SubmitButton" runat="server" Text="Update List Of Descriptions" OnClick="SubmitButton_Click" />
        </div>
        <div class="col-md-2">    
            <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClick="DeleteButton_Click" CausesValidation="false" />
            <ajaxToolkit:ConfirmButtonExtender runat="server" ConfirmText="Delete this Description?" BehaviorID="DeleteButton_ConfirmButtonExtender" TargetControlID="DeleteButton" ID="DeleteButton_ConfirmButtonExtender"></ajaxToolkit:ConfirmButtonExtender>
        </div>
    </div>
    
    
</asp:Content>

