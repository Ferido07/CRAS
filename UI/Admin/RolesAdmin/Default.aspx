<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_RolesAdmin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3>Roles</h3>
    <asp:HyperLink runat="server" NavigateUrl="Create">Create New</asp:HyperLink>
    <br />
    <br />
    <asp:Table ID="Table1" runat="server" CssClass="table"></asp:Table>
    
</asp:Content>

