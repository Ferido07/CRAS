<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Admin_RolesAdmin_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h2>Create.</h2>

    <div class="form-horizontal">
        <h4>Role.</h4>
        <hr />
       <asp:ValidationSummary  runat="server" ID="summary"/>

        <div class="form-group">
            <asp:Label runat="server" Text="Role Name" AssociatedControlID="roleName" CssClass = "control-label col-md-2"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" CssClass = "form-control" ID="roleName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="req" ControlToValidate="roleName" ErrorMessage="Role Name required!" Display="Dynamic"></asp:RequiredFieldValidator>
             </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="SaveRole" OnClick="SaveRole_Click" CssClass="btn btn-default" Text="Save" />
            </div>
        </div>
    </div>


<div>
    <asp:HyperLink runat="server" Text ="Back to list" NavigateUrl= "~/Admin/RolesAdmin/Default.aspx"></asp:HyperLink>
</div>


</asp:Content>

