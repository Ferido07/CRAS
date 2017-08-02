<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_UsersAdmin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

     <h3>Users</h3>
    <asp:HyperLink runat="server" NavigateUrl="~/Account/Register.aspx">Create New</asp:HyperLink>
    <br />
    <br />
    <asp:Table ID="Table1" runat="server" CssClass="table"></asp:Table>
    
</asp:Content>

