using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class QuestionDetail
    {
        public int QuestionId { get; set; }
        public int? Q { get; set; }
        public int? Status { get; set; }
        public int? Category { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
