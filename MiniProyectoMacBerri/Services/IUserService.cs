using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;
using System.Runtime.CompilerServices;

namespace MiniProyectoMacBerri.Services
{
    public interface IUserService
    {
        public Task<User> login(LoginDto loginDto);
        public Task<List<User>> getAll();
        public Task<User> GetById(string id_user);
        public Task AddUser(UserDto userDto);
        public Task UpdateUser(UserDto userDto, string id_user);
        public Task DeleteById(string id_user);
        public Task<User?> GetByEmail(string email);
        public Task ChangePassword(string password, string id_user);
    }
}
