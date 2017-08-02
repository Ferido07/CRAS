<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  
    <h2>Select Criteria to Filter the Report</h2>
    <div class="form-inline form-group">

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="GradeDDL">Grade</asp:Label>
            <asp:DropDownList CssClass="form-control" ID="GradeDDL" runat="server">
               <asp:ListItem>10</asp:ListItem>
               <asp:ListItem>12</asp:ListItem>
               <asp:ListItem>Both</asp:ListItem> 
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="YearTB">Year</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" MaxLength="4" ID="YearTB"></asp:TextBox>
            
        </div> 
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="RegistrationNoTB">Registration No</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" MaxLength="7" ID="RegistrationNoTB"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Button runat="server" ID="ShowReportbtn" CssClass="btn btn-default" Text="Show Report" OnClick="ShowReportbtn_Click" />
        </div>
         
    </div>
     <asp:RangeValidator ID="YearRangeValidator" runat="server" ControlToValidate="YearTB"
                     CssClass="form-group text-danger text-left" ErrorMessage="Year must be between 1980 and 2017!" 
                     MaximumValue="2017"  MinimumValue="1980" Type="Integer" Display="None" /> 
    <asp:RangeValidator ID="RegNoRangeValidator" runat="server" ControlToValidate="RegistrationNoTB"
                     CssClass="form-group text-danger text-left" ErrorMessage="Registration No must be between 0000000 and 9999999!" 
                     MaximumValue="9999999"  MinimumValue="0" Type="Integer" Display="None" />
    <asp:ValidationSummary runat="server" ID="summary" CssClass="" />
    <dx:ASPxDocumentViewer Visible="false" ID="ASPxDocumentViewer1" runat="server" ToolbarMode="Ribbon"></dx:ASPxDocumentViewer>
   
</asp:Content>

