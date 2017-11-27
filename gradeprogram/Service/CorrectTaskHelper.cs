using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using gradeProgram.dllAPI;
using static gradeprogram.App_Start.constants;

namespace gradeprogram.Service
{
    public class CorrectTaskHelper
    {

        //compilation
        public static CorrectTaskResult compileprogram(HWPathHelper filepath, string file) {
            string strOutput;
            string compilecommand;
            string exefilename = Path.GetFileNameWithoutExtension(file);
            string exefilePath = filepath.HWExeFilePath;
            string modifiedProgram = modifyProgram(file);
            //build a process
            Process cmd1 = new Process();
            cmd1.StartInfo.WorkingDirectory = Path.GetDirectoryName(file);
            cmd1.StartInfo.FileName = "cmd.exe";
            cmd1.StartInfo.UseShellExecute = false;    //是否使用操作系统shell啟動
            cmd1.StartInfo.RedirectStandardInput = true;//接受来自調用程序的输入信息
            cmd1.StartInfo.RedirectStandardOutput = true;//由調用程序獲取输出信息
            cmd1.StartInfo.RedirectStandardError = true;//重定向標準錯誤输出
            cmd1.StartInfo.CreateNoWindow = true;//不顯示程序窗口
            compilecommand = @"g++ -g " + modifiedProgram + @" -o "+exefilePath +@"\"+ exefilename + @" 2>"+filepath.HWCompErrorFilePath+@"\" + exefilename;
            try
            {
                cmd1.Start();//啟動程序
                cmd1.StandardInput.WriteLine(compilecommand);
                cmd1.StandardInput.WriteLine("exit");
                cmd1.WaitForExit();//等待編譯執行完退出進程
                cmd1.Close();

            }
            catch (Exception e)
            {
                strOutput = e.Message;
               
            }
            
            return IsCompilationSuccess(filepath.HWCompErrorFilePath, exefilename);
        }
        //execution
        public static CorrectTaskResult executeprogram(HWPathHelper filepath, string filename, string inputfile) {
            string errOutput = null;
            string inputcommand;
            string outputResult;
            string error;
            string inputfilepath = inputfile;
            string inputfileName = Path.GetFileNameWithoutExtension(inputfile);
            string testnum = inputfileName.Substring(inputfileName.IndexOf("-"));
            string outputfilepath = filepath.HWOutputFilePath+@"\" + filename+testnum + @".txt";
            string errorfilepath = filepath.HWExeErrorFilePath + @"\" + filename+testnum;

            //build a reader, and the encoding choose ASCII
            StreamReader inputText = new StreamReader(inputfilepath, System.Text.Encoding.ASCII);
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = filepath.HWExeFilePath;
            proc.StartInfo.FileName = filepath.HWExeFilePath + @"\" + filename+ @".exe";
            proc.StartInfo.UseShellExecute = false;    //是否使用操作系统shell啟動
            proc.StartInfo.RedirectStandardInput = true;//接受来自調用程序的输入信息
            proc.StartInfo.RedirectStandardOutput = true;//由調用程序獲取输出信息
            proc.StartInfo.RedirectStandardError = true;//重定向標準錯誤输出
            proc.StartInfo.CreateNoWindow = true;//不顯示程序窗口

            try
            {
                proc.Start();//啟動程序
                StreamWriter commandStreamWriter = proc.StandardInput;
                //proc.BeginOutputReadLine();
                do
                {
                    inputcommand = inputText.ReadLine();
                    if (!String.IsNullOrEmpty(inputcommand))
                    {
                        commandStreamWriter.WriteLine(inputcommand);
                    }
                }
                while (!String.IsNullOrEmpty(inputcommand));
                commandStreamWriter.Close();
                inputText.Close();
                proc.WaitForExit(WaitExecutionTime);//等待程序執行完退出進程
                if (proc.HasExited == false)
                {
                    if (proc.Responding)
                    {
                        proc.Kill();
                        errOutput += "Time Limit Exceed. \r\n";
                        error = proc.StandardError.ReadToEnd();
                        errOutput += error;
                    }
                    else
                    {
                        proc.Kill();
                        errOutput += " Runtime Error.\r\n";
                        error = proc.StandardError.ReadToEnd();
                        errOutput += error;
                    }
                }
                else
                {
                    error = proc.StandardError.ReadToEnd();
                    errOutput += error;
                    StreamReader reader = proc.StandardOutput;
                    outputResult = reader.ReadToEnd();
                    proc.Close();
                    StreamWriter outputfile = new StreamWriter(outputfilepath, false);
                    outputfile.Write(outputResult);
                    outputfile.Close();
                }
            }
            catch (Exception e)
            {
                errOutput = errOutput + e.Message;
                proc.Close();

            }
            finally
            {
                StreamWriter errorfile = new StreamWriter(errorfilepath, false);
                errorfile.Write(errOutput);
                errorfile.Close();
            }
            return IsExecutionSuccess(filepath, filename + testnum);
        }

        //comparison
        public static void comparefile(HWPathHelper HWfilepath, string filename, string solutionfile) {
            string outputfile;
            string comparecommand;
            string erroutput;
            outputfile = HWfilepath.HWOutputFilePath + @"\" + filename + @".txt";
            Process cmd1 = new Process();
            cmd1.StartInfo.WorkingDirectory = HWfilepath.HWOutputFilePath;
            cmd1.StartInfo.FileName = "cmd.exe";
            cmd1.StartInfo.UseShellExecute = false;    //是否使用操作系统shell啟動
            cmd1.StartInfo.RedirectStandardInput = true;//接受来自調用程序的输入信息
            cmd1.StartInfo.RedirectStandardOutput = true;//由調用程序獲取输出信息
            cmd1.StartInfo.RedirectStandardError = true;//重定向標準錯誤输出
            cmd1.StartInfo.CreateNoWindow = true;//不顯示程序窗口
            comparecommand = @"FC /W " + solutionfile + @" " + outputfile + @"> "+HWfilepath.HWresultFilePath+@"\" + filename;
            try
            {
                cmd1.Start();//啟動程序
                cmd1.StandardInput.WriteLine(comparecommand);
                cmd1.StandardInput.WriteLine("exit");
                cmd1.WaitForExit();//等待比較執行完退出進程
                cmd1.Close();
            }
            catch (Exception e)
            {
                erroutput = e.Message;
            }
        }

        //get the compilation result
        public static CorrectTaskResult IsCompilationSuccess(string CompErrfilepath, string filename) {
            CorrectTaskResult result = new CorrectTaskResult(false);
            string ComparisonErrorfile = CompErrfilepath+ @"\" + filename;
            StreamReader errorFile = new StreamReader(ComparisonErrorfile, System.Text.Encoding.Default);
            string errorString = null;
            errorString = errorFile.ReadToEnd();
            errorFile.Close();
            if (errorString == "")
            {
                result.Success = true;
                return result;
            }
            else
            {
                result.errorMessage = errorString;
                return result;
            }
        }


        //get the execution result
        public static CorrectTaskResult IsExecutionSuccess(HWPathHelper HWfilepath, string filename)
        {
            CorrectTaskResult result = new CorrectTaskResult(false);
            string ExecutionErrorfile = HWfilepath.HWExeErrorFilePath+@"\"+ filename;
            StreamReader errorFile = new StreamReader(ExecutionErrorfile, System.Text.Encoding.UTF8);
            string errorString = null;
            errorString = errorFile.ReadToEnd();
            errorFile.Close();
            if (errorString == "")
            {
                result.Success = true;
                return result;
            }
            else
            {
                result.errorMessage = errorString;
                return result;
            }
        }
        //replace system("pause") to exit(1) to avoid stuck
        public static string modifyProgram( string filename)
        {
            string Programfile;
            string modifiedProgram;
            string ProgramString;
            Programfile = filename;
            StreamReader programFile = new StreamReader(Programfile, System.Text.Encoding.Default);
            ProgramString = programFile.ReadToEnd();
            programFile.Close();
            modifiedProgram = ProgramString.Replace(@"system(""pause"")", "exit(1)");
            Programfile = filename.Replace(".cpp", @"afterMF.cpp");
            StreamWriter output = new StreamWriter(Programfile, false, System.Text.Encoding.Default);             
            output.Write(modifiedProgram);
            output.Close();
            return Programfile;
        }

        //adjust the output
        public static void adjustoutput(HWPathHelper HWfilepath, string filename)
        {
            string outputfile;
            string[] adjustedOutput;
            string outputString;
            outputfile = HWfilepath.HWOutputFilePath + @"\" + filename + @".txt";
            StreamReader outputFile = new StreamReader(outputfile, System.Text.Encoding.Default);
            outputString = outputFile.ReadToEnd();
            outputFile.Close();
            adjustedOutput = outputString.Split(':');
            StreamWriter output = new StreamWriter(outputfile, false, System.Text.Encoding.Default);
            foreach (string line in adjustedOutput)
            {
                output.WriteLine(line);
            }
            output.Close();
        }

        //determine comparison 
        public static bool PassOrNot(HWPathHelper HWfilepath, string filename)
            {
            string ComparisonErrorfile = HWfilepath.HWresultFilePath+@"\"+ filename;
            StreamReader errorFile = new StreamReader(ComparisonErrorfile, System.Text.Encoding.Default);
            string errorString = null;
            errorString = errorFile.ReadToEnd();
            errorFile.Close();
            var test = errorString.IndexOf("FC: 找不到相異處");
            if (test != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}