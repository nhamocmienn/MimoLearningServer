using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!; // student | teacher

        // Quan hệ 1-n
        public virtual ICollection<Class> CreatedClasses { get; set; } = new List<Class>();
        public virtual ICollection<Subject> CreatedSubjects { get; set; } = new List<Subject>();
        public virtual ICollection<Lesson> CreatedLessons { get; set; } = new List<Lesson>();

        // Quan hệ n-n
        public ICollection<ClassMember> ClassMembers { get; set; }

        public virtual ICollection<Class> JoinedClasses { get; set; } = new List<Class>();
    }

}
