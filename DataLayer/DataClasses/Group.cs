using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudApp.Models
{
    public class Group
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Invalid Group Name length")]
        public string Name { get; set; }
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
        public Group()
        {
            StudentGroup = new List<StudentGroup>();
        }
    }
}
