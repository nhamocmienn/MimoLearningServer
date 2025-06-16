using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Subject
    {
        public int Id { get; set; }
        public int? ClassId { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Class? Class { get; set; }
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }

}
