using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gradeprogram.Models;
using gradeprogram.ViewModels;

namespace gradeprogram.ViewModels
{
    public class CreateQuestionViewModel
    {
       public AnswerPropertyModel PropertyParameter { get; set; } 
       public SelectList ClassData{ get; set;}

       public SelectList HW_ExamData { get; set; }  

       public string sparatestring{ get; set; }
       
    }
}