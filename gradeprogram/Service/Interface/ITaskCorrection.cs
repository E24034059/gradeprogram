using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gradeprogram.Models;

namespace gradeprogram.Service.Interface
{
   public interface ITaskCorrection
    {
        IResult Create(StuCouHWDe_prog instance);

        IResult Update(StuCouHWDe_prog instance);

        IResult Delete(string StuCouHWDeID);

        bool IsExists(string StuProgramFN);

        StuCouHWDe_prog GetByID(string StuCouHWDeID);

        IEnumerable<StuCouHWDe_prog> GetAll();

        IEnumerable<Class> GetAllClass();

        string GetHWNum(int CourseID, string tagName, string selectedValue);
        string GetExerciseData(int CourseID, string tagName, string selectedValue);
        string GetTypes(int CourseID, string tagName, string selectedValue);

        string CorrectTask(int CourseID,string HWNum);
        string CorrectTask(string ProgramFilePath, string cQID, string StuProgramFN, string questionNum);
        bool IsAnswerExists(int Course_ID,string HWNum);
        void InitStuCouHWDe_prog_record(string StuCouHWDe_ID, string StuProgramFN);
    }
}
