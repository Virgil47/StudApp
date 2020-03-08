using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudApp.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int GenderId { get; set; }
        public Gender Gender { get; set; } 
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Invalid FirstName length")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Invalid LastName length")]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Invalid Patronymic length")]
        public string Patronymic { get; set; }
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Invalid Identifier length")]
        public string Identifier { get; set; }
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
        public Student()
        {
            StudentGroup = new List<StudentGroup>();
        }
    }
}
