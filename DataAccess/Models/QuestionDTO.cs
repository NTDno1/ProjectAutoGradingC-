namespace DataAccess.Models
{
    public class QuestionDTO
    {
        public int StudentId { get; set; }
        public string QuestionId { get; set; } = null!;
        public double? TotalMark { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
