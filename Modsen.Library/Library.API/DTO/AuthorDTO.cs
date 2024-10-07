namespace Library.API.DTO
{
    public class AuthorDTO
    {
        public Guid AuthorId { get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
