using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RazorPDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL.DTO.View;
using Testing.BLL.Interfaces;
using Rotativa;
namespace Testing.WEB.Controllers
{
    public class GeneratePdfController : Controller
    {
        IExamCheck examCheck;
        public GeneratePdfController(IExamCheck examCheck)
        {
            this.examCheck = examCheck;
        }
        public ActionResult ExamResult(Guid idExam, Guid IdOpenAnswer)
        {
          
            ViewForExamPdf listViewForExamPdf = examCheck.GenerateExamResultPdf(idExam, IdOpenAnswer);

            //return new PdfResult(listViewForExamPdf, "ExamResult");
            return new ViewAsPdf(listViewForExamPdf);
            // return new PdfResult(null, "ExamResult");
            // return View(listViewForExamPdf);
            //return listViewForExamPdf;
        }

    }
}