namespace ServiceLayer.Models
{
    public class StudentRequest
    {
        public string FiltredBy { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string FiltredValue { get; set; }
    }
}
