using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Mark
    {
        public Mark()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public double TotalMark { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual User Student { get; set; } = null!;
        public virtual ICollection<Question> Questions { get; set; }
    }
}
