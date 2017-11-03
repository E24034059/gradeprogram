using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using gradeprogram.Models;
using gradeprogram.ViewModels;
using gradeprogram.Models.Interface;
using gradeprogram.Models.Repository;
using gradeprogram.Service.Interface;
using gradeprogram.Service;
using static gradeprogram.App_Start.constants;

namespace gradeprogram.Controllers
{
    public class CreateQuestionController : Controller
    {
        private IQuestions CreateQuestionService;

        public CreateQuestionController()
        {
            this.CreateQuestionService = new Questions();
        }

        private IEnumerable<Class> ClassData { get { return this.CreateQuestionService.GetAllClass().Distinct(); } }
        

        public ActionResult CreateQuestion()
        {
            var model = new CreateQuestionViewModel
            {

                PropertyParameter = new AnswerPropertyModel(),
                ClassData = new SelectList(this.ClassData, "Course_ID", "Course_Name"),
                HW_ExamData = null,
                sparatestring = App_Start.constants.segmentsCut
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult GetHWData(string Course_ID)
        {
            //  List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            string id = string.Empty;
            string tagName = "HWNum";
            string HWdatas = string.Empty;
            if (!string.IsNullOrWhiteSpace(Course_ID))
            {
                HWdatas = this.CreateQuestionService.GetHWNum(Course_ID, tagName, null);
            }
            return Content(HWdatas);
        }

        [HttpPost]
        public ActionResult QuestionUpload( FormCollection formCollection)
        {
            QuestionObj OneQuestion = new QuestionObj();
            OneQuestion.CourseID = formCollection["Course_ID"];
            OneQuestion.question = formCollection["question"];
            OneQuestion.outputformat = formCollection["outputformat"];
            OneQuestion.testinput = formCollection["testinput"];
            OneQuestion.answer = formCollection["answer"];
            OneQuestion.setCQID();
            this.CreateQuestionService.Create(OneQuestion);                         

            return RedirectToAction("CreateQuestion");
        }

    }
}