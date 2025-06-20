﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User Creator { get; set; } = null!;


        public ICollection<ClassMember> ClassMembers { get; set; }

        public virtual ICollection<User> Members { get; set; } = new List<User>(); // n-n
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
