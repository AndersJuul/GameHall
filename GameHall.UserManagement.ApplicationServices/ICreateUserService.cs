using System;
using System.Threading.Tasks;

namespace GameHall.UserManagement.ApplicationServices
{
    public interface ICreateUserService
    {
        Task CreateUser(Guid userId, string userName);
    }
}