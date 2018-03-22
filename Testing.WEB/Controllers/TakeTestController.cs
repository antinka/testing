using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities.Connection;

namespace Testing.WEB.Controllers
{
    [Authorize]
    public class TakeTestController : Controller
    {
        ISubjectService subjectService;
        ITestService testService;
        IQuestionService questionService;
        ITestResultService testResultService;
        public TakeTestController(ITestResultService testResultService, ISubjectService subjectService, ITestService testService, IQuestionService questionService)
        {
            this.subjectService = subjectService;
            this.testService= testService;
            this.questionService = questionService;
            this.testResultService = testResultService;
        }

        // Return all test which exist in db choosen by subject and sorted (grid)
        public ActionResult ViewTest(Guid? id)
        { 
            ViewBag.subject = new SelectList(subjectService.GetSubjects(), "Id", "Name");
            Guid idSubj = id ?? Guid.Empty;
            if (idSubj == Guid.Empty)
            {
                return View(testService.ReturnViewTestSub(idSubj));
            }
            else
            {
                ViewBag.id = id;
                if (testService.CountTestForSubject(idSubj) < 1)
                    return View("ViewEmptyTest");
                return View(testService.ReturnViewTestSub(idSubj));
            }
        }

        //Start passing the test.
        public ActionResult Test(Guid id)
        {
            DateTime timeStart = DateTime.Now;
            ViewBag.timeStart = timeStart;
            ViewBag.Time = testService.GetTestById(id).Runtime;
            ViewBag.idTest = id;
            Logger.Log.Info("User " + User.Identity.GetUserId() + "start passing the test");
            return View(questionService.ReturnQuestionWithAnswers(id));
        }

        //End passing test, count result.
        public ActionResult Result(string [] questionId, string[] answerIdQuestionId,Guid idTest, DateTime timeStart)
        {
            DateTime timeEnd = DateTime.Now;
            Guid studResultId= testResultService.AddStudResultReturnId(User.Identity.GetUserId(), idTest, timeStart, timeEnd);
             Logger.Log.Info("User " + User.Identity.GetUserId() + "end passing the test");

            //for (int i = 0; i < questionId.Length; i++)
            //{
            //    bool giveAnsw = false;
            //    int count = 0;
            //    if (answerIdQuestionId != null)
            //    {
            //        for (int j = 0; j < answerIdQuestionId.Length; j++)
            //        {
            //            string[] quesAnsw = answerIdQuestionId[j].Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
            //            if ((new Guid(questionId[i])) == (new Guid(quesAnsw[0])))
            //            {
            //                testResultService.AddAnswerGivenByStud((new Guid(quesAnsw[0])), (new Guid(quesAnsw[1])), studResultId);
            //                giveAnsw = true;
            //            }
            //            else
            //            {
            //                count++;
            //            }
            //        }
            //        if (count == answerIdQuestionId.Length && giveAnsw == false)
            //        {
            //            testResultService.AddAnswerGivenByStud((new Guid(questionId[i])), (Guid.Empty), studResultId);
            //            break;
            //        }
            //        Logger.Log.Info("User " + User.Identity.GetUserId() + "didnt give any answer");
            //    }
            //    else
            //    {
            //        testResultService.AddAnswerGivenByStud((new Guid(questionId[i])), (Guid.Empty), studResultId);
            //    }
            //}

            for (int i = 0; i < questionId.Length; i++)
            {
                if (answerIdQuestionId != null)
                {
                    bool find = false;
                    for (int j = 0; j < answerIdQuestionId.Length; j++)
                    {
                        string[] quesAnsw = answerIdQuestionId[j].Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                        if ((new Guid(questionId[i])) == (new Guid(quesAnsw[0])))
                        {
                            testResultService.AddAnswerGivenByStud((new Guid(quesAnsw[0])), (new Guid(quesAnsw[1])), studResultId);
                            find = true;
                        }
                    }
                    if (find == false)
                    {
                        testResultService.AddAnswerGivenByStud((new Guid(questionId[i])), (Guid.Empty), studResultId);
                    }
                }
                else
                {
                    testResultService.AddAnswerGivenByStud((new Guid(questionId[i])), (Guid.Empty), studResultId);
                }
            }
            double mark = testResultService.CountMarkForTest(idTest, studResultId);
            testResultService.UpdateStudResult(mark, studResultId);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "markr "+ mark);
            ViewBag.mark = mark;
            ViewBag.timeStart = timeStart;
            ViewBag.timeEnd = timeEnd;
            return View();
        }
    }
}