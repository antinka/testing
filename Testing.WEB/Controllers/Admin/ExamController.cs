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

namespace Testing.WEB.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class ExamController : Controller
    {
        IExamService examService;
        public ExamController(IExamService examService)
        {
            this.examService = examService;
        }

        //Return all exam to choosen subject.
        public ActionResult ViewExam(Guid id)
        {
            ViewBag.subjectId = id;
            if (examService.CountExamForSubject(id) < 1)
                return View("ViewEmptyExam");

            return View(examService.ReturnViewExamSub(id));
        }
        //Add new exam to subject which id=subjectId.
        [HttpGet]
        public ActionResult AddExam(Guid subjectId)
        {
            ViewBag.subjectId = subjectId;
            return View();
        }
        [HttpPost]
        public ActionResult AddExam(ExamDTO examDTO, Guid subjectId)
        {
            if (ModelState.IsValid)
            {
                examDTO.Id = Guid.NewGuid();
                examService.AddNewTExam(examDTO);
                examService.AddNewCOnnectionSubjectExam(examDTO.Id, subjectId);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "add new exam " + examDTO.Id + " to subject " + subjectId);
                return RedirectToAction("ViewExam", new { id = subjectId});
            }
            return RedirectToAction("ViewExam", new { id = subjectId });
        }

        // Delete exam from db.
        public ActionResult DeleteExam(Guid id, Guid subjectId)
        {
            examService.DeleteExam(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete exam " + id);
            return RedirectToAction("ViewExam", new { id = subjectId });
        }

        // Edit exam.
        [HttpGet]
        public ActionResult EditExam(Guid id, Guid subjectId)
        {
            ExamDTO examDTO = examService.GetExamById(id);
            if (examDTO != null)
            {
                ViewBag.subjectId = subjectId;
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit test " + id);
                return View(examDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditExam(ExamDTO examDTO, Guid subjectId)
        {
            if (ModelState.IsValid)
            {
                examService.UpdateExam(examDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "save changes test ");
            }
            return RedirectToAction("ViewExam", new { id = subjectId });
        }
    }
}