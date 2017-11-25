using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace gradeProgram.dllAPI
{
    public class filecomparedllAPI
    {

        [DllImport("D:/programdll.dll" ,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Readfile(byte[] fileName,byte[] buf);
        [DllImport("D:/programdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Comparefile(string output, string solutionContact,byte[] buf );

    }
}