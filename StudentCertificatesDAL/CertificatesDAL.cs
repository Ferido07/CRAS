using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for CertificatesDAL
/// </summary>
namespace StudentCertificatesDAL
{
    public class CertificatesDAL
    {
        private SqlConnection conn = null;

        public void OpenConnection(String connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }
        public void CloseConnection()
        {
            conn.Close();
        }

        private void AddParameter(SqlCommand command, String parameterName, Object parameterValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            if (parameterValue == null) param.Value = DBNull.Value;
            else param.Value = parameterValue;
            command.Parameters.Add(param);
        }
        public Student FindGrade10Certificate(int regNo, int year)
        {
            Student student = new Student();

            String sql = "SELECT RegNo, Year, Name, FName, LName, " +
                                 "English, Amharic, Mathematics, " +
                                 "Physics, Biology, Chemistry, " +
                                 "Geography, History ,Civics " +
                         "FROM Grade10Score " +
                         "WHERE RegNo=@RegNo and Year=@Year";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                AddParameter(command, "@RegNo", regNo);
                AddParameter(command, "@Year", year);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    student.RegistrationNo = (int)reader["RegNo"];
                    student.Year = (short)reader["Year"];
                    student.Name = (String)reader["Name"];
                    student.FName = (String)reader["FName"];
                    student.LName = (String)reader["LName"];
                    for (int i = 5; i < reader.FieldCount; i++)
                    {
                        if (reader[i] != null)
                        {
                            Grade10Subject subject = new Grade10Subject();
                            // student.Subjects = new List<Subject>();
                            subject.SubjectName = reader.GetName(i);
                            if (reader.IsDBNull(i))
                            {
                                subject.Grade = null;
                            }
                            else
                                subject.Grade = (String)reader.GetSqlString(i);
                            if (subject.Grade != null)
                                student.Subjects.Add(subject);
                        }
                    }
                }
                else return null;
            }
            return student;
        }

        public Student FindGrade12Certificate(int regNo, int year)
        {
            Student student = new Student();
            String sql = "SELECT RegNo, Year, Name, FName, LName, " +
                                 "English, Math, Aptitude, Physics, " +
                                 "Chemistry, Biology, Civics, Economics, " +
                                 "History, Geography " +
                         "FROM Grade12Score " +
                         "WHERE RegNo=@RegNo and Year=@Year";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                AddParameter(command, "@RegNo", regNo);
                AddParameter(command, "@Year", year);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    student.RegistrationNo = (int)reader["RegNo"];
                    student.Year = (short)reader["Year"];
                    student.Name = (String)reader["Name"];
                    student.FName = (String)reader["FName"];
                    student.LName = (String)reader["LName"];
                    for (int i = 5; i < reader.FieldCount; i++)
                    {
                        if (reader[i] != null)
                        {
                            Grade12Subject subject = new Grade12Subject();
                            // student.Subjects = new List<Subject>();
                            subject.SubjectName = reader.GetName(i);
                            if (reader.IsDBNull(i))
                                subject.Score = null;
                            else
                                subject.Score = (Byte)reader.GetSqlByte(i);
                            if (subject.Score != null)
                                student.Subjects.Add(subject);
                        }
                    }
                }
                else return null;
            }
            return student;
        }

        public void UpdateStudentRecord(Student student, int grade)
        {
            if (grade == 12)
            {
                short? englishScore = ((Grade12Subject)student.GetSubject("English")).Score;
                short? mathScore = ((Grade12Subject)student.GetSubject("Math")).Score;
                short? aptitudeScore = ((Grade12Subject)student.GetSubject("Aptitude")).Score;
                short? physicsScore = ((Grade12Subject)student.GetSubject("Physics")).Score;
                short? chemistryScore = ((Grade12Subject)student.GetSubject("Chemistry")).Score;
                short? biologyScore = ((Grade12Subject)student.GetSubject("Biology")).Score;
                short? civicsScore = ((Grade12Subject)student.GetSubject("Civics")).Score;
                short? economicsScore = ((Grade12Subject)student.GetSubject("Economics")).Score;
                short? historyScore = ((Grade12Subject)student.GetSubject("History")).Score;
                short? geographyScore = ((Grade12Subject)student.GetSubject("Geography")).Score;

                String sql = "Update Grade12Score " +
                             "Set Name='" + student.Name + "',FName='" + student.FName + "',LName='" + student.LName + "'," +
                             "English='" + englishScore + "',Math='" + mathScore + "',Aptitude='" + aptitudeScore + "'," +
                             "Physics='" + physicsScore + "',Chemistry='" + chemistryScore + "',Biology='" + biologyScore + "'," +
                             "Civics='" + civicsScore + "',Economics='" + economicsScore + "',History='" + historyScore + "',Geography='" + geographyScore + "' " +
                             "Where RegNo=" + student.RegistrationNo + " and [Year]=" + student.Year;

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            else if (grade == 10)
            {
                String englishGrade = ((Grade10Subject)student.GetSubject("English")).Grade;
                String amharicGrade = ((Grade10Subject)student.GetSubject("Amharic")).Grade;
                String mathemathicsGrade = ((Grade10Subject)student.GetSubject("Mathemathics")).Grade;
                String physicsGrade = ((Grade10Subject)student.GetSubject("Physics")).Grade;
                String biologyGrade = ((Grade10Subject)student.GetSubject("Biology")).Grade;
                String chemistryGrade = ((Grade10Subject)student.GetSubject("Chemistry")).Grade;
                String geographyGrade = ((Grade10Subject)student.GetSubject("Geography")).Grade;
                String historyGrade = ((Grade10Subject)student.GetSubject("History")).Grade;
                String civicsGrade = ((Grade10Subject)student.GetSubject("Civics")).Grade;

                String sql = "Update Grade10Score " +
                             "Set Name='" + student.Name + "',FName='" + student.FName + "',LName='" + student.LName + "'," +
                             "English='" + englishGrade + "',Amharic='" + amharicGrade + "',Mathemathics='" + mathemathicsGrade + "'," +
                             "Physics='" + physicsGrade + "',Biology='" + biologyGrade + "',Chemistry='" + chemistryGrade + "'," +
                             "Geography='" + geographyGrade + "',History='" + historyGrade + "',Civics='" + civicsGrade + "' " +
                             "Where RegNo=" + student.RegistrationNo + " and [Year]=" + student.Year;
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            else throw new ArgumentOutOfRangeException("" + grade, "Grade parameter must be either '10' or '12'");
        }

      /*  public ArrayList G10ManuallyAddedStudentRecords(int? regNo, short? year)
        {
            ArrayList list = new ArrayList();
            //TODO: ManuallyAddedG10Students
            using (SqlCommand command = new SqlCommand("G10ManuallyAddedRecords", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (regNo != null)
                    AddParameter(command, "@RegNo", regNo);
                if (year != null)
                    AddParameter(command, "@Year", year);

                SqlDataReader reader = command.ExecuteReader();
                GetManullyAddedRecordsFromResultSet(reader, list);
            }
            return list;
        }
        public ArrayList G12ManuallyAddedStudentRecords(int? regNo, short? year)
        {
            //TODO: ManuallyAdddedG12Students
            ArrayList list = new ArrayList();
            using (SqlCommand command = new SqlCommand("G12ManuallyAddedRecords", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (regNo != null)
                    AddParameter(command, "@RegNo", regNo);
                if (year != null)
                    AddParameter(command, "@Year", year);

                SqlDataReader reader = command.ExecuteReader();
                GetManullyAddedRecordsFromResultSet(reader, list);
            }
            return list;
        }
        public ArrayList GBothManuallyAddedStudentRecords(int? regNo, short? year)
        {
            ArrayList list = new ArrayList();
            //TODO: Manually added students
            using (SqlCommand command = new SqlCommand("GBothManuallyAddedRecords", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (regNo != null)
                    AddParameter(command, "@RegNo", regNo);
                if (year != null)
                    AddParameter(command, "@Year", year);

                SqlDataReader reader = command.ExecuteReader();
                list = GetManullyAddedRecordsFromResultSet(reader, list);
                //an if clause could be used because it wud only return 2 result sets but if it needs
                // to evolve while makes it more flexible in case more are added even though its highly unlikely
                while (reader.NextResult())
                {
                    list = GetManullyAddedRecordsFromResultSet(reader, list);
                }
            }
            return list;
        }
       */ 
        public ArrayList GetManuallyAddedRecords(int? regNo, short? year, int? grade)
        {
            ArrayList list = new ArrayList();
            //TODO: Manually added students
            using (SqlCommand command = new SqlCommand())
            {
                //set the connection
                command.Connection = conn;
                //set commandText based on grade
                if (grade == 10)
                    command.CommandText = "G10ManuallyAddedRecords";
                else if (grade == 12)
                    command.CommandText = "G12ManuallyAddedRecords";
                else if (grade == null)
                    command.CommandText = "GBothManuallyAddedRecords";
                else
                    throw new ArgumentOutOfRangeException("" + grade, "Grade must be either 10 or 12 or null for both");
                //set commandtype as storedproceedure
                command.CommandType = CommandType.StoredProcedure;
                //add parameters
                if (regNo != null)
                    AddParameter(command, "@RegNo", regNo);
                if (year != null)
                    AddParameter(command, "@Year", year);
                //execute
                SqlDataReader reader = command.ExecuteReader();
                //get list of records from result set
                list = GetManullyAddedRecordsFromResultSet(reader, list);
                //if grade is both that is null then perform additional step to get records from the 
                //next result set
                if (grade == null)
                {
                    //an if clause could be used because it wud only return 2 result sets but if it needs
                    // to evolve while makes it more flexible in case more are added even though its highly unlikely
                    while (reader.NextResult())
                        list = GetManullyAddedRecordsFromResultSet(reader, list);
                }
            }
            return list;
        } 

        private ArrayList GetManullyAddedRecordsFromResultSet(SqlDataReader reader, ArrayList list)
        {
            while (reader.Read())
            {
                StudentReport StudentReport = new StudentReport();
                StudentReport.Name = (String)reader["Name"] + " " + (String)reader["FName"] + " " + (String)reader["LName"];
                StudentReport.RegistrationNo = (int)reader["RegNo"];
                StudentReport.Year = (short)reader["Year"];
                StudentReport.Grade = (int)reader["Grade"];
                list.Add(StudentReport);
            }
            return list;
        }

        public int InsertStudentRecord(Student student, int grade)
        {
            //todo student object check before starting insert procedure
            if (grade == 12)
            {
                //getting each subject score and storing them in local variables
                short? englishScore = ((Grade12Subject)student.GetSubject("English")).Score;
                short? mathScore = ((Grade12Subject)student.GetSubject("Math")).Score;
                short? aptitudeScore = ((Grade12Subject)student.GetSubject("Aptitude")).Score;
                short? physicsScore = ((Grade12Subject)student.GetSubject("Physics")).Score;
                short? chemistryScore = ((Grade12Subject)student.GetSubject("Chemistry")).Score;
                short? biologyScore = ((Grade12Subject)student.GetSubject("Biology")).Score;
                short? civicsScore = ((Grade12Subject)student.GetSubject("Civics")).Score;
                short? economicsScore = ((Grade12Subject)student.GetSubject("Economics")).Score;
                short? historyScore = ((Grade12Subject)student.GetSubject("History")).Score;
                short? geographyScore = ((Grade12Subject)student.GetSubject("Geography")).Score;

                String sql = "Insert into Grade12Score(RegNo,[Year],Name,FName,LName,English,Math,Aptitude,Physics," +
                                                      "Chemistry,Biology,Civics,Economics,History,Geography) values ( @RegNo, @Year, " +
                                                      "@Name, @FName, @LName, @English, @Math, @Aptitude, @Physics, @Chemistry, " +
                                                      "@Biology, @Civics, @Economics, @History, @Geography)";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    AddParameter(command, "@RegNo", student.RegistrationNo);
                    AddParameter(command, "@Year", student.Year);
                    AddParameter(command, "@Name", student.Name);
                    AddParameter(command, "@FName", student.FName);
                    AddParameter(command, "@LName", student.LName);
                    AddParameter(command, "@English", englishScore);
                    AddParameter(command, "@Math", mathScore);
                    AddParameter(command, "@Aptitude", aptitudeScore);
                    AddParameter(command, "@Physics", physicsScore);
                    AddParameter(command, "@Chemistry", chemistryScore);
                    AddParameter(command, "@Biology", biologyScore);
                    AddParameter(command, "@Civics", civicsScore);
                    AddParameter(command, "@Economics", economicsScore);
                    AddParameter(command, "@History", historyScore);
                    AddParameter(command, "@Geography", geographyScore);
                    return command.ExecuteNonQuery();
                }
            }
            else if (grade == 10)
            {
                String englishGrade = ((Grade10Subject)student.GetSubject("English")).Grade;
                String amharicGrade = ((Grade10Subject)student.GetSubject("Amharic")).Grade;
                String mathemathicsGrade = ((Grade10Subject)student.GetSubject("Mathematics")).Grade;
                String physicsGrade = ((Grade10Subject)student.GetSubject("Physics")).Grade;
                String biologyGrade = ((Grade10Subject)student.GetSubject("Biology")).Grade;
                String chemistryGrade = ((Grade10Subject)student.GetSubject("Chemistry")).Grade;
                String geographyGrade = ((Grade10Subject)student.GetSubject("Geography")).Grade;
                String historyGrade = ((Grade10Subject)student.GetSubject("History")).Grade;
                String civicsGrade = ((Grade10Subject)student.GetSubject("Civics")).Grade;

                String sql = "Insert into Grade10Score(RegNo,[Year],Name,FName,LName,English,Amharic,Mathematics,Physics," +
                                                     "Biology,Chemistry,Geography,History,Civics) values ( @RegNo, @Year, " +
                                                     "@Name, @FName, @LName, @English, @Amharic, @Mathematics, @Physics, " +
                                                     "@Biology, @Chemistry, @Geography, @History, @Civics)";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    AddParameter(command, "@RegNo", student.RegistrationNo);
                    AddParameter(command, "@Year", student.Year);
                    AddParameter(command, "@Name", student.Name);
                    AddParameter(command, "@FName", student.FName);
                    AddParameter(command, "@LName", student.LName);
                    AddParameter(command, "@English", englishGrade);
                    AddParameter(command, "@Amharic", amharicGrade);
                    AddParameter(command, "@Mathematics", mathemathicsGrade);
                    AddParameter(command, "@Physics", physicsGrade);
                    AddParameter(command, "@Chemistry", chemistryGrade);
                    AddParameter(command, "@Biology", biologyGrade);
                    AddParameter(command, "@Civics", civicsGrade);
                    AddParameter(command, "@History", historyGrade);
                    AddParameter(command, "@Geography", geographyGrade);
                    return command.ExecuteNonQuery();
                }
            }
            else throw new ArgumentOutOfRangeException("" + grade, "Grade parameter must be either '10' or '12'");
        }
        //end of class
    }      

    public class Student
    {
        private List<Subject> subjects= new List<Subject>();
        public String Name { set; get; }
        public String FName { set; get; }
        public String LName { set; get; }
        public int RegistrationNo { set; get; }
        public short Year { set; get; }
        public List<Subject> Subjects { set { this.subjects = value; } get { return this.subjects; } }

        private int GetSubjectIndex(String subjectName)
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                if (String.Equals(subjects[i].SubjectName,subjectName,StringComparison.OrdinalIgnoreCase) ){ return i; }
            }
            throw new System.ArgumentOutOfRangeException(subjectName, "No such subject exists");
        }
        //Looks for suject in the list of subjects a student has taken nad returns it 
        //returns null if not found
        public Subject GetSubject(String subjectName)
        {
            Subject subject=null;
            try { subject = subjects[GetSubjectIndex(subjectName)]; }
            catch (ArgumentOutOfRangeException e)
            {
               //the exeception thrown by GetSubjectIndex() if the subject is not in the list is
               //caught and a null subject is returned
            }
            return subject;   
        }
        
        public static Boolean CheckStudentValidity(Student student)
        {
            Boolean valid = false;
            //todo complete Student.CheckStudentValidity method before database insertion

            return valid;
        }
    }

    public class Subject
    {
        //must change the name of the property but that leads to change in the gridview column name
        //private String subjectName;
        public String SubjectName { set; get; }
    }
    public class Grade12Subject : Subject
    {
        public short? Score { set; get; }
        public static short? findTotal(List<Grade12Subject> subjects)
        {
            short? total = 0;
            foreach (var subject in subjects)
            {
                total += subject.Score;
            }
            return total;
        }
    }

    public class Grade10Subject : Subject
    {
        public String Grade { set ; get; }
    }

    public class StudentReport
    {
        public String Name { set; get; }
        public int RegistrationNo { set; get; }
        public short Year { set; get; }
        public int Grade { set; get; }
    }
}