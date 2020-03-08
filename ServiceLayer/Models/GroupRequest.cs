using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Models
{
    public class GroupRequest
    {
        public string FiltredBy { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string FiltredValue { get; set; }
    }
}
