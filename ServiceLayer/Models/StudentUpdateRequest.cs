using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.Models
{
    public class StudentUpdateRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public int GenderId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Invalid FirstName length")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Invalid LastName length")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Identifier { get; set; }
    }
}
