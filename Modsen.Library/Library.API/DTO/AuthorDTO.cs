namespace Library.DTO
{
    public class AuthorDto
    {
        public Guid AuthorId { get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
