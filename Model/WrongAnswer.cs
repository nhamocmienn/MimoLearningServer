using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WrongAnswer
    {
        public int Id { get; set; }
        public int StudySectionID { get; set; }
        public int QuestionId { get; set; }
        public string WrongOption { get; set; } = null!;

        public virtual Question Question { get; set; } = null!;
    }

}
