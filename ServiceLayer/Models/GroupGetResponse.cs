using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.Models
{
    public class GroupGetResponse
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Invalid Group Name length")]
        public string GroupName { get; set; }
        public int StudentsCount { get; set; }
    }
}
