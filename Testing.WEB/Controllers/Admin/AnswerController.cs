using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;

namespace Testing.WEB.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class AnswerController : Controller
    {
        IQuestionService questionService;
        IAnswerService answerService;
        public AnswerController(IQuestionService questionService, IAnswerService answerService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
        }

        //Return all answer to choosen subject and test.
        public ActionResult ViewAnswer(Guid id, Guid subjectId, Guid idTest)
        {
            ViewBag.questionId = id;
            ViewBag.idTest = idTest;
            ViewBag.subjectId = subjectId;
            return View(questionService.ReturnAllAnswerToQuestion(id));
        }

        //Return form to edit answer to choosen subject and test.
        [HttpGet]
        public ActionResult EditAnswer(Guid id, Guid subjectId, Guid idTest)
        {
            AnswerDTO answerDTO = answerService.GetAnswerById(id);
            if (answerDTO != null)
            {
                ViewBag.idTest = idTest;
                ViewBag.subjectId = subjectId;
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit answer "+ id);
                return View(answerDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditAnswer(AnswerDTO answerDTO, Guid subjectId, Guid idTest)
        {
            if (ModelState.IsValid)
            {
                answerService.UpdateAnswer(answerDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "save changes answer ");
            }
            return RedirectToAction("ViewQuestion", "TestQuestion", new { subjectId = subjectId, id = idTest });
        }
        //Add new answer to test which id=idTest.
        [HttpGet]
        public ActionResult AddAnswer(Guid subjectId, Guid idTest, Guid questionId)
        {
            ViewBag.questionId = questionId;
            return View();
        }
        [HttpPost]
        public ActionResult AddAnswer(AnswerDTO answerDTO, bool? isRight, Guid subjectId, Guid idTest, Guid questionId)
        {
            if (ModelState.IsValid)
            {
                answerDTO.Id = Guid.NewGuid();
                answerService.AddNewAnswer(answerDTO);
                if (isRight != null)
                    answerService.AddNewConnectionQuestionAnswer(questionId, answerDTO.Id, true);
                else
                    answerService.AddNewConnectionQuestionAnswer(questionId, answerDTO.Id, false);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "add new answer to test " + idTest);
            }
            return RedirectToAction("ViewQuestion", "TestQuestion", new { subjectId = subjectId, id = idTest });
        }
        // Delete chosen from db.
        public ActionResult DeleteAnswer(Guid id, Guid subjectId, Guid idTest)
        {
            answerService.DeleteAnswer(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete answer "+ id + " from test " + idTest);
            return RedirectToAction("ViewQuestion", "TestQuestion", new { subjectId = subjectId, id = idTest });
        }
        // Mark answer as right answer to test with id=idTest.
        public ActionResult MarkAsRightAnswer(Guid id, Guid subjectId, Guid idTest)
        {
            answerService.MarkAsRight(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "change right answer "+ id);
            return RedirectToAction("ViewQuestion", "TestQuestion", new { subjectId = subjectId, id = idTest });
        }
        //Mark answer  as wrong answer to test with id=idTest.
        public ActionResult MarkAsWrongAnswer(Guid id, Guid subjectId, Guid idTest)
        {
            answerService.MarkAsWrong(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "change right answer " + id);
            return RedirectToAction("ViewQuestion", "TestQuestion", new { subjectId = subjectId, id = idTest });
        }
    }
}