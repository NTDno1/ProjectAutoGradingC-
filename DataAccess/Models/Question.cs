using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Question
    {
        public int StudentId { get; set; }
        public string QuestionId { get; set; } = null!;
        public double? TotalMark { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual QuestionDetail QuestionNavigation { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
