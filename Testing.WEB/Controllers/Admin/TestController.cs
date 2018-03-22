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
    public class TestController : Controller
    {
        ITestService testService;
        ITestDifficultService testDifficultService;
        public TestController(ITestService testService, ITestDifficultService testDifficultService)
        {
            this.testService = testService;
            this.testDifficultService = testDifficultService;
        }

        //Return all test to choosen subject.
        public ActionResult ViewTest(Guid id)
        {
            ViewBag.subjectId = id;
            if (testService.CountTestForSubject(id) < 1)
                return View("ViewEmptyTest");

             return View(testService.ReturnViewTestSub(id));
        }
        //Add new test to subject which id=subjectId.
        [HttpGet]
        public ActionResult AddTest(Guid subjectId)
        {
           ViewBag.difficult = new SelectList(testDifficultService.GetTestDifficult(), "Id", "Difficult"); 
           ViewBag.subjectId = subjectId;
           return View();
        }
        [HttpPost]
        public ActionResult AddTest(TestView testView, Guid difficultId, Guid subjectId)
        {
            if (ModelState.IsValid && difficultId!=null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestView, TestDTO>());
                IMapper mapper = config.CreateMapper();
                TestDTO testDTO = new TestDTO();
                testDTO = mapper.Map<TestView, TestDTO>(testView);
                testDTO.CountQuestion = 0;
                testDTO.Id = Guid.NewGuid();
                testService.AddNewTest(testDTO, difficultId);
                testService.AddNewCOnnectionSubjectTest(testDTO.Id, subjectId);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "add new test " + testDTO.Id + " to subject " + subjectId);
                return RedirectToAction("ViewTest", new { id = subjectId, difficultId = testDTO.Id });
            }
            return RedirectToAction("ViewTest", new { id = subjectId });
        }
        // Delete test from db.
        public ActionResult DeleteTest(Guid id, Guid subjectId)
        {
            testService.DeleteTest(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete test " + id);
            return RedirectToAction("ViewTest", new { id = subjectId });
        }

        // Edit test.
        [HttpGet]
        public ActionResult EditTest(Guid id, Guid subjectId)
        {
            ViewBag.difficult = new SelectList(testDifficultService.GetTestDifficult(), "id", "Difficult");
            TestDTO testDTO = testService.GetTestById(id);
            if (testDTO != null)
            {
                ViewBag.subjectId = subjectId;
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit test " + id);
                return View(testDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditTest(TestDTO testDTO, Guid subjectId)
        {
            if (ModelState.IsValid)
            {
                testService.UpdateTest(testDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "save changes test ");
            }
            return RedirectToAction("ViewTest",new { id = subjectId });
        }
    }
}