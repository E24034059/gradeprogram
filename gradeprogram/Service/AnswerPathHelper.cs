using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static gradeprogram.App_Start.constants;
using System.IO;

namespace gradeprogram.Service
{
    public class AnswerPathHelper
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
        public string AnswerFilePath
        {
            get;
            set;
        }
        public string AnswerInputFilePath
        {
            get;
            set;
        }

        public string AnswerExampleFilePath
        {
            get;
            set;
        }
        public string AnswerFormatFilePath
        {
            get;
            set;
        }
        public AnswerPathHelper()
            : this("")
        {
        }

        public AnswerPathHelper(string Answerpath)
        {
            if (Answerpath != "")
            {
                Success = true;
                AnswerFilePath = Answerpath;
                AnswerInputFilePath = Answerpath.Replace("correctAnswer", "testinput");
                AnswerExampleFilePath = Answerpath.Replace("correctAnswer","correctProgram");
                AnswerFormatFilePath = Answerpath.Replace("correctAnswer","outputFormat");
            }
            else
            {
                Success = false;
            }
        }
        public AnswerPathHelper(string courseName,int x )
        {
            if (courseName != "")
            {
                Success = true;
                AnswerFilePath = answerPath+@"\" +courseName+@"\"+ "correctAnswer";
                AnswerInputFilePath = AnswerFilePath.Replace("correctAnswer", "testinput");
                AnswerExampleFilePath = AnswerFilePath.Replace("correctAnswer", "correctProgram");
                AnswerFormatFilePath = AnswerFilePath.Replace("correctAnswer", "outputFormat");
            }
            else
            {
                Success = false;
            }
        }
    }
}