using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Q { get; set; }
        public double Mark { get; set; }
        public string Input { get; set; } = null!;
        public string Output { get; set; } = null!;
        public int Status { get; set; }
        public int Category { get; set; }
        public string Note { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Mark QuestionNavigation { get; set; } = null!;
    }
}
