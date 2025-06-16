using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Question
    {
        public int Id { get; set; }
        public int StudySectionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;

        public virtual StudySection StudySection { get; set; } = null!;
        public virtual ICollection<WrongAnswer> WrongAnswers { get; set; } = new List<WrongAnswer>();
    }

}
