using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;

namespace Testing.WEB.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class ExamQuestionController : Controller
    {
        IQuestionService questionService;
        IExamService examService;
        public ExamQuestionController(IQuestionService questionService, IExamService examService)
        {
            this.questionService = questionService;
            this.examService = examService;
        }
        //Return all question to exam id = id
        [HttpGet]
        public ActionResult ViewQuestion(Guid subjectId, Guid id)
        {
            ViewBag.subjectId = subjectId;
            ViewBag.idExam = id;
            if (questionService.CountQuestionForExam(id) < 1)
                return View("NoQuestionInExam");

            return View(questionService.ReturnQuestionExam(id));
        }

        //Add question to exam id= idtest
        [HttpGet]
        public ActionResult AddQuestion(Guid subjectId, Guid idExam)
        {
            ViewBag.subjectId = subjectId;
            ViewBag.idExam = idExam;
            return View();
        }
        [HttpPost]
        public ActionResult AddQuestion(QuestionDTO question, Guid subjectId, Guid idExam)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                questionService.AddNewQuestion(question);
                questionService.AddNewCOnnectionExamQuestion(idExam, question.Id);
            }
            return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idExam });
        }

        //Add question to exam id= idtest
        [HttpGet]
        public ActionResult AddExistQuestion(Guid subjectId, Guid idExam)
        {
            ViewBag.subjectId = subjectId;
            ViewBag.idExam = idExam;
            return View(questionService.GetQuestionsNotInCurrentExam(idExam));
        }

        public ActionResult AddExistQuestionToExam(Guid id, Guid subjectId, Guid idExam)
        {
            QuestionDTO question = questionService.GetQuestionById(id);
            QuestionDTO questionCopy = new QuestionDTO();
            questionCopy.QuestionTitle = question.QuestionTitle;
            questionCopy.Id = Guid.NewGuid();
            questionService.AddNewQuestion(questionCopy);
            questionService.AddNewCOnnectionExamQuestion(idExam, questionCopy.Id);
            return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idExam });
        }

        //Edit question.
        [HttpGet]
        public ActionResult EditQuestion(Guid id, Guid subjectId, Guid idExam)
        {
            QuestionDTO questionDTO = questionService.GetQuestionById(id);
            if (questionDTO != null)
            {
                ViewBag.idExam = idExam;
                ViewBag.subjectId = subjectId;
                return View(questionDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditQuestion(QuestionDTO questionDTO, Guid subjectId, Guid idExam)
        {
            if (ModelState.IsValid)
            {
                questionService.UpdateQuestion(questionDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit question to exam " + idExam);
            }
            return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idExam });
        }
        // Delete question
        public ActionResult DeleteQuestion(Guid id, Guid subjectId, Guid idExam)
        {
            questionService.DeleteQuestion(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete question to exam " + idExam);
            return RedirectToAction("ViewQuestion", new { subjectId = subjectId, id = idExam });
        }
    }
}