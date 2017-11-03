using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;
using gradeprogram.Models;
using gradeprogram.Models.Interface;
using gradeprogram.Models.Repository;
using gradeprogram.Service.Interface;
using static gradeprogram.App_Start.constants;

namespace gradeprogram.Service
{
    public class TaskCorrection : ITaskCorrection
    {
        private IRepository<StuCouHWDe_prog> StuCouHWDerepository = new GenericRepository<StuCouHWDe_prog>();
        private IRepository<Class> Classrepository = new GenericRepository<Class>();
        private IRepository<HW_Exam_File> HWFilerepository = new GenericRepository<HW_Exam_File>();
        private IRepository<Correct_Answer> CorrectAnswerrepository = new GenericRepository<Correct_Answer>();
        private IRepository<HW_Exam> HWrepository = new GenericRepository<HW_Exam>();
        private IRepository<ORCS_FileUploadData> Exerciserepository = new GenericRepository<ORCS_FileUploadData>();
        private IRepository<Program_Question> Questionrepository = new GenericRepository<Program_Question>();
        private IRepository<Program_Answer> Answerrepository = new GenericRepository<Program_Answer>();


        public IResult Create(StuCouHWDe_prog instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.StuCouHWDerepository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(StuCouHWDe_prog instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.StuCouHWDerepository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(string StuCouHWDeID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(StuCouHWDeID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(StuCouHWDeID);
                this.StuCouHWDerepository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(string StuCouHWDeID)
        {
            return this.StuCouHWDerepository.GetAll().Any(x => x.StuCouHWDe_ID == StuCouHWDeID);
        }

        public StuCouHWDe_prog GetByID(string StuCouHWDeID)
        {
            return this.StuCouHWDerepository.Get(x => x.StuCouHWDe_ID == StuCouHWDeID);
        }

        public IEnumerable<StuCouHWDe_prog> GetAll()
        {
            return this.StuCouHWDerepository.GetAll();
        }

        public IEnumerable<Class> GetAllClass()
        {
            return this.Classrepository.GetAll();
        }


        public bool IsAnswerExists(string Course_ID ,string HWNum)
        {
            bool success;
            success=this.CorrectAnswerrepository.GetAll().Any(x => x.Course_ID == Course_ID && x.HW_Exam_Number == HWNum && x.CorrectAnswerFilePath != null);
            return success;
        }

        public string GetHWNum(string CourseID, string tagName, string selectedValue)
        {

            var HWdata = this.HWFilerepository.GetAll().Where(x => x.Course_ID == CourseID);
            int i = 0;
            Dictionary<string, string> optionData = new Dictionary<string, string>();
            string keyText = string.Empty;
            foreach (var item in HWdata)
            {
                if (item.HW_Exam_Number.Contains("HW") || item.HW_Exam_Number.Contains("Exam"))
                {
                    keyText = string.Concat(item.HW_Exam_Number.Trim());
                    if (optionData.Keys.Where(x => x == keyText).Count().Equals(0))
                    {
                        optionData.Add(keyText, i.ToString());
                        i += 1;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                tagName = "ClassName";
            }
            string _html = DropDownListHelper.GetDropdownList(tagName, optionData, selectedValue, null, true, "選擇要批改之作業");
            return _html;

        }
        public string GetExerciseData(string CourseID, string tagName, string selectedValue)
        {
            var Exercisedata = this.HWFilerepository.GetAll().Where(x => x.Course_ID == CourseID);
            int i = 0;
            Dictionary<string, string> optionData = new Dictionary<string, string>();
            string keyText = string.Empty;
            foreach (var item in Exercisedata)
            {
                if (item.HW_Exam_Number.Contains("Exercise"))
                {
                    keyText = string.Concat(item.HW_Exam_Number);
                    if (optionData.Keys.Where(x => x == keyText).Count().Equals(0))
                    {
                        optionData.Add(keyText, i.ToString());
                        i += 1;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                tagName = "ClassName";
            }
            string _html = DropDownListHelper.GetDropdownList(tagName, optionData, selectedValue, null, true, "選擇要批改之作業");
            return _html;
        }
        public string GetTypes(string CourseID, string tagName, string selectedValue)
        {
            List<SelectListItem> HWorExerList = new List<SelectListItem>();
            HWorExerList.AddRange(new[]{
                  new SelectListItem() {Text = "HW_Exam", Value = "HW_Exam", Selected = false},
                  new SelectListItem() {Text = "Exercise", Value = "Exercise", Selected = false}
                                     });
            var Typedata = HWorExerList;
            int i = 0;
            Dictionary<string, string> optionData = new Dictionary<string, string>();
            string keyText = string.Empty;
            foreach (var item in Typedata)
            {
                keyText = string.Concat(item.Value.Trim());
                if (optionData.Keys.Where(x => x == keyText).Count().Equals(0))
                {
                    optionData.Add(keyText, i.ToString());
                    i += 1;
                }
            }
            if (string.IsNullOrWhiteSpace(tagName))
            {
                tagName = "assignmentType";
            }
            string _html = DropDownListHelper.GetDropdownList(tagName, optionData, selectedValue, null, true, "選擇類型");
            return _html;
        }
        public void CorrectTask(string CourseID, string assignmentType, string HWNum)
        {
            List<HW_Exam> StuHW = new List<HW_Exam>();
            List<StuCouHWDe_prog> StuHWDe = new List<StuCouHWDe_prog>();
            string HWdata;
            string HWAnswer ;
            string cQID;
            Program_Answer answer;
            if (assignmentType == "HW_Exam")
            {
                HWdata = this.HWFilerepository.Get(x => x.Course_ID == CourseID && x.HW_Exam_Number == HWNum).HW_Exam_FilePath;
                cQID = this.CorrectAnswerrepository.Get(x => x.Course_ID == CourseID && x.HW_Exam_Number == HWNum).CorrectAnswerFilePath;
                HWAnswer = this.Answerrepository.Get(x => x.cQID == cQID).cAnswer_Input;
            }
            else if (assignmentType == "Exercise")
            {
                var exercise = this.Exerciserepository.Get(x => ((x.cExerciseCondID + x.cExerciseID) == HWNum));
                HWdata = exercisePath + @"\" + exercise.cExerciseCondID + @"\" + exercise.cExerciseID;
                cQID = this.CorrectAnswerrepository.Get(x => x.Course_ID == CourseID && x.HW_Exam_Number == HWNum).CorrectAnswerFilePath;
                HWAnswer = this.Answerrepository.Get(x=>x.cQID==cQID).cAnswer_Input;
            }
            else
            {
                HWdata = "ErrorType";
                HWAnswer = "ErrorType";
                cQID = "ErrorType";
            }

            if (HWdata != "ErrorType")
            {
                HWPathHelper HWFilePath = new HWPathHelper(HWdata);
                HWFilePath.HWPathdir();
                AnswerPathHelper CorrectAnswer = new AnswerPathHelper(HWAnswer);
                List<string> fileFullname=new List<string>();
                string fileName ;
                List<CorrectTaskReturn> FinalResult = new List<CorrectTaskReturn>();
                answer = this.Answerrepository.Get(x => x.cQID == cQID);
                DirectoryInfo direct = new DirectoryInfo(HWFilePath.HWProgFilePath);
               
                 foreach (var file in direct.EnumerateFiles("*.cpp", SearchOption.AllDirectories))
                 {
                    if (file.Name.IndexOf("afterMF") == -1 )
                    {
                        fileFullname.Add(file.FullName);
                        fileName = Path.GetFileNameWithoutExtension(file.FullName);
                        StuHW.Add(this.HWrepository.Get(x => x.StuCouHWDe_ID == fileName));
                        StuHWDe.Add(this.StuCouHWDerepository.Get(x => x.StuCouHWDe_ID == fileName));
                    }
                }
                Parallel.ForEach(fileFullname,(onefile, loopState)=> {
                    FinalResult.Add(StartCorrect(HWFilePath, onefile, answer));
                });
                foreach (var result in FinalResult)
                {
                    HW_Exam StuHWNow = StuHW.Find(x => x.StuCouHWDe_ID == result.resultID);
                    StuCouHWDe_prog StuHWDeNow = StuHWDe.Find(x => x.StuCouHWDe_ID == result.resultID);
                    StuHWNow.HW_Exam_grade = result.score();
                    StuHWDeNow.Pass_compilation = result.ComplationErrorMessage;
                    StuHWDeNow.Success_execution = result.ExecutionErrorMessage;
                    StuHWDeNow.Compare_situation = result.compareresult.TrimEnd(',');
                    this.HWrepository.Update(StuHWNow);
                    this.StuCouHWDerepository.Update(StuHWDeNow);
                }

            }

        }
        public CorrectTaskReturn StartCorrect(HWPathHelper HWFilePath,string assignmentPath, Program_Answer answer)
        {
            
            AnswerPathHelper CorrectAnswer = new AnswerPathHelper(answer.cAnswer_Input);
            string filename;
            CorrectTaskReturn FinalResult;
            CorrectTaskResult compilationResult;
            CorrectTaskResult executionResult;
                    
                filename = Path.GetFileNameWithoutExtension(assignmentPath);
                FinalResult = new CorrectTaskReturn();
                compilationResult = new CorrectTaskResult();
                executionResult = new CorrectTaskResult();
                compilationResult = CorrectTaskHelper.compileprogram(HWFilePath, assignmentPath);
                FinalResult.resultID = filename;

            if (compilationResult.Success)
            {
                FinalResult.IsCompilationSuccess = true;
                DirectoryInfo direct = new DirectoryInfo(CorrectAnswer.AnswerInputFilePath);
                int count = 0;
                foreach (var file in direct.GetFiles(answer.cQID + "_*.txt"))
                {
                    count++;
                    string testnum = file.Name.Replace(".txt","").Substring(file.Name.Replace(".txt", "").IndexOf("_"));
                    executionResult = CorrectTaskHelper.executeprogram(HWFilePath, filename, CorrectAnswer.AnswerInputFilePath+@"\"+file.Name);
                    if (executionResult.Success)
                    {
                        CorrectTaskHelper.adjustoutput(HWFilePath, filename+testnum);
                        CorrectTaskHelper.comparefile(HWFilePath, filename+testnum, CorrectAnswer.AnswerFilePath+@"\"+file.Name);
                        FinalResult.IsExecutionSuccess.Add(true);
                        FinalResult.IsComparsionSuccess.Add(CorrectTaskHelper.PassOrNot(HWFilePath, filename+testnum));
                        FinalResult.addcomparsion(CorrectTaskHelper.PassOrNot(HWFilePath, filename+testnum));
                    }
                    else
                    {
                        FinalResult.ExecutionErrorMessage +="測試"+testnum+@"執行錯誤:"+executionResult.errorMessage+"\r\n";
                    }
                }
                FinalResult.testnum = count;
            }
            else
            {
                FinalResult.ComplationErrorMessage = compilationResult.errorMessage;
            }
            
            return FinalResult;

        }
    }
}