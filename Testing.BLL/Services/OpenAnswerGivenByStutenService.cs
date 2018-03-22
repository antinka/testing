using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //Class for add different connection exam answer given by std.
    public class OpenAnswerGivenByStutenService : IOpenAnswerGivenByStutenService
    {
        IUnitOfWork Database { get; set; }
        public OpenAnswerGivenByStutenService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddNewConnectionExamAnswer(Guid examId, Guid answersId,Guid studExamRes)
        {
            try
            {
                ExamOpenAnswerByStd openAnswerByStd = new ExamOpenAnswerByStd();
                openAnswerByStd.ExamId = examId;
                openAnswerByStd.StudentExamResultId = studExamRes;
                openAnswerByStd.IsChecked = false;
                openAnswerByStd.OpenAnswerGivenByStutentId = answersId;
                Database.ExamOpenAnswerByStds.Create(openAnswerByStd);
                Database.ExamOpenAnswerByStds.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void AddNewConnectionExamEmptyAnswer(Guid examId, Guid answersId, Guid studExamRes)
        {
            try
            {
                ExamOpenAnswerByStd openAnswerByStd = new ExamOpenAnswerByStd();
                openAnswerByStd.ExamId = examId;
                openAnswerByStd.StudentExamResultId = studExamRes;
                openAnswerByStd.IsChecked = true;
                openAnswerByStd.OpenAnswerGivenByStutentId = answersId;
                Database.ExamOpenAnswerByStds.Create(openAnswerByStd);
                Database.ExamOpenAnswerByStds.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }


        public Guid AddNewOpenAnswer(string openAnswerGivenByStutentDTO, string studId)
        {
            try
            {
                OpenAnswersGivenByStutent openAnswersGivenByStutent = new OpenAnswersGivenByStutent();
                openAnswersGivenByStutent.Answers = openAnswerGivenByStutentDTO;
                openAnswersGivenByStutent.Id = Guid.NewGuid();
                Database.OpenAnswerGivenByStutents.Create(openAnswersGivenByStutent);
                Database.OpenAnswerGivenByStutents.Save();
                return openAnswersGivenByStutent.Id;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                return Guid.Empty;
            }
        }
    }
}
