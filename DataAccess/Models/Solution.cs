namespace DataAccess.Models
{
    public class Solution
    {
        public string StuName { get; set; }
        public string Question { get; set; }
        public string InputTestCase { get; set; }
        public string OutputTestCase { get; set; }
        public string Mark { get; set; }
        public string OutPut { get; set; }
        public float TotalMark { get; set; }

        public Solution(string stuName, string question, string inputTestCase, string outputTestCase, string mark, string outPut)
        {
            StuName = stuName;
            Question = question;
            InputTestCase = inputTestCase;
            OutputTestCase = outputTestCase;
            Mark = mark;
            OutPut = outPut;
        }

        public Solution(string stuName, string question, string outPut)
        {
            StuName = stuName;
            Question = question;
            OutPut = outPut;
        }

        public Solution(string stuName, string question)
        {
            StuName = stuName;
            Question = question;
        }

        public Solution(string stuName, float totalMark)
        {
            StuName = stuName;
            TotalMark = totalMark;
        }

        public override string? ToString()
        {
            return "   StudentName: " + StuName + "   QuestionNo: " + Question + "   InputTestCase: " + InputTestCase + "     : " + OutputTestCase + "   Mark: " + Mark + "   OutPut: " + OutPut;
        }
    }
}
