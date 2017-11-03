using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace gradeprogram.Service
{
    public class QuestionObj
    {
        public bool Success
        {
            get;
            set;
        }
        public string errorMessage
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }

        public QuestionObj()
            : this(false)
        {
            cQID = Regex.Replace(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), @"[^0-9]", String.Empty);
        }

        public QuestionObj(bool success)
        {
            Success = success;
        }

        public string cQID { get; set; }
        public string CourseID { get; set; }
        public string question { get; set; }
        public string outputformat { get; set; }
        public string testinput { get; set; }
        public string answer { get; set; }
        
        public void setCQID()
        {
            cQID = this.CourseID + cQID;
        }
       

    }
}