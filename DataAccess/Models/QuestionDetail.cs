using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class QuestionDetail
    {
        public QuestionDetail()
        {
            QuestionNos = new HashSet<QuestionNo>();
            Questions = new HashSet<Question>();
        }

        public string QuestionId { get; set; } = null!;
        public int? Q { get; set; }
        public int? Status { get; set; }
        public int? Category { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<QuestionNo> QuestionNos { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
