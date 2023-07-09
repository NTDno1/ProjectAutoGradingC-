using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class TestCaseDb
    {
        public int QuestionNo { get; set; }
        public int Name { get; set; }
        public string? InputTesCase { get; set; }
        public string? OutputTestCase { get; set; }
        public string? Ouput { get; set; }
    }
}
