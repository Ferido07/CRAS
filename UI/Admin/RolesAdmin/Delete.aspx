<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Admin_RolesAdmin_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    
<h2>Delete.</h2>

<h3>Are you sure you want to delete this Role? </h3>
<p>Deleting this Role will remove all users from this role. It will not delete the users.</p>
    <div>
        <h4>Role.</h4>
        <hr />
        <asp:Label runat="server" ID="result" CssClass="text-danger"></asp:Label>
        <dl class="dl-horizontal">
            <dt>
                <asp:Label runat="server">Name</asp:Label>
            </dt>

            <dd>
                <asp:Label runat="server" ID="roleName"></asp:Label>
            </dd>
        </dl>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="DeleteRole" OnClick="DeleteRole_Click" CssClass="btn btn-default" Text="Delete" />
            </div>
        </div>
    </div>
<div>
    <asp:HyperLink runat="server" Text ="Back to list" NavigateUrl= "~/Admin/RolesAdmin/Default.aspx"></asp:HyperLink>
</div>
</asp:Content>

