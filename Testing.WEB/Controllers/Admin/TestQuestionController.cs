using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.WEB.Models.Testing;

namespace Testing.WEB.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class TestQuestionController : Controller
    {
        IQuestionService questionService;
        IAnswerService answerService;
        ITestService testService;
        public TestQuestionController(IQuestionService questionService, IAnswerService answerService, ITestService testService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
            this.testService = testService;
        }
        //Return all question to test id = id
        [HttpGet]
        public ActionResult ViewQuestion(Guid subjectId, Guid id)
        {
           
            ViewBag.subjectId = subjectId;
            ViewBag.idTest = id;
            if (questionService.CountQuestionForTest(id) < 1)
                return View("NoQuestionInTest");

            return View(questionService.ReturnQuestionTest(id));
        }

        //Add question to test id= idtest
        [HttpGet]
        public ActionResult AddQuestion(Guid subjectId, Guid idTest)
        {
            ViewBag.subjectId = subjectId;
            ViewBag.idTest = idTest;
            return View();
        }
        [HttpPost]
        public ActionResult AddQuestion(string question, string[] aswer, Guid idTest, Guid subjectId)
        {
            ViewBag.subjectId = subjectId;
            ViewBag.idTest = idTest;
            for (int i = 0; i < aswer.Length; i++)
            {
                if (string.IsNullOrEmpty(aswer[i]))
                    ModelState.AddModelError("aswer", "Некорректный ввод ответа");
            }
            if (string.IsNullOrEmpty(question))
            {
                ModelState.AddModelError("question", "Некорректный ввод вопроса");
            }

            if (aswer.Length < 2)
            {
                ModelState.AddModelError("aswer", "Недопустимое количество ответов, их должно быть минимум 2");
            }
            if (ModelState.IsValid)
            { 
                QuestionDTO questionDTO = new QuestionDTO();
                questionDTO.Id = Guid.NewGuid();
                questionDTO.QuestionTitle = question;
                TempData["question"] = questionDTO;

                AnswerDTO[] ansvers = new AnswerDTO[aswer.Length];

                for (int i = 0; i < aswer.Length; i++)
                {
                    AnswerDTO answerDTO = new AnswerDTO();
                    answerDTO.AnswerTitle = aswer[i];
                    answerDTO.Id = Guid.NewGuid();
                    ansvers[i] = answerDTO;
                    TempData["ansvers"] = ansvers;
                }
                return RedirectToAction("AddRightAnswerToTest", new { questionId = questionDTO.Id, ansversc = ansvers.Length, idTest = idTest, subjectId = subjectId });
            }
            return View("AddQuestion", new { question= question, aswer= aswer, idTest = idTest, subjectId = subjectId });
            
        }

        //Return View to mark right answer to question, work only after ActionResult AddQuestion.
        [HttpGet]
        public ActionResult AddRightAnswerToTest(Guid questionId,int ansversc, Guid idTest, Guid subjectId)
        {
            QuestionDTO question = TempData["question"] as QuestionDTO;
            if (question != null)
            {
                ViewBag.question = question.QuestionTitle;
            }
            AnswerDTO[] ansvers = TempData["ansvers"] as AnswerDTO[];
            if(ansvers!=null)
            {
                ViewBag.ansver = ansvers;
            }
            else
            {
                ansvers = TempData["ansversBack"] as AnswerDTO[];
                ViewBag.ansver = ansvers;
            }
           
            ViewBag.questionId = questionId;
            ViewBag.idTest = idTest;
            ViewBag.subjectId = subjectId;

            TempData["ansvers"] = ansvers;
            TempData["question"] = question;
            return View();
        }
        [HttpPost]
        public ActionResult AddRightAnswerToTest(Guid[] allAnswersId, Guid questionId, Guid idTest, Guid subjectId, Guid[] rightAnswersId)
        {
            if (rightAnswersId != null)
            {
                AnswerDTO[] ansversSave = TempData["ansvers"] as AnswerDTO[];
                QuestionDTO question = TempData["question"] as QuestionDTO;
                questionService.AddNewQuestion(question);

                TestDTO testDTO = testService.GetTestById(idTest);
                //testDTO.CountQuestion += 1;
                testService.UpdateTest(testDTO);

                questionService.AddNewCOnnectionTestQuestion(idTest, question.Id);
                for (int i = 0; i < ansversSave.Length; i++)
                {
                    answerService.AddNewAnswer(ansversSave[i]);
                }

                int count = 0;
                foreach (var ansv in allAnswersId)
                {
                    if (rightAnswersId != null)
                    {
                        count = 0;
                        foreach (Guid id in rightAnswersId)
                        {
                            count++;
                            if (ansv == id)
                            {

                                answerService.AddNewConnectionQuestionAnswer(questionId, ansv, true);
                                break;
                            }
                            if (count == rightAnswersId.Length)
                            {
                                answerService.AddNewConnectionQuestionAnswer(questionId, ansv, false);
                            }
                        }
                    }
                    else
                        answerService.AddNewConnectionQuestionAnswer(questionId, ansv, false);
                }
                return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idTest });
            }
            AnswerDTO[] ansvers = new AnswerDTO[allAnswersId.Length];
            for (int i = 0; i < allAnswersId.Length; i++)
            {
                AnswerDTO answerDTO = new AnswerDTO();
                answerDTO =answerService.GetAnswerById(allAnswersId[i]);
                ansvers[i] = answerDTO;
            }
            Logger.Log.Info("User " + User.Identity.GetUserId() + "add new question and answers to test " + idTest);
            TempData["ansversBack"] = ansvers;
            return RedirectToAction("AddRightAnswerToTest", new {  questionId= questionId, ansversc= allAnswersId.Length, idTest = idTest, subjectId = subjectId });
        }

        //Edit question only.
        [HttpGet]
        public ActionResult EditQuestion(Guid id, Guid subjectId, Guid idTest)
        {
            QuestionDTO questionDTO = questionService.GetQuestionById(id);
            if(questionDTO!=null)
            { 
                ViewBag.idTest = idTest;
                ViewBag.subjectId = subjectId;
                return View(questionDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditQuestion(QuestionDTO questionDTO, Guid subjectId, Guid idTest )
        {
            if (ModelState.IsValid)
            {
                questionService.UpdateQuestion(questionDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit question to test " + idTest);
            }
             return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id= idTest });
        }
        // Delete question
        public ActionResult DeleteQuestion(Guid id, Guid subjectId, Guid idTest)
        {
            questionService.DeleteQuestion(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete question and answers to test " + idTest);

            TestDTO testDTO = testService.GetTestById(idTest);
            testService.UpdateTest(testDTO);

            return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idTest });
        }
    }
}