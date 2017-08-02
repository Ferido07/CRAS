using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentCertificatesDAL;
using System.Configuration;
using System.IO;
//using WebSite1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

public partial class NewStudentRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!Context.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");
        System.Diagnostics.Debug.WriteLine("page load before is postback if clause");
        
        if (IsPostBack)
        {
            System.Diagnostics.Debug.WriteLine("page load in postback if clause");
            //clearing previous display status 
            SubjectsPanel.GroupingText = "";
            ResultPanel.Controls.Clear();
            ResultPanel.CssClass = "";
            //If first time page is submitted and we have file in FileUpload control but not in session
            // Store the values to SEssion Object
            if (Session["FileUpload"] == null && FileUpload.HasFile)
            {
                Session["FileUpload"] = FileUpload;
                System.Diagnostics.Debug.WriteLine("saving file upload element");
                //FileUploadName.Text = FileUpload.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            // restore if there was file before postback---applies after postback from addsubjects or removesubjects 
            else if (Session["FileUpload"] != null && (!FileUpload.HasFile))
            {
                FileUpload = (FileUpload)Session["FileUpload"];
                System.Diagnostics.Debug.WriteLine("restore file upload element");
                //FileUploadName.Text = FileUpload.FileName;
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            //or for multiple postbacks wzout user changing the fileupload
            else if (FileUpload.HasFile)
            {
                Session["FileUpload"] = FileUpload;
                System.Diagnostics.Debug.WriteLine(" replace file uplaod in session");
                //FileUploadName.Text = FileUpload.FileName;
            }
            
           //always save student in sesson before any postback so in case any postback removes data or 
           //postback fails as in case of submit then the previous data is restore whrn needed
            SaveStudentInSession();
           
        }

    }
    private void AddScoreRangeValidator(Panel parentPanel, String ControlToValidate, String CssClass)
    {
        RangeValidator validator = new RangeValidator();
        validator.ControlToValidate = ControlToValidate;
        validator.CssClass = CssClass;
        validator.Type = ValidationDataType.Integer;
        validator.MinimumValue = "0";
        validator.MaximumValue = "100";
        validator.ErrorMessage = "Score must be between 0 and 100!";
        parentPanel.Controls.Add(validator);
    }

    private int GetGrade()
    {
        if (G10RadioButton.Checked)
            return 10;
        else { G12RadioButton.Checked = true; return 12; }
    }

    private void SaveStudentInSession() 
    {
           //save the student object it ovewrites any existing data
            System.Diagnostics.Debug.WriteLine("saving student");
            int grade = GetGrade();

            //check if there is student fully(for submition) or partially(between postbacks with incomplete 
            //form- might happen if the submition of a student record failed, or upload button is clicked) created
            if (grade == 10)
            {
                Student G10Student = GetG10Student();
                //check student and save for later
                if (G10Student != null)
                {
                    Session["Student"] = G10Student;
                    System.Diagnostics.Debug.WriteLine("grade10 student saved");
                }
            }
            else if (grade == 12)
            {
                Student G12Student = GetG12Student();
                //check student and save for later
                if (G12Student != null)
                {
                    Session["Student"] = G12Student; 
                    System.Diagnostics.Debug.WriteLine("grade12 student saved");
                }
            }
    }

    private void RestoreStudentInSession() {
        //restore code might not be useful because student objects cannot be 
        // null between postbacks cause the only way studetn might be null is if the registration no
        // is faulty. But wait the subject scores or gades might have been lost so
        //there must also be code to add subjects back with the score in place.
        if (Session["Student"] != null)
        {
            System.Diagnostics.Debug.WriteLine("restoring student");
            int grade = GetGrade();
            if (grade == 10)
            {
                //TODO: check for code(check redundency) and optimize or remove the check if clause 
                // if (GetG10Student() == null)
                {
                    Student G10Student = (Student)Session["Student"];
                    AddG10Subjects(G10Student);
                    System.Diagnostics.Debug.WriteLine("grade10 subjects added");
                }
            }
            else if (grade == 12)
            {
                //TODO: check for code(check redundency) and optimize or remove the check if clause 
                // if (GetG12Student() == null)
                {
                    Student G12Student = (Student)Session["Student"];
                    AddG12Subjects(G12Student);
                    System.Diagnostics.Debug.WriteLine("grade12 subjects added");
                }
            }


        }
    }

    private Label NewSubjectLabel(String Text, String CssClass, String AssociatedControlID)
    {
        Label subjectLabel = new Label();
        subjectLabel.Text = Text;
        subjectLabel.CssClass = CssClass;
        subjectLabel.AssociatedControlID = AssociatedControlID;
        return subjectLabel;
    }

    private DropDownList NewG10GradesDropDownList(String ID)
    {
        //doesn't cause any problem if null or empty etring("") is set as the SelectedValue of the DropDownList
        DropDownList G10GradesList = new DropDownList();
        G10GradesList.Text = "Grade 10 Subjects";
        G10GradesList.CssClass = "form-control";
        ListItem None = new ListItem("Select Grade");
        ListItem A = new ListItem("A");
        ListItem B = new ListItem("B");
        ListItem C = new ListItem("C");
        ListItem D = new ListItem("D");
        ListItem F = new ListItem("F");

        G10GradesList.Items.Add(None);
        G10GradesList.Items.Add(A);
        G10GradesList.Items.Add(B);
        G10GradesList.Items.Add(C);
        G10GradesList.Items.Add(D);
        G10GradesList.Items.Add(F);
        G10GradesList.ID = ID;
        return G10GradesList;
    }

    private TextBox NewSubjectTextBox(String ID, String CssStyle)
    {
        //doesn't cause any problem if null or empty etring("") is set as the Text of the TextBox
        TextBox subjectTextBox = new TextBox();
        subjectTextBox.ID = ID;
        subjectTextBox.CssClass = CssStyle;
        return subjectTextBox;
    }
    
    private String GetSubjectGrade(String SubjectDropDownID)
    {
        String grade = null; 
        SubjectDropDownID = "ctl00$MainContent$" + SubjectDropDownID;
        try
        {
            if (String.Equals(Request.Form.Get(SubjectDropDownID), "Select Grade"))
                return null;

            else grade= Request.Form.Get(SubjectDropDownID);
        }
        catch (ArgumentNullException ane) { ;}
        return grade;
    }

    private short? GetSubjectScore(String SubjectTextBoxID)
    {
        short? score = null;
        SubjectTextBoxID = "ctl00$MainContent$" + SubjectTextBoxID;
        try
        {
            score = short.Parse(Request.Form.Get(SubjectTextBoxID));
        }
        catch (FormatException fe) { return null;}
        catch (OverflowException oe) { return null;}
        catch (ArgumentNullException ane) { ;/*todo code to handle the no subject added instance*/}
        //todo : exeption handle code
        return score;
    }

    private void AddG10Subjects(Student student)
    {
        //TODO: Code to add subjects with grades of student object in session in AddG10Subjects(Student)
        Boolean StudentNotNull;
        if (student == null)
            StudentNotNull = false;
        else StudentNotNull = true;
        //Add English Controls
        DropDownList englishGrade = NewG10GradesDropDownList("english");
        Label englishLabel = NewSubjectLabel("English", "control-label col-sm-2", "english");
        Panel englishPanel = new Panel { CssClass = "form-group" };
        englishPanel.Controls.Add(englishLabel);
        englishPanel.Controls.Add(englishGrade);
        SubjectsPanel.Controls.Add(englishPanel);
        //Add Amharic Controls
        DropDownList amharicGrade = NewG10GradesDropDownList("amharic"); 
        Label amharicLabel = NewSubjectLabel("Amharic", "control-label col-sm-2", "amharic");
        Panel amharicPanel = new Panel { CssClass = "form-group" };
        amharicPanel.Controls.Add(amharicLabel);
        amharicPanel.Controls.Add(amharicGrade);
        SubjectsPanel.Controls.Add(amharicPanel);
        //Add Mathematics Controls
        DropDownList mathematicsGrade = NewG10GradesDropDownList("mathematics");
        Label mathematicsLabel = NewSubjectLabel("Mathematics", "control-label col-sm-2", "mathematics");
        Panel mathematicsPanel = new Panel { CssClass = "form-group" };
        mathematicsPanel.Controls.Add(mathematicsLabel);
        mathematicsPanel.Controls.Add(mathematicsGrade);
        SubjectsPanel.Controls.Add(mathematicsPanel);
        //Add Physics Controls
        DropDownList physicsGrade = NewG10GradesDropDownList("physics");
        Label physicsLabel = NewSubjectLabel("Physics", "control-label col-sm-2", "physics");
        Panel physicsPanel = new Panel { CssClass = "form-group" };
        physicsPanel.Controls.Add(physicsLabel);
        physicsPanel.Controls.Add(physicsGrade);
        SubjectsPanel.Controls.Add(physicsPanel);
        //Add biology Controls
        DropDownList biologyGrade = NewG10GradesDropDownList("biology");
        Label biologyLabel = NewSubjectLabel("Biology", "control-label col-sm-2", "biology");
        Panel biologyPanel = new Panel { CssClass = "form-group" };
        biologyPanel.Controls.Add(biologyLabel);
        biologyPanel.Controls.Add(biologyGrade);
        SubjectsPanel.Controls.Add(biologyPanel);
        //Add Chemistry Controls
        DropDownList chemistryGrade = NewG10GradesDropDownList("chemistry");
        Label chemistryLabel = NewSubjectLabel("Chemistry", "control-label col-sm-2", "chemistry");
        Panel chemistryPanel = new Panel { CssClass = "form-group" };
        chemistryPanel.Controls.Add(chemistryLabel);
        chemistryPanel.Controls.Add(chemistryGrade);
        SubjectsPanel.Controls.Add(chemistryPanel);
        //Add Geography Controls
        DropDownList geographyGrade = NewG10GradesDropDownList("geography");
        Label geograpyLabel = NewSubjectLabel("Geography", "control-label col-sm-2", "geography");
        Panel geographyPanel = new Panel { CssClass = "form-group" };
        geographyPanel.Controls.Add(geograpyLabel);
        geographyPanel.Controls.Add(geographyGrade);
        SubjectsPanel.Controls.Add(geographyPanel);
        //Add History Controls
        DropDownList historyGrade = NewG10GradesDropDownList("history");
        Label historyLabel = NewSubjectLabel("History", "control-label col-sm-2", "history");
        Panel historyPanel = new Panel { CssClass = "form-group" };
        historyPanel.Controls.Add(historyLabel);
        historyPanel.Controls.Add(historyGrade);
        SubjectsPanel.Controls.Add(historyPanel);
        //Add Civics Controls
        DropDownList civicsGrade = NewG10GradesDropDownList("civics");
        Label civicsLabel = NewSubjectLabel("Civics", "control-label col-sm-2", "civics");
        Panel civicsPanel = new Panel { CssClass = "form-group" };
        civicsPanel.Controls.Add(civicsLabel);
        civicsPanel.Controls.Add(civicsGrade);
        SubjectsPanel.Controls.Add(civicsPanel);
        //Set GroupingText for Subjects
        SubjectsPanel.GroupingText = "Grade 10 Subjects";
        
        //Set the appropriate values for each subject if a student with some sourses is available 
        //mainly used to restore student in session between postbacks and/or registration fails.
        if (StudentNotNull)
        {
            englishGrade.SelectedValue = ((Grade10Subject)student.GetSubject("english")).Grade;
            amharicGrade.SelectedValue = ((Grade10Subject)student.GetSubject("amharic")).Grade;
            mathematicsGrade.SelectedValue = ((Grade10Subject)student.GetSubject("mathematics")).Grade;
            physicsGrade.SelectedValue = ((Grade10Subject)student.GetSubject("physics")).Grade;
            biologyGrade.SelectedValue = ((Grade10Subject)student.GetSubject("biology")).Grade;
            chemistryGrade.SelectedValue = ((Grade10Subject)student.GetSubject("chemistry")).Grade;
            geographyGrade.SelectedValue = ((Grade10Subject)student.GetSubject("geography")).Grade;
            historyGrade.SelectedValue = ((Grade10Subject)student.GetSubject("history")).Grade;
            civicsGrade.SelectedValue = ((Grade10Subject)student.GetSubject("civics")).Grade;
        }

        //Disable Grade selection radio buttons
        G10RadioButton.Enabled = false;
        G12RadioButton.Enabled = false;
        
        System.Diagnostics.Debug.WriteLine("adding grade10 subjects");
    }

    private void AddG12Subjects(Student student)
    {
        Boolean StudentNotNull;
        if (student == null)
            StudentNotNull = false;
        else StudentNotNull = true;
        //TODO: Code to add subjects with grades of student object in session in AddG12Subjects(Student)
        //Add English Controls
        Label englishLabel = NewSubjectLabel("English", "control-label col-sm-2", "english");
        TextBox englishTB = NewSubjectTextBox("english", "form-control"); 
        Panel englishPanel = new Panel { CssClass = "form-group" };
        englishPanel.Controls.Add(englishLabel);
        englishPanel.Controls.Add(englishTB);
        AddScoreRangeValidator(englishPanel, "english", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(englishPanel);
        //Add Math Controls
        Label mathLabel = NewSubjectLabel("Math", "control-label col-sm-2", "math");
        TextBox mathTB = NewSubjectTextBox("math", "form-control"); 
        Panel mathPanel = new Panel { CssClass = "form-group" };
        mathPanel.Controls.Add(mathLabel);
        mathPanel.Controls.Add(mathTB);
        AddScoreRangeValidator(mathPanel, "math", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(mathPanel);
        //Add Aptitude Controls
        Label aptitudeLabel = NewSubjectLabel("Aptitude", "control-label col-sm-2", "aptitude");
        TextBox aptitudeTB = NewSubjectTextBox("aptitude", "form-control");
        Panel aptitudePanel = new Panel { CssClass = "form-group" };
        aptitudePanel.Controls.Add(aptitudeLabel);
        aptitudePanel.Controls.Add(aptitudeTB);
        AddScoreRangeValidator(aptitudePanel, "aptitude", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(aptitudePanel);
        //Add Civics Controls
        Label civicsLabel = NewSubjectLabel("Civics", "control-label col-sm-2", "civics");
        TextBox civicsTB = NewSubjectTextBox("civics", "form-control");
        Panel civicsPanel = new Panel { CssClass = "form-group" };
        civicsPanel.Controls.Add(civicsLabel);
        civicsPanel.Controls.Add(civicsTB);
        AddScoreRangeValidator(civicsPanel, "civics", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(civicsPanel);
        //Add Physics Controls
        Label physicsLabel = NewSubjectLabel("Physics", "control-label col-sm-2", "physics");
        TextBox physicsTB = NewSubjectTextBox("physics", "form-control");
        Panel physicsPanel = new Panel { CssClass = "form-group" };
        physicsPanel.Controls.Add(physicsLabel);
        physicsPanel.Controls.Add(physicsTB);
        AddScoreRangeValidator(physicsPanel, "physics", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(physicsPanel);
        //Add Chemistry Controls
        Label chemistryLabel = NewSubjectLabel("Chemistry", "control-label col-sm-2", "chemistry");
        TextBox chemistryTB = NewSubjectTextBox("chemistry", "form-control");
        Panel chemistryPanel = new Panel { CssClass = "form-group" };
        chemistryPanel.Controls.Add(chemistryLabel);
        chemistryPanel.Controls.Add(chemistryTB);
        AddScoreRangeValidator(chemistryPanel, "chemistry", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(chemistryPanel);
        //Add Biology Controls
        Label biologyLabel = NewSubjectLabel("Biology", "control-label col-sm-2", "biology");
        TextBox biologyTB = NewSubjectTextBox("biology", "form-control");
        Panel biologyPanel = new Panel { CssClass = "form-group" };
        biologyPanel.Controls.Add(biologyLabel);
        biologyPanel.Controls.Add(biologyTB);
        AddScoreRangeValidator(biologyPanel, "biology", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(biologyPanel);
        //Add Economics Controls
        Label economicsLabel = NewSubjectLabel("Economics", "control-label col-sm-2", "economics");
        TextBox economicsTB = NewSubjectTextBox("economics", "form-control");
        Panel economicsPanel = new Panel { CssClass = "form-group" };
        economicsPanel.Controls.Add(economicsLabel);
        economicsPanel.Controls.Add(economicsTB);
        AddScoreRangeValidator(economicsPanel, "economics", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(economicsPanel);
        //Add History Controls
        Label historyLabel = NewSubjectLabel("History", "control-label col-sm-2", "history");
        TextBox historyTB = NewSubjectTextBox("history", "form-control");
        Panel historyPanel = new Panel { CssClass = "form-group" };
        historyPanel.Controls.Add(historyLabel);
        historyPanel.Controls.Add(historyTB);
        AddScoreRangeValidator(historyPanel, "history", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(historyPanel);
        //Add Geography Controls
        Label geograpyLabel = NewSubjectLabel("Geography", "control-label col-sm-2", "geography");
        TextBox geographyTB = NewSubjectTextBox("geography", "form-control");
        Panel geographyPanel = new Panel { CssClass = "form-group" };
        geographyPanel.Controls.Add(geograpyLabel);
        geographyPanel.Controls.Add(geographyTB);
        AddScoreRangeValidator(geographyPanel, "geography", "text-danger col-sm-offset-2");
        SubjectsPanel.Controls.Add(geographyPanel);
        //Set gfroupingText for Subjects Added
        SubjectsPanel.GroupingText = "Grade 12 Subjects";

        //Set the appropriate values for each subject if a student with some sourses is available 
        //mainly used to restore student in session between postbacks and/or registration fails.
        if (StudentNotNull)
        {
            englishTB.Text = "" + ((Grade12Subject)student.GetSubject("english")).Score;
            mathTB.Text = "" + ((Grade12Subject)student.GetSubject("math")).Score;
            aptitudeTB.Text = "" + ((Grade12Subject)student.GetSubject("aptitude")).Score;
            civicsTB.Text = "" + ((Grade12Subject)student.GetSubject("civics")).Score;
            physicsTB.Text = "" + ((Grade12Subject)student.GetSubject("physics")).Score;
            biologyTB.Text = "" + ((Grade12Subject)student.GetSubject("biology")).Score;
            chemistryTB.Text = "" + ((Grade12Subject)student.GetSubject("chemistry")).Score;
            historyTB.Text = "" + ((Grade12Subject)student.GetSubject("history")).Score;
            geographyTB.Text = "" + ((Grade12Subject)student.GetSubject("geography")).Score;
            economicsTB.Text = "" + ((Grade12Subject)student.GetSubject("economics")).Score;

        }
        //Disable Grade Selection Radio Buttons
        G10RadioButton.Enabled = false;
        G12RadioButton.Enabled = false;
        
        System.Diagnostics.Debug.WriteLine("adding grade12 subjects");

    }

    private Student GetG10Student()
    {
        Student student = new Student();
        try
        {
            student.RegistrationNo = int.Parse(RegNo.Text);
            student.Year = short.Parse(Year.Text);
        }
        catch (FormatException fe) { return null; }
        catch (OverflowException oe) { return null; }
        //Todo :handle exception code or add validation and validity of null student to be inserted in certificatesDAL

        student.Name = Name.Text;
        student.FName = FName.Text;
        student.LName = LName.Text;

        //A student object always has all the subjects of his grade but some or all grades or scores can be null
        //which is because of the methods GetSubjectGrade("subjectid") and GetSubjectScore("subjectid")
        //in this case a grade 10 student with Grade10Subject property
        Grade10Subject english = new Grade10Subject { SubjectName = "English", Grade = GetSubjectGrade("english") };
        Grade10Subject amharic = new Grade10Subject { SubjectName = "Amharic", Grade = GetSubjectGrade("amharic") };
        Grade10Subject mathematics = new Grade10Subject { SubjectName = "Mathematics", Grade = GetSubjectGrade("mathematics") };
        Grade10Subject physics = new Grade10Subject { SubjectName = "Physics", Grade = GetSubjectGrade("physics") };
        Grade10Subject biology = new Grade10Subject { SubjectName = "Biology", Grade = GetSubjectGrade("biology") };
        Grade10Subject chemistry = new Grade10Subject { SubjectName = "Chemistry", Grade = GetSubjectGrade("chemistry") };
        Grade10Subject geography = new Grade10Subject { SubjectName = "Geography", Grade = GetSubjectGrade("geography") };
        Grade10Subject history = new Grade10Subject { SubjectName = "History", Grade = GetSubjectGrade("history") };
        Grade10Subject civics = new Grade10Subject { SubjectName = "Civics", Grade = GetSubjectGrade("civics") };

        //adding the subjects to student object's Subjects property.
        student.Subjects.Add(english);
        student.Subjects.Add(amharic);
        student.Subjects.Add(mathematics);
        student.Subjects.Add(physics);
        student.Subjects.Add(biology);
        student.Subjects.Add(chemistry);
        student.Subjects.Add(geography);
        student.Subjects.Add(history);
        student.Subjects.Add(civics);
        
        
        System.Diagnostics.Debug.WriteLine("geting grade10 studet");
        return student;
    }

    private Student GetG12Student()
    {
        Student student = new Student();

        try
        {
            student.RegistrationNo = int.Parse(RegNo.Text);
            student.Year = short.Parse(Year.Text);
        }
        catch (FormatException fe) { return null; }
        catch (OverflowException oe) { return null; }
        //Todo :handle exception code or add validation and validity of null student to be inserted in certificatesDAL


        student.Name = Name.Text;
        student.FName = FName.Text;
        student.LName = LName.Text;

        //A student object always has all the subjects of his grade but some or all grades or scores can be null
        //which is because of the methods GetSubjectGrade("subjectid") and GetSubjectScore("subjectid")
        //in this case a grade 10 student with Grade10Subject property
        Grade12Subject english = new Grade12Subject { SubjectName = "English", Score = GetSubjectScore("english") };
        Grade12Subject aptitude = new Grade12Subject { SubjectName = "Aptitude", Score = GetSubjectScore("aptitude") };
        Grade12Subject math = new Grade12Subject { SubjectName = "Math", Score = GetSubjectScore("math") };
        Grade12Subject physics = new Grade12Subject { SubjectName = "Physics", Score = GetSubjectScore("physics") };
        Grade12Subject biology = new Grade12Subject { SubjectName = "Biology", Score = GetSubjectScore("biology") };
        Grade12Subject chemistry = new Grade12Subject { SubjectName = "Chemistry", Score = GetSubjectScore("chemistry") };
        Grade12Subject geography = new Grade12Subject { SubjectName = "Geography", Score = GetSubjectScore("geography") };
        Grade12Subject history = new Grade12Subject { SubjectName = "History", Score = GetSubjectScore("history") };
        Grade12Subject civics = new Grade12Subject { SubjectName = "Civics", Score = GetSubjectScore("civics") };
        Grade12Subject economics = new Grade12Subject { SubjectName = "Economics", Score = GetSubjectScore("economics") };

        //adding the subjects to student object's Subjects property.
        student.Subjects.Add(english);
        student.Subjects.Add(aptitude);
        student.Subjects.Add(math);
        student.Subjects.Add(physics);
        student.Subjects.Add(biology);
        student.Subjects.Add(chemistry);
        student.Subjects.Add(geography);
        student.Subjects.Add(history);
        student.Subjects.Add(civics);
        student.Subjects.Add(economics);
        
      
        System.Diagnostics.Debug.WriteLine("getting grade12 student");
        return student;
    }

    private void ShowResultMessage(String message, String CssClass){
        HyperLink link = new HyperLink(); link.NavigateUrl = "#"; link.CssClass = "close"; link.Text = "&times;";
        link.Attributes.Add("data-dismiss", "alert");
        link.Attributes.Add("aria-label", "close");
        Label result = new Label();
        result.Text = message;
        ResultPanel.Controls.Add(link);
        ResultPanel.Controls.Add(result);
        ResultPanel.CssClass = CssClass;
    }

    protected void addSubject_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("add subject click");
        int grade = GetGrade();
        
        SubjectsPanel.Controls.Clear();
        if (grade == 10)
        {
            AddG10Subjects(null);
        }
        else if (grade == 12)
        {
            AddG12Subjects(null);
        }
        Session["SubjectsAdded"] = true;
    }

    protected void removeAllSubjects_Click(object sender, EventArgs e)
    {
        SubjectsPanel.Controls.Clear();
        SubjectsPanel.GroupingText = "";
        //TODO: CLEAN UP CODE AFTER REMOVING SUBJECTS and reflect changes in student object in sesson by replacing it    
        G10RadioButton.Enabled = true;
        G12RadioButton.Enabled = true;
        //removeAllSubjects.Enabled = false;
        Session["SubjectsAdded"] = false;
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        //check if subjects are added
        if (Session["SubjectsAdded"] != null && (Boolean)Session["SubjectsAdded"])
        {
            //used to check successful execution by comparing with number of rows returned after insert statement
            int successfulInsert = 0;
            int grade = GetGrade();
            CertificatesDAL certificates = new CertificatesDAL();
            String connectionString = ConfigurationManager.ConnectionStrings["CertificatesSqlProvider"].ConnectionString;
            certificates.OpenConnection(connectionString);

            if (grade == 10)
            {
                Student G10Student = GetG10Student();
                try
                {
                    if (G10Student != null)
                        successfulInsert = certificates.InsertStudentRecord(G10Student, 10);
                    else
                        ShowResultMessage("Invalid Student Registration No!", "alert alert-danger");
                }
                catch (System.Data.SqlClient.SqlException sqle)
                {
                    if (sqle.Number == 2627)
                    { //the number 2627 indicates a primary key conflict and therefore the record already exists and is double
                        ShowResultMessage("The Student Record Already Exists!", "alert alert-danger");
                        successfulInsert = 2;
                        RestoreStudentInSession();
                    }
                }
            }
            else if (grade == 12)
            {
                Student G12Student = GetG12Student();
                try
                {
                    if (G12Student != null)
                        successfulInsert = certificates.InsertStudentRecord(G12Student, 12);
                    else
                        ShowResultMessage("Invalid Student Registration No!", "alert alert-danger");
                }
                catch (System.Data.SqlClient.SqlException sqle)
                {
                    if (sqle.Number == 2627)
                    { //the number 2627 indicates a primary key conflict and therefore the record already exists and is double
                        ShowResultMessage("The Student Record Already Exists!", "alert alert-danger");
                        successfulInsert = 2;
                        RestoreStudentInSession();
                    }
                }
            }
            // No Student record(row) added
            if (successfulInsert == 0)
            {
                ShowResultMessage("Sorry the Operation Failed!", "alert alert-danger");
                //todo: insert restore code if submit fails
                RestoreStudentInSession();

            }
            // 1 Student record(row) Has been Added Successfully
            else if (successfulInsert == 1)
            {
                ShowResultMessage("A Student Record Has Been Successfully Added!", "alert alert-success");
                G10RadioButton.Enabled = true;
                G12RadioButton.Enabled = true;
                SubjectsPanel.GroupingText = "";
                //clear variables
                Session["SubjectsAdded"] = null;
                Session["Student"] = null;
            }
        }
        else ShowResultMessage("No Subjects Added!", "alert alert-info");
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
           // String imageName = Path.GetFileName(FileUpload.PostedFile.FileName);
            String photoName = Year.Text + "_" + RegNo.Text + ".jpg";
            FileUpload.PostedFile.SaveAs(Server.MapPath("~/Images/") +photoName );
            Photo.ImageUrl = "Images/" + photoName;
            Photo.Height = Unit.Parse("200px");
            Photo.Width = Unit.Parse("200px");
        }
        if (Session["SubjectsAdded"] != null && (Boolean)Session["SubjectsAdded"])
            RestoreStudentInSession();
    }

    /*
    private void ListControlsInPanel()
    {
         string theInfo = "";
         theInfo = string.Format("<b>Does the panel have controls? {0} </b><br/>", 
         SubjectsPanel.HasControls());

        // Get all controls in the panel.
         foreach (Control c in SubjectsPanel.Controls)
         {
            if (!object.ReferenceEquals(c.GetType(),
             typeof(System.Web.UI.LiteralControl)))
            {
                theInfo += "***************************<br/>";
                theInfo += string.Format("Control Name? {0} <br/>", c.ToString());
                theInfo += string.Format("ID? {0} <br>", c.ID);
                theInfo += string.Format("Control Visible? {0} <br/>", c.Visible);
                theInfo += string.Format("ViewState? {0} <br/>", c.EnableViewState);  
            }
         }
         TextBox1.Text = theInfo;
    }
    
    private DropDownList NewG12DropDownList()
    {
        DropDownList G12SubjectsList = new DropDownList();
        G12SubjectsList.Text = "Grade 12 Subjects";
        G12SubjectsList.CssClass = "form-control";
        ListItem englishG12 = new ListItem("English");
        ListItem mathG12 = new ListItem("Math");
        ListItem aptitude = new ListItem("Aptitude");
        ListItem physicsG12 = new ListItem("Physics");
        ListItem chemistryG12 = new ListItem("Chemistry");
        ListItem biologyG12 = new ListItem("Biology");
        ListItem civicsG12 = new ListItem("Civics");
        ListItem economics = new ListItem("Economics");
        ListItem historyG12 = new ListItem("History");
        ListItem geographyG12 = new ListItem("Geography");

        G12SubjectsList.Items.Add(englishG12);
        G12SubjectsList.Items.Add(mathG12);
        G12SubjectsList.Items.Add(aptitude);
        G12SubjectsList.Items.Add(physicsG12);
        G12SubjectsList.Items.Add(chemistryG12);
        G12SubjectsList.Items.Add(biologyG12);
        G12SubjectsList.Items.Add(civicsG12);
        G12SubjectsList.Items.Add(economics);
        G12SubjectsList.Items.Add(historyG12);
        G12SubjectsList.Items.Add(geographyG12);
        return G12SubjectsList;
    }*/

    protected void test_for_dynamic_form(object sender, EventArgs e)
    {
        String info = "<hr>loook</hr> ";
        String form = Request.Form[0];
        string key = Request.Form.GetKey(4);
        String KeyValue = Request.Form[key];
        string f2 = "" + Request.Form.Count;
        info = form + info;
        info += Request.Form[4];
        info += Request.Form.Get("Year");
        info += Request.Form.Get("ctl00$MainContent$english");
        info += Request.Form.Get("math");
        
        TextBox1.Text = info;
    }
}