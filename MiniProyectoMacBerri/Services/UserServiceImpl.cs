using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly MacberriprojectContext _context;
        public UserServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task AddUser(UserDto userDto)
        {
            var user = new User
            {
                Names = userDto.Names,
                Lastnames = userDto.Lastnames,
                Email = userDto.Email,
                Password = userDto.Password,
                IdRol = userDto.Id_rol
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePassword(string password, string id_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser.ToString().Equals(id_user));
            if (user != null)
            {
                user.Password = password;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(string id_user)
        {
            var user = await _context.Users.FirstAsync(u => u.IdUser.ToString().Equals(id_user));
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> getAll()
        {
            var userList = await _context.Users.Include(r => r.IdRolNavigation).ToListAsync();
            return userList;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            return user;
        }

        public async Task<User> GetById(string id_user)
        {
            var user = await _context.Users.FirstAsync(u => u.IdUser.ToString().Equals(id_user));
            return user;
        }

        public async Task<User> login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(r => r.IdRolNavigation).FirstOrDefaultAsync(u => u.Email.Equals(loginDto.Email));
            if (user.Password.Equals(loginDto.Password))
            {
                return user;
            }
            return null;
        }

        public async Task UpdateUser(UserDto userDto, string id_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser.ToString().Equals(id_user));
            if (user != null)
            {
                user.Names = userDto.Names;
                user.Lastnames = userDto.Lastnames;
                user.Email = userDto.Email;
                user.Password = userDto.Password;
                user.IdRol = userDto.Id_rol;
            }
            await _context.SaveChangesAsync();
        }
    }
}
