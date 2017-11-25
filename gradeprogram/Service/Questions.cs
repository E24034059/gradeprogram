using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gradeprogram.Models;
using gradeprogram.Models.Interface;
using gradeprogram.Models.Repository;
using gradeprogram.Service.Interface;
using gradeprogram.Service;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace gradeprogram.Service
{
    public class Questions : IQuestions
    {
        private IRepository<Correct_Answer> CorrectAnsrepository = new CorrectDBGenericRepository<Correct_Answer>();
        private IRepository<Class> Classrepository = new CorrectDBGenericRepository<Class>();
        private IRepository<Take_Course> HWNumrepository = new CorrectDBGenericRepository<Take_Course>();
        private IRepository<HW_Exam> HWrepository = new CorrectDBGenericRepository<HW_Exam>();

        private IRepository<Program_Question> Questionrepository = new HintDBGenericRepository<Program_Question>();
        private IRepository<Program_Answer> Answerrepository = new HintDBGenericRepository<Program_Answer>();
        private IRepository<QuestionIndex> QIndexrepository = new CorrectDBGenericRepository<QuestionIndex>();
        private IRepository<QuestionMode> QModerepository = new CorrectDBGenericRepository<QuestionMode>();

        public IResult Create(QuestionObj instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                CreateQuestionHelper createOne = new CreateQuestionHelper();
                
                
                createOne.creatOnequestion(instance);
                
                this.Questionrepository.Create(createOne.question);
                this.Answerrepository.Create(createOne.answer);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

       public IResult Update(Correct_Answer instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.CorrectAnsrepository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int CourseID,string HWNum)
        {
            IResult result = new Result(false);

            if (!this.IsExists(CourseID , HWNum))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(CourseID,HWNum);
                this.CorrectAnsrepository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int CourseID,string HWNum)
        {
            return this.CorrectAnsrepository.GetAll().Any(x => x.Course_ID == CourseID && x.HW_Exam_Number == HWNum);
        }

        public Correct_Answer GetByID(int CourseID,string HWNum)
        {
            return this.CorrectAnsrepository.Get(x => x.Course_ID == CourseID && x.HW_Exam_Number == HWNum);
        }

        public IEnumerable<Correct_Answer> GetAll()
        {
            return this.CorrectAnsrepository.GetAll();
        }


        public IEnumerable<Class> GetAllClass()
        {
            return this.Classrepository.GetAll();
        }
        public IEnumerable<Take_Course> GetAllHWNum()
        {
            return this.HWNumrepository.GetAll();
        }

        public string GetClassName(int CourseID)
        {
            return this.Classrepository.Get(x => x.Course_ID == CourseID).Course_Name;
        }
        public string GetHWNum(int CourseID,string tagName,string selectedValue)
        {

           
            var HWdata = this.CorrectAnsrepository.GetAll().Where(x => x.Course_ID == CourseID);
            int i = 0;
            Dictionary<string, string> optionData = new Dictionary<string, string>();
            string keyText = string.Empty;
            foreach (var item in HWdata)
            {
                keyText = string.Concat(item.HW_Exam_Number.Trim());
                if (optionData.Keys.Where(x => x == keyText).Count().Equals(0))
                {
                    optionData.Add(keyText,i.ToString());
                    i+=1;
                }
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                tagName = "ClassName";
            }
            string _html = DropDownListHelper.GetDropdownList( tagName, optionData, selectedValue, null, true,"選擇一個作業");
            return _html;
        }
    }
}