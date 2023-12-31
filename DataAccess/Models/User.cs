﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            QuestionNos = new HashSet<QuestionNo>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public int ClassId { get; set; }
        public string Name { get; set; } = null!;
        public string Mssv { get; set; } = null!;
        public int Role { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual ICollection<QuestionNo> QuestionNos { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
