using Ninject.Modules;
using Testing.BLL.Interfaces;
using Testing.BLL.Services;

namespace Testing.WEB.Util
{
    public class TestingModule: NinjectModule
    {
        public override void Load()
        {

            Bind<ISubjectService>().To<SubjectService>();
            Bind<ITestService>().To<TestService>();
            Bind<IQuestionService>().To<QuestionService>();
            Bind<IAnswerService>().To<AnswerService>();
            Bind<ITestDifficultService>().To<TestDifficultService>();
            Bind<ITestResultService>().To<TestResultService>();
            Bind<IExamResultService>().To<ExamResultService>();
            Bind<IExamService>().To<ExamService>();
            Bind<IOpenAnswerGivenByStutenService>().To<OpenAnswerGivenByStutenService>();
            Bind<IExamCheck>().To<ExamCheck>();
        }
    }
}