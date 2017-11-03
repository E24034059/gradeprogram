using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gradeprogram.Models;
using gradeprogram.ViewModels;
using gradeprogram.Models.Interface;
using gradeprogram.Models.Repository;
using gradeprogram.Service.Interface;
using gradeprogram.Service;

namespace gradeprogram.Controllers
{
    public class TaskCorrectionController :Controller
    {
        private ITaskCorrection TaskCorrectionService;

        public TaskCorrectionController()
        {
            this.TaskCorrectionService = new TaskCorrection();
        }

        private IEnumerable<Class> ClassData { get { return this.TaskCorrectionService.GetAllClass().Distinct(); } }
        private List<SelectListItem> assignmentType { get {
                List<SelectListItem> HWorExerList = new List<SelectListItem>();
                HWorExerList.AddRange(new[]{
                  new SelectListItem() {Text = "HW_Exam", Value = "HW_Exam", Selected = false},
                  new SelectListItem() {Text = "Exercise", Value = "Exercise", Selected = false}
                                     });
                return HWorExerList;
            } }
        public ActionResult TaskCorrection()
        {
            var model = new TaskCorrectionViewModel
            {
                PropertyParameter = new HWPropertyModel(),
                ClassData = new SelectList(this.ClassData, "Course_ID", "Course_Name"),
                HW_ExamData = null           
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GetHWData(string Course_ID,string assignmentType)
        {
            //  List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            string id = string.Empty;
            string tagName = "HWNum";
            string HWdatas = string.Empty;
            if (!string.IsNullOrWhiteSpace(Course_ID))
            {
                if (assignmentType == "HW_Exam")
                {
                    HWdatas = this.TaskCorrectionService.GetHWNum(Course_ID, tagName, null);
                }
                else if (assignmentType == "Exercise")
                {
                    HWdatas = this.TaskCorrectionService.GetExerciseData(Course_ID, tagName, null);
                }
            }
            return Content(HWdatas);
        }
        [HttpPost]
        public ActionResult GetTypeData(string Course_ID)
        {
            //  List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            string id = string.Empty;
            string tagName = "assignmentType";
            string Typedatas = string.Empty;
            if (!string.IsNullOrWhiteSpace(Course_ID))
            {
                Typedatas = this.TaskCorrectionService.GetTypes(Course_ID, tagName, null);
            }
            return Content(Typedatas);
        }

        [HttpPost]
        public bool CheckIsAnswerExist(string Course_ID, string HWNum)
        {
           return this.TaskCorrectionService.IsAnswerExists(Course_ID, HWNum);
        }
        

        [HttpPost]
        public ActionResult CorrectTask(string Course_ID,string assignmentType,string HWNum)
        {
          
            this.TaskCorrectionService.CorrectTask( Course_ID, assignmentType, HWNum);

            return RedirectToAction("TaskCorrection");
        }
    }

          
}