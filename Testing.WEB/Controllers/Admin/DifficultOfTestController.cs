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
    public class DifficultOfTestController : Controller
    {
        ITestDifficultService testDifficultService;
        public DifficultOfTestController( ITestDifficultService testDifficultService)
        {
            this.testDifficultService = testDifficultService;
        }
        
        //Return all exist difficult.
        public ActionResult ViewDifficultOfTest()
        {
            return View(testDifficultService.GetTestDifficult());
        }

        //Add new difficult.
        [HttpGet]
        public ActionResult AddDifficultOfTest()
        { 
                return View();
        }
        [HttpPost]
        public ActionResult AddDifficultOfTest(TestDifficultDTO testDifficultDTO)
        {
            if (ModelState.IsValid)
            {
                testDifficultDTO.Id = Guid.NewGuid();
                testDifficultService.AddNewTestDifficult(testDifficultDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "add difficult " + testDifficultDTO.Id);
            }
            return RedirectToAction("ViewDifficultOfTest");
        }

        //Delete difficult.
        public ActionResult DeleteDifficultOfTest(Guid id)
        {
            testDifficultService.DeleteTestDifficult(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "delete difficult " + id);
            return RedirectToAction("ViewDifficultOfTest");
        }

        //Edit difficult.
        [HttpGet]
        public ActionResult EditDifficultOfTest(Guid id)
        {
            TestDifficultDTO testDifficultDTO = testDifficultService.GetTestDifficultById(id);
            if (testDifficultDTO != null)
            {
                Logger.Log.Info("User " + User.Identity.GetUserId() + "edit difficult " + id);
                return View(testDifficultDTO);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditDifficultOfTest(TestDifficultDTO testDifficultDTO)
        {
            if (ModelState.IsValid)
            {
                testDifficultService.UpdateTestDifficult(testDifficultDTO);
                Logger.Log.Info("User " + User.Identity.GetUserId() + "save changes editing difficult ");
            }
            return RedirectToAction("ViewDifficultOfTest");
        }
    }
}