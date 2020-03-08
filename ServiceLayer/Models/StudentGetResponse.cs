namespace ServiceLayer.Models
{
    public class StudentGetResponse
    {
        public int Id { get; set; }
        public string GenderName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Identifier { get; set; }
        public string Groups { get; set; }
    }
}
