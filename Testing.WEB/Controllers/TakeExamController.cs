using Microsoft.AspNet.Identity;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.Interfaces;

namespace Testing.WEB.Controllers
{
    [Authorize]
    public class TakeExamController : Controller
    {
        IQuestionService questionService;
        IExamService examService;
        IExamResultService examResultService;
        ISubjectService subjectService;
        IOpenAnswerGivenByStutenService openAnswerGivenByStutenService;
        public TakeExamController(IExamResultService examResultService, IOpenAnswerGivenByStutenService openAnswerGivenByStutenService,
            IExamService examService, ISubjectService subjectService, ITestService testService, IQuestionService questionService)
        {
            this.subjectService = subjectService;
            this.questionService = questionService;
            this.examService = examService;
            this.openAnswerGivenByStutenService = openAnswerGivenByStutenService;
            this.examResultService = examResultService;
        }

        // Return all exams which exist in db choosen by subject and sorted (grid).
        public ActionResult ViewExams(Guid? id)
        {
            ViewBag.subject = new SelectList(subjectService.GetSubjects(), "Id", "Name");
            Guid idSubj = id ?? Guid.Empty;
            ViewBag.id = id;
            if (idSubj == Guid.Empty)
            {
                return View(examService.ReturnViewExamSub(idSubj));
            }
            else
            {
                if (examService.CountExamForSubject(idSubj) < 1)
                    return View("ViewEmptyExam");
                return View(examService.ReturnViewExamSub(idSubj));
            }
        }

        //Start passing the exam.
        public ActionResult Exam(Guid id)
        {
            DateTime timeStart = DateTime.Now;
            ViewBag.timeStart = timeStart;
            ViewBag.Time = examService.GetExamById(id).Runtime;
            ViewBag.idExam = id;
            Logger.Log.Info("User " + User.Identity.GetUserId() + "start passing the exam");
            return View(questionService.ReturnQuestionExam(id));
        }

        //End passing exam.
        public ActionResult SaveAnswersToExam(string[] answers, Guid idExam, DateTime timeStart)
        {
            DateTime timeEnd = DateTime.Now;
            Guid idExamRes=examResultService.AddStudExamRes(User.Identity.GetUserId(), timeStart, timeEnd);
            string SumUpAnswerToExam = string.Empty;
            if (answers.Length != 0)
            {
                for (int i = 0; i < answers.Length; i++)
                {
                    SumUpAnswerToExam += "Ответ на " + (i+1) + " вопрос: " + answers[i]+". ";
                }
                Guid answerId = openAnswerGivenByStutenService.AddNewOpenAnswer(SumUpAnswerToExam, User.Identity.GetUserId());
                openAnswerGivenByStutenService.AddNewConnectionExamAnswer(idExam, answerId, idExamRes);
            }
            else
            {
                Guid emptyAnswernswerId = openAnswerGivenByStutenService.AddNewOpenAnswer(SumUpAnswerToExam, User.Identity.GetUserId());
                openAnswerGivenByStutenService.AddNewConnectionExamEmptyAnswer(idExam, emptyAnswernswerId, idExamRes);
            }
            Logger.Log.Info("User " + User.Identity.GetUserId() + "end passing the test");
            return View("InfoAfterPassExam");
        }
      
    }
}