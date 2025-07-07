using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.authentication.Domain.Model.Aggregates;
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
        var entity = await _context.Set<User>().FindAsync(id);
        return entity;
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        var entity = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == email);
            
        return entity == null ? null : MapToAuthUser(entity);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await FindByEmailAsync(email);
    }

    public async Task<User> FindByEmailExceptIdAsync(int id, string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        var entity = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == email && u.Id != id);
            
        return entity == null ? null : MapToAuthUser(entity);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var entities = await _context.Set<User>().ToListAsync();
        return entities.Select(MapToAuthUser);
    }

    public async Task<IEnumerable<User>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<User>();

        var entities = await _context.Set<User>()
            .Where(u => u.Username.Contains(name))
            .ToListAsync();

        return entities.Select(MapToAuthUser);
    }

    public async Task AddAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var iamUser = new User
        {
            Username = user.Username,
            Password = user.Password, // Note: This should be hashed before calling this method
            Email = user.Email
        };

        await _context.Set<User>().AddAsync(iamUser);
        await _context.SaveChangesAsync();

        // Update the domain object with the generated ID
        user.Id = iamUser.Id;
    }

    public async Task UpdateAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var entity = await _context.Set<User>().FindAsync(user.Id);
        if (entity == null)
            throw new KeyNotFoundException($"User with ID {user.Id} not found");

        // Update the properties that are allowed to be updated
        entity.Update(user.Username, user.Email, user.Password);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _context.Set<User>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        var entity = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username);
            
        return entity == null ? null : MapToAuthUser(entity);
    }

    private static User MapToAuthUser(User iamUser)
    {
        if (iamUser == null) return null;

        return new User
        {
            Id = iamUser.Id,
            Username = iamUser.Username,
            Email = iamUser.Email,
            Password = iamUser.Password
        };
    }
}