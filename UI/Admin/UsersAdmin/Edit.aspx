<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Admin_UsersAdmin_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h2>Edit.</h2>

    <div class="form-horizontal">
        <h4>Edit User Form.</h4>
        <hr />
        <asp:ValidationSummary  runat="server" ID="summary"/>

        <div class="form-group">
            <asp:Label runat="server" Text="User Name" AssociatedControlID="userName" CssClass = "control-label col-md-2"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" CssClass = "form-control" ID="userName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="req" ControlToValidate="userName" ErrorMessage="User Name required!" Display="Dynamic"></asp:RequiredFieldValidator>
             </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" CssClass="control-label col-md-2" AssociatedControlID="rolesSpan">Roles</asp:Label>
            <span runat="server" id="rolesSpan" class=" col-md-10">
              
            </span>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="SaveUser" OnClick="SaveUser_Click" CssClass="btn btn-default" Text="Save" />
            </div>
        </div>
    </div>

    <asp:Label runat="server" ID="info"></asp:Label>
<div>
    <asp:HyperLink runat="server" Text ="Back to list" NavigateUrl= "~/Admin/UsersAdmin/Default.aspx"></asp:HyperLink>
</div>
</asp:Content>

