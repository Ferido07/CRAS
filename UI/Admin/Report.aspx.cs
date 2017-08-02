using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentCertificatesDAL;
using System.Collections;
using System.Configuration;

using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;

public partial class Report : System.Web.UI.Page
{

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");

        if (IsPostBack)
        {
            //restore report
            if (Session["DataSourceList"] != null)
            {
                ASPxDocumentViewer1.Report = GenerateReport((ArrayList)Session["DataSourceList"]);
               // Session["DataSourceList"] = null;
                
            }
           //why the heck does this code doesnt work and say in an alert object reference not set to an
            //instance of an object after the sesion object is retrieved and set as the Report for the 
            //documentViewer. This could have saved a second trip to GenerateReport() Method
            /*if (Session["Report"] != null)
            {
                ASPxDocumentViewer1.Report = (XtraReport)Session["Report"];
                Session["Report"] = null;
            }*/
        }

    }

    private XtraReport GenerateReport(ArrayList DataSourceList)
    {
        //create report object
        XtraReport1 report = new XtraReport1();

        //bind the array to datasource
        report.DataSource = DataSourceList;

        //add bounded labels to detail band of the report with the bindingMember strings being exactly the
        //same as the property names to bind to 
        report.AddBoundLabel("RegistrationNo", new System.Drawing.Rectangle(40, 0, 142, 25));
        report.AddBoundLabel("year", new System.Drawing.Rectangle(182, 0, 79, 25));
        report.AddBoundLabel("Name", new System.Drawing.Rectangle(261, 0, 295, 25));
        report.AddBoundLabel("Grade", new System.Drawing.Rectangle(556, 0, 65, 25));
        report.AddBoundLabel("", new System.Drawing.Rectangle(620, 0, 94, 25));

        return report;
    }

    protected void ShowReportbtn_Click(object sender, EventArgs e)
    {
        short? year = null;
        int? registrationNo = null;
        int? grade = null;// grade can be 10, 12, or null meaning both grades

        if (!String.IsNullOrEmpty(YearTB.Text))
        {
            //use different try blocks so that an exception thrown when processing year dont make the regNO null
            //or skipped
            try
            {
                year = short.Parse(YearTB.Text);
            }
            catch (FormatException fe) { }
        }
        if (!String.IsNullOrEmpty(RegistrationNoTB.Text)) { 
            try
            {
                registrationNo = int.Parse(RegistrationNoTB.Text);
            }
            catch (FormatException fe) { }
        }


        if (GradeDDL.SelectedValue == "10")
            grade = 10;
        else if (GradeDDL.SelectedValue == "12")
            grade = 12;
        else
            grade = null;//i.e both grade 10 and 12

        

        // create database connection
        CertificatesDAL certificates = new CertificatesDAL();
        String connectionString = ConfigurationManager.ConnectionStrings["CertificatesSqlProvider"].ConnectionString;
        certificates.OpenConnection(connectionString);

        ArrayList StudentRecordList = certificates.GetManuallyAddedRecords(registrationNo, year, grade); 

        

        Session["DataSourceList"] = StudentRecordList;
        //GenerateReport(StudentRecordList);

        ASPxDocumentViewer1.Visible = true;
        //set the report property of DocumentViewer 
        ASPxDocumentViewer1.Report = GenerateReport(StudentRecordList);
        //bith lines commented are no use and result in situation described in page_load 
        //Session["Report"] = ASPxDocumentViewer1.Report;
        //Session["Report"] = report;
        //just added to add a breakpoint int i = 0;
    }
}