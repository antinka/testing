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
    public class SubjectController : Controller
    {
        ISubjectService subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }
        //Return all subject.
        public ActionResult ViewSubject()
        {
            return View(subjectService.GetSubjects());
        }
        //Add new subject.
        [HttpGet]
        public ActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(SubjectDTO subjectDTO)
        {
            if (ModelState.IsValid)
            {
                subjectDTO.Id = Guid.NewGuid();
                Logger.Log.Info("User " + User.Identity.GetUserId() + "add subject " + subjectDTO.Id);
                subjectService.AddNewSubject(subjectDTO);
            }
            return RedirectToAction("ViewSubject");
        }
        //Delete subject.
        public ActionResult DeleteSubject(Guid id)
        {
            subjectService.DeleteSubject(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete subject " + id);
            return RedirectToAction("ViewSubject");
        }
        //Edit subject.
        [HttpGet]
        public ActionResult EditSubject(Guid id)
        {
            SubjectDTO subjectDTO = subjectService.GetSubjectById(id);
            if (subjectDTO != null)
            {
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit subject " + id);
                return View(subjectDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditSubject(SubjectDTO subjectDTO)
        {
            if (ModelState.IsValid)
            {
                subjectService.UpdateSubject(subjectDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "save changes subject ");
            }
            return RedirectToAction("ViewSubject");
        }

    }
}