using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gradeprogram.Models;


namespace gradeprogram.Service.Interface
{
    public interface IQuestions
    {
         IResult Create(QuestionObj instance);

        IResult Update(Correct_Answer instance);

        IResult Delete(int CourseID,string HWNum);

        bool IsExists(int CourseID, string HWNum);

         Correct_Answer GetByID(int CourseID, string HWNum);

        IEnumerable<Correct_Answer> GetAll();

        IEnumerable<Class> GetAllClass();

        IEnumerable<Take_Course> GetAllHWNum();
        string GetHWNum(int CourseID, string tagName, string selectedValue);
        string GetClassName(int CourseID);


    }
}
