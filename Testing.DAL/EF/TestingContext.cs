using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Repositories;
using Testing.DAL.Repositories.Connection;

namespace Testing.DAL.EF
{
    public class TestingContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<SubjectTest> SubjectTests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<SubjectExam> SubjectExams { get; set; }
        public DbSet<OpenAnswersGivenByStutent> OpenAnswerGivenByStutents{ get; set; }

        public DbSet<ExamOpenAnswerByStd> ExamOpenAnswerByStds { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerGivenByStudent> AnswerGivenByStudents { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<StudentTestResult> StudentTestResults { get; set; }
        public DbSet<StudentExamResult> StudentExamResults { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestDifficult> TestDifficults { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<CommentToExamResult> CommentToExamResults { get; set; }
        
        public TestingContext(string conectionString) 
            : base(conectionString) { }

        public class TestingDbInitializer 
            : DropCreateDatabaseIfModelChanges<TestingContext>
        {
        }

       
    }
}