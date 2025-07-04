using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.authentication.Infrastucture.Repositories;

 public class UserRepository : IUserRepository
 {
     private readonly AppDbContext _context;

     public UserRepository(AppDbContext context)
     {
         _context = context ?? throw new ArgumentNullException(nameof(context));
     }

     public async Task<User> FindByIdAsync(int id)
     {
         var entity = await _context.Users.FindAsync(id);
         return entity == null ? null : MapToDomain(entity);
     }

     public async Task<User> FindByEmailAsync(string email)
     {
         if (string.IsNullOrWhiteSpace(email))
             throw new ArgumentException("Email cannot be empty", nameof(email));

         var entity = await _context.Users
             .FirstOrDefaultAsync(u => u.Email == email);
             
         return entity == null ? null : MapToDomain(entity);
     }

     public async Task<User> GetUserByEmail(string email)
     {
         return await FindByEmailAsync(email);
     }

     public async Task<User> FindByEmailExceptIdAsync(int id, string email)
     {
         if (string.IsNullOrWhiteSpace(email))
             throw new ArgumentException("Email cannot be empty", nameof(email));

         var entity = await _context.Users
             .FirstOrDefaultAsync(u => u.Email == email && u.Id != id);
             
         return entity == null ? null : MapToDomain(entity);
     }

     public async Task<IEnumerable<User>> GetAllAsync()
     {
         var entities = await _context.Users.ToListAsync();
         return entities.Select(MapToDomain);
     }

     public async Task<IEnumerable<User>> SearchByNameAsync(string name)
     {
         if (string.IsNullOrWhiteSpace(name))
             return new List<User>();

         var entities = await _context.Users
             .Where(u => u.Username.Contains(name))
             .ToListAsync();

         return entities.Select(MapToDomain);
     }

     public async Task AddAsync(User user)
     {
         if (user == null) throw new ArgumentNullException(nameof(user));

        var entity = new User
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            Rol = user.Rol
        };

         _context.Users.Add(entity);
         await _context.SaveChangesAsync();

         // Update the domain object with the generated ID
         user.Id = entity.Id;
     }

     public async Task UpdateAsync(User user)
     {
         if (user == null) throw new ArgumentNullException(nameof(user));

         var entity = await _context.Users.FindAsync(user.Id);
         if (entity == null)
             throw new KeyNotFoundException($"User with ID {user.Id} not found");

         entity.Username = user.Username;
         entity.Email = user.Email;
         entity.Password = user.Password;
         entity.Rol = user.Rol;
         await _context.SaveChangesAsync();
     }

     public async Task DeleteByIdAsync(int id)
     {
         var entity = await _context.Users.FindAsync(id);
         if (entity != null)
         {
             _context.Users.Remove(entity);
             await _context.SaveChangesAsync();
         }
     }

     public async Task<User> GetUserByUsername(string username)
     {
         if (string.IsNullOrWhiteSpace(username))
             throw new ArgumentException("Username cannot be empty", nameof(username));

         var entity = await _context.Users
             .FirstOrDefaultAsync(u => u.Username == username);
             
         return entity == null ? null : MapToDomain(entity);
     }

     private static User MapToDomain(User entity)
     {
         if (entity == null) return null;

        return new User
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            Password = entity.Password,
             Rol = entity.Rol
        };
     }
 }