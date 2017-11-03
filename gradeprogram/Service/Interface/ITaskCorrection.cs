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

        bool IsExists(string StuCouHWDeID);

        StuCouHWDe_prog GetByID(string StuCouHWDeID);

        IEnumerable<StuCouHWDe_prog> GetAll();

        IEnumerable<Class> GetAllClass();

        string GetHWNum(string CourseID, string tagName, string selectedValue);
        string GetExerciseData(string CourseID, string tagName, string selectedValue);
        string GetTypes(string CourseID, string tagName, string selectedValue);

        void CorrectTask(string CourseID,string assignmentType,string HWNum);
        bool IsAnswerExists(string Course_ID,string HWNum);
    }
}
