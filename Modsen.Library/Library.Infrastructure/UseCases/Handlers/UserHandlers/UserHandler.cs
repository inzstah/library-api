using Library.Domain.Data;
using Library.Persistence.Database;
using Library.Persistence.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.UseCases.Handlers.UserHandlers;

public class UserHandler(
    UnitOfWork unitOfWork,
    TokenProvider tokenProvider,
    LoginUser loginUser)
{
    private readonly TokenProvider tokenProvider = tokenProvider;

    
    public List<User> GetAllUsers()
    {
        return unitOfWork.users.GetUsers().ToList();
    }
    
    public User GetUserById(Guid id)
    {
        return unitOfWork.users.GetUserById(id);
    }
    
    public async Task<string?> IssueBook(Guid bookId, Guid userId)
    {
        try
        {
            unitOfWork.users.IssueBook(bookId, userId);
            await unitOfWork.SaveAsync();
            return "Added";
        }
        catch (Exception e)
        {
            return e.InnerException?.Message;
        }
    }
    
    public async Task<string> ToggleAdmin(Guid userId)
    {
        unitOfWork.users.ToggleAdmin(userId);
        await unitOfWork.SaveAsync();
        return "Promoted user to admin";
    }
    
    public async Task<string> UploadImage(string root, IFormFile file)
    {
        string fileName;
        if (file is { Length: > 0 })
        {
            fileName = file.FileName;
            var physPath = root + "/UserPhotos/" + file.FileName;
            await using var stream = new FileStream(physPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
        else
        {
            fileName = "default.png";
        }

        return fileName;
    }
    
    public Task<string?> Login(string email, string password)
    {
        return Task.FromResult(loginUser.Handle(email, password));;
    }
    
    public async Task UpdateUserPfp(Guid id, string fileName)
    {
        unitOfWork.users.UpdateUserPfp(id, fileName);
        await unitOfWork.SaveAsync();
    }
    
    public async Task ReturnBook(Guid bookId, Guid userId)
    {
        unitOfWork.users.ReturnBook(bookId, userId);
        await unitOfWork.SaveAsync();
    }
}