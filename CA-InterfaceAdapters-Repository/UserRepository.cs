using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Models;
using CL_EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Repository
{
    public class UserRepository : IRepository<User>
    {

        private readonly AppDbContext _DbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(User dto)
        {
            var user = new UserModel
            {
                Id = dto.Id,
                Departament = dto.Departament,
                Name = dto.Name,
                Rol = dto.Rol
            };

            await _DbContext.AddAsync(user);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userModel = await _DbContext.Users.FindAsync(id);

            _DbContext.Users.Remove(userModel);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _DbContext.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    Name = u.Name,
                    Rol = u.Rol,
                    Departament = u.Departament
                })
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var userModel = await _DbContext.Users
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            // Si no existe, devolvemos null inmediatamente
            if (userModel == null) return null;

            return new User
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Rol = userModel.Rol,
                Departament = userModel.Departament
            };
        }

        public async Task UpdateAsync(User user)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Departament = user.Departament,
                Rol = user.Rol
            };

            _DbContext.Users.Update(userModel);

            await _DbContext.SaveChangesAsync();
        }
    }
}
