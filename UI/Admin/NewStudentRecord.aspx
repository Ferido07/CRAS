<%@ Page Title="Add a New Student Record" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewStudentRecord.aspx.cs" Inherits="NewStudentRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div class="row-fluid">
        <h2><%: Title %>.</h2>
        <hr />
        <div class="col-md-7">
            <div class="form-horizontal" role="form" runat="server">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-4" AssociatedControlID="RegNo">Registration No 
                        <asp:Label runat="server" CssClass="text-danger">*</asp:Label> 
                    </asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="RegNo" MaxLength="7" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RegNoFieldValidator" runat="server" ControlToValidate="RegNo"/>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-4" AssociatedControlID="Year">Year
                        <asp:Label runat="server" CssClass="text-danger">*</asp:Label>
                    </asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="Year" MaxLength="4" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="YearFieldValidator" runat="server" ControlToValidate="Year"/>
                        <asp:RangeValidator ID="YearRangeValidator" runat="server" ControlToValidate="Year"
                            CssClass="" ErrorMessage= "Year must be between 1980" 
                            MaximumValue="2018" MinimumValue="1980" Type="Integer" Display="None" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-4" AssociatedControlID="Name">Name
                        <asp:Label runat="server" CssClass="text-danger">*</asp:Label>
                    </asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="Name" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NameFieldValidator1" runat="server" ControlToValidate="Name"/>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-4" AssociatedControlID="FName">Father's Name
                        <asp:Label runat="server" CssClass="text-danger">*</asp:Label>
                    </asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="FName" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="FNameFieldValidator" runat="server" ControlToValidate="FName"/>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-4" AssociatedControlID="LName">Grandfather's Name
                        <asp:Label runat="server" CssClass="text-danger">*</asp:Label>
                    </asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox ID="LName" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LNameFieldValidator" runat="server" ControlToValidate="LName" ErrorMessage="* Required Field" Display="None"/>
                    </div>
                </div>
            </div>
            
            <asp:validationsummary runat="server" ID="summary" CssClass="form-group text-danger"></asp:validationsummary>
        </div>
        <div class="col-md-5">
            <div class="row-fluid">
                <div class="col-md-12 form-group">
                    <asp:Image ID="Photo" runat="server" CssClass="text-center" ImageUrl="~/anonymous.gif" Height="200px" Width="200px" />
                </div>
                <div class ="col-md-12 form-group">
                    <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-group btn btn-default" />
                </div>
                <div class="col-md-12 form-group">
                    <asp:LinkButton ID="UploadButton" runat="server" CssClass=" btn btn-default" OnClick="UploadButton_Click">
                <span class="glyphicon glyphicon-upload"></span> Upload Photo</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div>

        <div class="container-fluid form-group">
            <fieldset class="row-fluid col-md-offset-1">
                <legend>Select Grade</legend>
                <asp:RadioButton GroupName="Grade" ID="G10RadioButton" runat="server" CssClass="radio" Text="Grade 10" Checked="true" />
                <asp:RadioButton GroupName="Grade" ID="G12RadioButton" runat="server" CssClass="radio" Text="Grade 12" />
            </fieldset>
        </div>
        <div class="container-fluid form-group"><div class="row-fluid col-md-offset-1">
            <asp:LinkButton ID="addSubject" runat="server" CssClass="btn btn-default" OnClick="addSubject_Click"><span class="glyphicon glyphicon-plus text-success"></span> Subjects</asp:LinkButton>
            <asp:LinkButton ID="removeAllSubjects" runat="server" CssClass="btn btn-default disable" OnClick="removeAllSubjects_Click"><span class="glyphicon glyphicon-minus text-danger"></span> Subjects</asp:LinkButton>
        </div></div>

        <div class="container-fluid form-group">
            <div class="row-fluid col-md-offset-1">
                <asp:Panel ID="SubjectsPanel" runat="server"></asp:Panel>
                <asp:Panel ID="ResultPanel" runat="server"></asp:Panel>
            </div>
        </div>
        <div class="container-fluid col-md-offset-1">
            <asp:LinkButton ID="SubmitBtn"
                runat="server"
                CssClass="btn btn-default" OnClick="SubmitBtn_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-ok text-success"></span> Submit
            </asp:LinkButton>
        </div>
    </div>

    <asp:Label ID="TextBox1" runat="server"></asp:Label>

</asp:Content>

