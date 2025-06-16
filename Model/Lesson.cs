using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Lesson
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<StudySection> StudySections { get; set; } = new List<StudySection>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }

}
