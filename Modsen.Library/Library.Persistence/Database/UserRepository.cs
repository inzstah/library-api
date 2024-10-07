using Library.Domain.Data;
namespace Library.Persistence.Database
{
    public class UserRepository(LibraryDbContext context)
    {
        private readonly LibraryDbContext context = context;
       
        public IQueryable<User> GetUsers()
        {
            return context.users;
        }
        
        public void IssueBook(Guid _BookId, Guid _UserId)
        {
            UserBook userBook = new UserBook(_BookId, _UserId);
            if (context.userBooks.All(ub => ub != userBook))
            {
                context.userBooks.Add(userBook);
            }
            else
            {
                throw new Exception("User already has this book");
            }
        }
       
        public void AddUser(User user)
        {
            if (context.users.All(u => u.Email != user.Email))
            {
                context.users.Add(user);
            }
            else throw new Exception("User exists");
        }
        public User GetUserByEmail(string _Email)
        {
            return context.users.Single(x => x.Email == _Email);
        }
        public bool UserEmailIsFree(string _Email)
        {
            return context.users.All(x => x.Email != _Email);
        }
        
        public User GetUserById(Guid _id)
        {
            return context.users.Single(x => x.UserId == _id);
        }
        
        public void ReturnBook(Guid _BookId, Guid _UserId)
        {
            UserBook userBook = new UserBook(_BookId, _UserId);
            if (context.userBooks.Any(ub => ub == userBook))
            {
                context.userBooks.Remove(userBook);
            }
            else
            {
                throw new Exception("User doesn't have this book");
            }
        }
        public void ToggleAdmin(Guid UserId)
        {
            User user = GetUserById(UserId);
            user.IsAdmin = !user.IsAdmin;
            context.users.Entry(GetUserById(user.UserId)).CurrentValues.SetValues(user);
        }
        public void UpdateUserPfp(Guid id, string fileName)
        {
            User user = GetUserById(id);
            user.ProfilePictureName = fileName;
            context.users.Entry(GetUserById(user.UserId)).CurrentValues.SetValues(user);
        }
    }
}
