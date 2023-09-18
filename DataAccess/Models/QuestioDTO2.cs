namespace DataAccess.Models
{
    public class QuestioDTO2
    {
        public int StudentId { get; set; }
        public string QuestionId { get; set; } = null!;
        public double? TotalMark { get; set; }
        public string studentCode { get; set; } = null!;
        public string studentName { get; set; } = null!;
        public int? classId { get; set; }
        public string classs { get; set; } = null!;
    }
}
