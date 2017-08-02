using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentCertificatesDAL;
using System.Configuration;
using System.Data;

public partial class _Default : Page
{
    private Boolean IsGrade12 = false;
    private short? total;

    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
    protected void Search10_Click(object sender, EventArgs e)
    {
        CertificatesDAL certificates = new CertificatesDAL();
        String connectionString = ConfigurationManager.ConnectionStrings["CertificatesSqlProvider"].ConnectionString;
        certificates.OpenConnection(connectionString);
        Student student=null;
        if (!(String.IsNullOrEmpty(RegNo.Text) || String.IsNullOrEmpty(Year.Text)))
        {
            try { student = certificates.FindGrade10Certificate(int.Parse(RegNo.Text), int.Parse(Year.Text)); }
            catch (FormatException fe){ NameLabel.Text = "Invalid Input!"; certificates.CloseConnection(); return;}
            catch (OverflowException ox) { NameLabel.Text = "Input value too Large!"; return; }
        }  
        certificates.CloseConnection();
       
        if (student != null)
        {
            NameLabel.Text = student.Name + " " + student.FName + " " + student.LName;
            NameLabel.CssClass = "label label-primary col-sm-offset-3 col-sm-6"; 
            //ResultGridView.DataSource = student.Subjects.OfType<Grade12Subject>().ToList();
            List<Grade10Subject> G10S = student.Subjects.Cast<Grade10Subject>().ToList();
            ResultGridView.DataSource = CreateG10DataTable( G10S );
            ResultGridView.ShowFooter = false;
            ResultGridView.DataBind();
            Photo.ImageUrl = "Images/" + student.Year + "_" + student.RegistrationNo+".jpg";
            Photo.Height = Unit.Parse("200px");
            Photo.Width = Unit.Parse("200px");

            ResultGridView.EmptyDataText="No Student with the given registration number and year exists";
        }
        else if (student == null)
        { 
            NameLabel.Text = "No Student with the given record exists!";
            NameLabel.CssClass = "label label-danger col-sm-offset-3 col-sm-6";
            ResultGridView.DataSource = null;
        }


    }
    protected void Search12_Click(object sender, EventArgs e)
    {
        CertificatesDAL certificates = new CertificatesDAL();
        String connectionString= ConfigurationManager.ConnectionStrings["CertificatesSqlProvider"].ConnectionString;
        certificates.OpenConnection(connectionString);
        Student student = null;
        List<Grade12Subject> G12S=null;

        if (!(String.IsNullOrEmpty(RegNo.Text) || String.IsNullOrEmpty(Year.Text)))
        {
            try { student = certificates.FindGrade12Certificate(int.Parse(RegNo.Text), int.Parse(Year.Text)); }
            catch (FormatException ex)
            {
                NameLabel.Text = "Invalid Input!"; certificates.CloseConnection();
                return;
            }
            catch (OverflowException ox) { NameLabel.Text = "Input value too Large!"; return; }
        }    
        certificates.CloseConnection();
        if (student != null)
        {
            NameLabel.Text = student.Name+ " "+student.FName+" " + student.LName;
            NameLabel.CssClass = "label label-primary col-sm-offset-3 col-sm-6";
            //ResultGridView.DataSource = student.Subjects.OfType<Grade12Subject>().ToList();
            //List<Grade12Subject> subjects12 =;
            
            G12S=student.Subjects.Cast<Grade12Subject>().ToList();

            ResultGridView.ShowFooter = true;
            IsGrade12 = true;
            total = Grade12Subject.findTotal(G12S);
            //G12S.Add(new Grade12Subject{SubjectName="Total",Score=Grade12Subject.findTotal(G12S)});

            
            ResultGridView.DataSource = CreateG12DataTable( G12S );
           /* 
            ResultGridView.FooterRow.Cells.Add(new TableCell { Text = "Total" });
            ResultGridView.FooterRow.Cells.Add(new TableCell { Text = "" + Grade12Subject.findTotal(G12S) });
            */
            ResultGridView.DataBind();
            Photo.ImageUrl = "Images/" + student.Year + "_" + student.RegistrationNo+".jpg";
            Photo.Height = Unit.Parse("200px");
            Photo.Width = Unit.Parse("200px");
          
        }
        else
        {
            NameLabel.Text = "No Student with the given record exists!";
            NameLabel.CssClass = "label label-danger col-sm-offset-3 col-sm-6";
            ResultGridView.DataSource = null;   
        }
        

    }
   

    public void UnitTest(object sender,EventArgs e)
    {
        Student student = new Student();
        student.Name = "Abc"; student.FName = "123"; student.LName = "doremi";
        student.RegistrationNo = 111111; student.Year = 2015;
        Grade12Subject english = new Grade12Subject { SubjectName = "English", Score = 80 };
        Grade12Subject mathematics = new Grade12Subject { SubjectName = "Mathematics", Score = 81 };
        Grade12Subject physics = new Grade12Subject { SubjectName = "Physics", Score = 82 };
        Grade12Subject biology = new Grade12Subject { SubjectName = "Biology", Score = 83 };
        Grade12Subject chemistry = new Grade12Subject { SubjectName = "Chemistry", Score = 84 };
        Grade12Subject Aptitude = new Grade12Subject { SubjectName = "Aptitude", Score = 85 };
        Grade12Subject civics = new Grade12Subject { SubjectName = "Civics", Score = 84 };
        student.Subjects.Add(english);
        student.Subjects.Add(mathematics);
        student.Subjects.Add(physics);
        student.Subjects.Add(biology);
        student.Subjects.Add(chemistry);
        student.Subjects.Add(Aptitude);
        student.Subjects.Add(civics);

        Grade12Subject find = (Grade12Subject)student.GetSubject("Chemistr");
        System.Diagnostics.Debug.WriteLine("The subject is " + find.SubjectName + " and the score is " + find.Score);
        //System.IO.Directory.GetFiles();
        
    }

    private System.Data.DataTable CreateG10DataTable(List<Grade10Subject> subjects)
    {
        DataTable table = new DataTable();
        //add columns
        table.Columns.Add("Subjects");
        table.Columns.Add("Grades");
        //add rows
        for(int i=0; i< subjects.Count; i++)
             table.Rows.Add(subjects[i].SubjectName,subjects[i].Grade);
                       
        return table;
    }

    private DataTable CreateG12DataTable(List<Grade12Subject> subjects)
    {
        DataTable table = new DataTable();
        //add columns
        table.Columns.Add("Subjects");
        table.Columns.Add("Scores");
        //add rows
        for (int i = 0; i < subjects.Count; i++)
            table.Rows.Add(subjects[i].SubjectName, subjects[i].Score);

        return table;

    }

    protected void ResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer && IsGrade12)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[1].Text = "" + total;   
        }
    }
}