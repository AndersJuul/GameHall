using System;
using System.Threading.Tasks;

namespace GameHall.UserManagement.ApplicationServices
{
    public interface IUserService
    {
        Task CreateUser(Guid userId, string userName);
    }
}