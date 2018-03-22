using System;
using System.Threading.Tasks;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Identity;
using Testing.DAL.Repositories;

namespace Testing.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Exam> Exams { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<Test> Tests { get; }
        IRepository<SubjectTest> SubjectTests { get; }
        IRepository<TestQuestion> TestQuestions { get; }
        IRepository<ExamQuestion> ExamQuestions { get; }
        IRepository<SubjectExam> SubjectExams { get; }

        IRepository<OpenAnswersGivenByStutent> OpenAnswerGivenByStutents { get; }
        IRepository<ExamOpenAnswerByStd> ExamOpenAnswerByStds { get; }

        IRepository<Question> Questions { get; }
        IRepository<QuestionAnswer> QuestionAnswers { get; }
        IRepository<Answer> Answers { get; }
        IRepository<TestDifficult> TestDifficults { get; }
        IRepository<StudentProfile> StudentProfiles { get; }
        IRepository<AnswerGivenByStudent> AnswerGivenByStudents { get; }
        IRepository<StudentTestResult> StudentTestResults { get; }
        IRepository<StudentExamResult> StudentExamResults { get; }
        IRepository<CommentToExamResult> CommentToExamResults { get; }

        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
