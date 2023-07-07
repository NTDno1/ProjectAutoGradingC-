namespace DataAccess.Models
{
    public class TestCase
    {
        public string Questions { get; set; }
        public string Mark { get; set; }
        public string InPut { get; set; }
        public string Output { get; set; }

        public TestCase(string question, string mark, string input, string output)
        {
            Questions = question;
            Mark = mark;
            InPut = input;
            Output = output;
        }
    }
}
