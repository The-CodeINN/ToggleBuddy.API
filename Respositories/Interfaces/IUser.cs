using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface IUser
    {
        public Task<User?> GetUserByIdAsync(string id);
    }

}

