using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gradeprogram.Service
{
    public class CorrectTaskReturn
    {
        public bool IsCompilationSuccess
            {
                get;
                set;
            }

            public List<bool> IsExecutionSuccess
            {
               get;
               set;
            }

            public List<bool> IsComparsionSuccess
            {
                get;
                set;
            }
            public string ComplationErrorMessage
            {
                get;
                set;
            }
            public string ExecutionErrorMessage
            {
                get;
                set;
            }
            public Exception Exception
            {
                get;
                set;
            }
            public string compareresult
            {
            get;
            set;
            }
            public string resultID { get; set; }
            public int testnum { get; set; }
            public CorrectTaskReturn()
                : this(false)
            {
            }

            public CorrectTaskReturn(bool success)
            {
            IsCompilationSuccess = success;
            IsExecutionSuccess = new List<bool>();
            IsComparsionSuccess = new List<bool>();
            testnum = 0;
            compareresult = "NotYetCompare";
            }

        public void addcomparsion(bool s)
        {
            if (this.compareresult != "NotYetCompare")
            {
                if (s == true)
                {
                    this.compareresult += "AC,";
                }
                else
                {
                    this.compareresult += "Fail,";
                }
            }
            else
            {
                if (s == true)
                {
                    this.compareresult ="AC,";
                }
                else
                {
                    this.compareresult = "Fail,";
                }
            }
        }

        public string score()
        {
            int stages = 1;
            int[] scores = {0,3,0,0,0};
            string HW_Grade="";
            string temp="";
            
            if (this.IsCompilationSuccess == true)
            {
                scores[0] += 30;
                scores[2]  = 30;
                foreach (bool i in this.IsExecutionSuccess)
                {
                        scores[3] += 30;                      
                }
                if (scores[3] != 0) { scores[3]=scores[3] / testnum; }
           
                    foreach (bool s in this.IsComparsionSuccess)
                {
                    if (s == true)
                    {
                        scores[4] += 40;
                    }
                }
                if (scores[4] != 0) { scores[4] = scores[4] / testnum; }
                scores[0] += scores[3];
                scores[0] += scores[4];
            }
            foreach (var i in scores)
            {

                temp += i.ToString() + @",";
            }

            HW_Grade = @"1," +temp.TrimEnd(',') + @":";
            return HW_Grade;
        }
    }
}