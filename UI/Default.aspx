<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-large">Learn more &raquo;</a></p>
    </div>

    <div class="form-horizontal" role="form" runat="server">
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-4" AssociatedControlID="RegNo" runat="server">Registration No</asp:Label>
            <div class="col-sm-8">
                <asp:TextBox MaxLength="7" CssClass="form-control" ID="RegNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RegNoFieldValidator" runat="server" ControlToValidate="RegNo"
                                CssClass="text-danger" ErrorMessage="Registration No field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-4" AssociatedControlID="Year" runat="server">Year</asp:Label>
            <div class="col-sm-8">
                <asp:TextBox MaxLength="4" CssClass="form-control" ID="Year" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="YearFieldValidator" runat="server" ControlToValidate="Year"
                                CssClass="text-danger" ErrorMessage="Year field is required." />
                <asp:RangeValidator ID="YearRangeValidator" runat="server" ControlToValidate="Year"
                     CssClass="text-danger text-left" ErrorMessage="Year must be between 1980 and 2017!" 
                     MaximumValue="2017"  MinimumValue="1980" Type="Integer" />
            </div>    
        </div>

        <div class="col-sm-offset-4 col-sm-8" style="margin-bottom:20px;">
            <div class="form group">
                <asp:Button CssClass="btn btn-primary btn-md" ID="Search10" Text="Search Grade 10" runat="server" OnClick="Search10_Click" />
                <asp:Button CssClass="btn btn-primary btn-md" ID="Search12" Text="Search Grade 12" runat="server" OnClick="Search12_Click" />
            </div>
         </div>
    </div>
    <div class="row form-group">
        <h3><asp:Label CssClass="label label-primary center" ID="NameLabel" runat="server"/></h3>
    </div>
    <div class="row col-sm-offset-3">
        <asp:Image CssClass="col-sm-3 col-sm-offset-1" ID="Photo" runat="server"/>
        <div class="col-sm-6 col-sm-offset-1" style="margin-left:20px;">
            <asp:GridView AutoGenerateColumns="true" style="margin-left:20px" ID="ResultGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="ResultGridView_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
