using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using gradeprogram.Service;
using gradeprogram.Models;
using System.Threading.Tasks;
using static gradeprogram.App_Start.constants;
namespace gradeprogram.Service
{
    public class CreateQuestionHelper
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

        public CreateQuestionHelper()
            : this(false)
        {
            question = new Program_Question();
            answer = new Program_Answer();
        }

        public CreateQuestionHelper(bool success)
        {
            Success = success;
        }
        public Program_Question question { get; set; }
        public Program_Answer answer { get; set; }
        

        public  void creatOnequestion(QuestionObj Question)
        {
            
            question.cQID = Question.cQID;
            question.cQuestion = Question.question;
            answer.cQID = Question.cQID;
            try
            {
                List<Action> tasks = new List<Action>();
                AnswerPathHelper ansPath = new AnswerPathHelper(Question.CourseID, 0);
                Action outputTask = new Action(() => question.OutputExample = storeOutputExample(Question, ansPath));
                tasks.Add(outputTask);
                Action inputTask = new Action(() => storeTestinput(Question, ansPath));
                tasks.Add(inputTask);
                Action answerTask = new Action(() => answer.cAnswer_Input=storeAnswer(Question, ansPath));
                tasks.Add(answerTask);

                Parallel.Invoke(tasks.ToArray());



            }
            catch(Exception ex)
            {
            }
            finally
            {
                
            }
           
        }

        public  string storeOutputExample(QuestionObj oneQuestion ,AnswerPathHelper Apath)
        {
            string filepath = Apath.AnswerFormatFilePath + @"\" + oneQuestion.cQID + @".txt";
            StreamWriter example = new StreamWriter(filepath,false);
            example.Write(oneQuestion.outputformat);
            example.Close();
            return filepath;
        }
        public void storeTestinput(QuestionObj oneQuestion, AnswerPathHelper Apath)
        {
            string[] Inputs = oneQuestion.testinput.Split(new string[] { segmentsCut+"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count = 1;
            foreach (string input in Inputs)
            {
                string filepath = Apath.AnswerInputFilePath + @"\" + oneQuestion.cQID + @"_" + count + @".txt";
                StreamWriter testinput = new StreamWriter(filepath, false);
                testinput.Write(input);
                testinput.Close();
                count += 1;
            }
        }
        public string storeAnswer(QuestionObj oneQuestion, AnswerPathHelper Apath)
        {
            string[] Answers = oneQuestion.answer.Split(new string[] { segmentsCut+"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count = 1;
            foreach (string Answer in Answers)
            {
                string filepath = Apath.AnswerFilePath + @"\" + oneQuestion.cQID + @"_" + count + @".txt";
                StreamWriter testinput = new StreamWriter(filepath, false);
                testinput.Write(Answer);
                testinput.Close();
                count += 1;
            }
            answer.cTestingDataAmount =(count-1).ToString();
            return Apath.AnswerFilePath;
        }


    }
}