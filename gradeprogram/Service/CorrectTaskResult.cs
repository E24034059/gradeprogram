using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gradeprogram.Service
{
    public class CorrectTaskResult
    {
        public bool Success {
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

        public CorrectTaskResult()
            : this(false)
        {
        }

        public CorrectTaskResult(bool success)
        {
            Success = success;
        }

    }
}