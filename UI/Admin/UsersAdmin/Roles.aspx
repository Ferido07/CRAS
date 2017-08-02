<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Admin_UsersAdmin_Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <h4>Users.</h4>
    <hr />
    <dl class="dl-horizontal">
       <dt>
                <asp:Label runat="server">User Name</asp:Label>
            </dt>

            <dd>
                <asp:Label runat="server" ID="userName"></asp:Label>
            </dd>
    </dl>
</div>
<h4>List of roles for this user</h4>

   <asp:Label runat="server" ID="MessageLabel" />

<asp:Table ID="Table1" runat="server" CssClass="table"></asp:Table>
<p>
   <!-- @Html.ActionLink("Edit", "Edit", new { id = Model.Id }) |-->
   <asp:HyperLink runat="server" Text ="Back to list" NavigateUrl= "~/Admin/UsersAdmin/Default.aspx"></asp:HyperLink>
</p>

</asp:Content>

