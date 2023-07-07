﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Class
    {
        public Class()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
