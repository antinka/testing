using RazorPDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL.DTO.View;
using Testing.BLL.Interfaces;

namespace Testing.WEB.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class CheckExamController : Controller
    {

        ISubjectService subjectService;
        IExamCheck examCheck;
        IExamResultService examResultService;
        public CheckExamController(ISubjectService subjectService, IExamCheck examCheck, IExamResultService examResultService)
        {
            this.subjectService = subjectService;
            this.examCheck = examCheck;
            this.examResultService = examResultService;
        }

        // GET: CheckExam
        public ActionResult ViewNotCheckExams(Guid? id)
        {
            ViewBag.subject = new SelectList(subjectService.GetSubjects(), "Id", "Name");
            Guid idSubj = id ?? Guid.Empty;
            ViewBag.id = id;
            return View(examCheck.GetNotCheckExams(idSubj));
        }
        [HttpGet]
        public ActionResult ViewCheckExam(Guid id)
        {
            return View(examCheck.CheckExam(id));
        }
        [HttpPost]
        public ActionResult ViewCheckExam(Guid studentExamResultId,int mark, string commentToExam, Guid idConnectionExamAnsw)
        {
            examResultService.UpdateStudResult(mark, studentExamResultId, idConnectionExamAnsw);
            examResultService.AddCommentToCheckExam(studentExamResultId, commentToExam);
            return RedirectToAction("ViewNotCheckExams");
        }
    }
}