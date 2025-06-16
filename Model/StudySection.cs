using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudySection
    {
        public int Id { get; set; }
        public int? LessonId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateDate { get; set; }

        public virtual Lesson? Lesson { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
