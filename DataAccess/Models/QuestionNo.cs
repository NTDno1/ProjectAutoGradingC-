using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class QuestionNo
    {
        public int QuestionId { get; set; }
        public int? QuestionNo1 { get; set; }
        public string? Mark { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }

        public virtual QuestionDetail Question { get; set; } = null!;
        public virtual TestCaseDb? QuestionNo1Navigation { get; set; }
    }
}
