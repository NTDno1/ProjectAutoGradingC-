using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class QuestionNo
    {
        public int Id { get; set; }
        public string QuestionId { get; set; } = null!;
        public int StudentId { get; set; }
        public string? QuestionStt { get; set; }
        public string? Mark { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public string? InputTestCase { get; set; }
        public string? OutputTestCase { get; set; }
        public string? Output { get; set; }

        public virtual QuestionDetail Question { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
