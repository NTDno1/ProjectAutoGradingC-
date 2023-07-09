using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Question
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public double? TotalMark { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual QuestionDetail QuestionNavigation { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
