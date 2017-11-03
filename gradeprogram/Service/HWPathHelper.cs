using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace gradeprogram.Service
{
    public class HWPathHelper
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
        public string HWFilePath
        {
            get;
            set;
        }
        public string HWProgFilePath
        {
            get;
            set;
        }
        public string HWExeFilePath
        {
            get;
            set;
        }
        public string HWOutputFilePath
        {
            get;
            set;
        }
        public string HWCompErrorFilePath
        {
            get;
            set;
        }
        public string HWExeErrorFilePath
        {
            get;
            set;
        }
        public string HWresultFilePath
        {
            get;
            set;
        }
      
        public HWPathHelper()
            : this("")
        {
        }

        public HWPathHelper(string HWpath)
        {
            if (HWpath != "")
            {
                Success = true;
                HWFilePath = HWpath;
                HWProgFilePath = HWpath + @"\StuProgram";
                HWExeFilePath = HWpath + @"\ExeProfile";
                HWOutputFilePath = HWpath + @"\output";
                HWCompErrorFilePath = HWpath + @"\error\compilationError";
                HWExeErrorFilePath = HWpath + @"\error\executionError";
                HWresultFilePath = HWpath + @"\result";

            }
            else
            {
                Success = false;
            }
        }

        public void HWPathdir()
        {
            Directory.CreateDirectory(this.HWProgFilePath);
            Directory.CreateDirectory(this.HWExeFilePath);
            Directory.CreateDirectory(this.HWOutputFilePath);
            Directory.CreateDirectory(this.HWCompErrorFilePath);
            Directory.CreateDirectory(this.HWExeErrorFilePath);
            Directory.CreateDirectory(this.HWresultFilePath);
        }



    }
}