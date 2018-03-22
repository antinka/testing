using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Identity;
using Testing.DAL.Interfaces;
using Testing.DAL.Repositories.Connection;

namespace Testing.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TestingContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private SubjectRepositories subjectRepositories;
        private TestRepositories testRepositories;
        private SubjectTestRepository subjectTestRepository;
        private TestQuestionRepository testQuestionRepository;
        private QuestionRepository questionRepository;
        private QuestionAnswerRepository questionAnswerRepository;
        private AnswerRepository answerRepository;
        private TestDifficultRepository testDifficultRepository;
        private StudenRepository studenRepository;
        private StudentTestResultRepository studentTestResultRepository;
        private StudentExamResultRepository studentExamResultRepository;
        private AnswerGivenByStudentRepository answerGivenByStudentRepository;
        private SubjectExamRepository subjectExamRepository;
        private ExamQuestionRepository examQuestionRepository;
        private ExamRepository examRepository;
        private CommentToExamResultRepository commentToExamResultRepository;
        private OpenAnswerGivenByStutentRepository OpenAnswerGivenByStutentRepository;
        private ExamOpenAnswerByStdepository ExamOpenAnswerByStdepository;
        

        public EFUnitOfWork(string connectionString)
        {
            db = new TestingContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            //studenRepository = new StudenRepository(db);
        }

        public IRepository<CommentToExamResult> CommentToExamResults
        {
            get
            {
                if (commentToExamResultRepository == null)
                    commentToExamResultRepository = new CommentToExamResultRepository(db);
                return commentToExamResultRepository;
            }
        }

        public IRepository<OpenAnswersGivenByStutent> OpenAnswerGivenByStutents
        {
            get
            {
                if (OpenAnswerGivenByStutentRepository == null)
                    OpenAnswerGivenByStutentRepository = new OpenAnswerGivenByStutentRepository(db);
                return OpenAnswerGivenByStutentRepository;
            }
        }

        public IRepository<ExamOpenAnswerByStd> ExamOpenAnswerByStds
        {
            get
            {
                if (ExamOpenAnswerByStdepository == null)
                    ExamOpenAnswerByStdepository = new ExamOpenAnswerByStdepository(db);
                return ExamOpenAnswerByStdepository;
            }
        }

        public IRepository<StudentExamResult> StudentExamResults
        {
            get
            {
                if (studentExamResultRepository == null)
                    studentExamResultRepository = new StudentExamResultRepository(db);
                return studentExamResultRepository;
            }
        }

        public IRepository<SubjectExam> SubjectExams
        {
            get
            {
                if (subjectExamRepository == null)
                    subjectExamRepository = new SubjectExamRepository(db);
                return subjectExamRepository;
            }
        }

        public IRepository<ExamQuestion> ExamQuestions
        {
            get
            {
                if (examQuestionRepository == null)
                    examQuestionRepository = new ExamQuestionRepository(db);
                return examQuestionRepository;
            }
        }

        public IRepository<Exam> Exams
        {
            get
            {
                if (examRepository == null)
                    examRepository = new ExamRepository(db);
                return examRepository;
            }
        }

        public IRepository<StudentTestResult> StudentTestResults
        {
            get
            {
                if (studentTestResultRepository == null)
                    studentTestResultRepository = new StudentTestResultRepository(db);
                return studentTestResultRepository;
            }
        }

        public IRepository<AnswerGivenByStudent> AnswerGivenByStudents
        {
            get
            {
                if (answerGivenByStudentRepository == null)
                    answerGivenByStudentRepository = new AnswerGivenByStudentRepository(db);
                return answerGivenByStudentRepository;
            }
        }

        public IRepository<StudentProfile> StudentProfiles
        {
            get
            {
                if (studenRepository == null)
                    studenRepository = new StudenRepository(db);
                return studenRepository;
            }
        }

        public IRepository<TestDifficult> TestDifficults
        {
            get
            {
                if (testDifficultRepository == null)
                    testDifficultRepository = new TestDifficultRepository(db);
                return testDifficultRepository;
            }
        }

        public IRepository<TestQuestion> TestQuestions
        {
            get
            {
                if (testQuestionRepository == null)
                    testQuestionRepository = new TestQuestionRepository(db);
                return testQuestionRepository;
            }
        }

        public IRepository<Question> Questions
        {
            get
            {
                if (questionRepository == null)
                    questionRepository = new QuestionRepository(db);
                return questionRepository;
            }
        }

        public IRepository<QuestionAnswer> QuestionAnswers
        {
            get
            {
                if (questionAnswerRepository == null)
                    questionAnswerRepository = new QuestionAnswerRepository(db);
                return questionAnswerRepository;
            }
        }

        public IRepository<Answer> Answers
        {
            get
            {
                if (answerRepository == null)
                    answerRepository = new AnswerRepository(db);
                return answerRepository;
            }
        }

        public IRepository<Subject> Subjects
        {
            get
            {
                if (subjectRepositories == null)
                    subjectRepositories = new SubjectRepositories(db);
                return subjectRepositories;
            }
        }

        public IRepository<Test> Tests
        {
            get
            {
                if (testRepositories == null)
                    testRepositories = new TestRepositories(db);
                return testRepositories;
            }
        }

        public IRepository<SubjectTest> SubjectTests
        {
            get
            {
                if (subjectTestRepository == null)
                    subjectTestRepository = new SubjectTestRepository(db);
                return subjectTestRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public void Dispose()
        {
          userManager.Dispose();
          roleManager.Dispose();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

    }
}
