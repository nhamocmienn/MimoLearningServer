using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ClassMember
    {
        public int JoinedClassesId { get; set; }
        public int MembersId { get; set; }

        
        public Class JoinedClass { get; set; }
        public User Member { get; set; }
    }
}
