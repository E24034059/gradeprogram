using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace gradeprogram.App_Start
{
    public static class constants
    {
        public static readonly string answerPath = HttpContext.Current.Server.MapPath(@"~\Answer");
        public static readonly string exercisePath = HttpContext.Current.Server.MapPath(@"~\HWfile");
        public const string segmentsCut = @"aeiouSeparateAEIOU";
        public const int WaitExecutionTime = 2000;
    }
}