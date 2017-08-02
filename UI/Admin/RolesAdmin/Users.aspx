<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_RolesAdmin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    
<h2>Details.</h2>

<div>
    <h4>Roles.</h4>
    <hr />
    <dl class="dl-horizontal">
       <dt>
                <asp:Label runat="server">Name</asp:Label>
            </dt>

            <dd>
                <asp:Label runat="server" ID="roleName"></asp:Label>
            </dd>
    </dl>
</div>
<h4>List of users in this role</h4>

   <asp:Label runat="server" ID="MessageLabel" />

<asp:Table ID="Table1" runat="server" CssClass="table"></asp:Table>
<p>
   <!-- @Html.ActionLink("Edit", "Edit", new { id = Model.Id }) |-->
   <asp:HyperLink runat="server" Text ="Back to list" NavigateUrl= "~/Admin/RolesAdmin/Default.aspx"></asp:HyperLink>
</p>

</asp:Content>

