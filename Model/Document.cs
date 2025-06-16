using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Document
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!; // word | ppt | link
        public string Url { get; set; } = null!;

        public virtual Lesson Lesson { get; set; } = null!;
    }

}
