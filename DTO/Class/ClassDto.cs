using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Class
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class CreateClassDto
    {
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
    }

    public class UpdateClassDto
    {
        public string Name { get; set; } = null!;
    }
}
