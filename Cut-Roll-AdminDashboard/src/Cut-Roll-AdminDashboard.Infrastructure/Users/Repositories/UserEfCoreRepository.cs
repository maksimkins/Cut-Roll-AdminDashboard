using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;
using Cut_Roll_AdminDashboard.Core.Users.Repositories;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Cut_Roll_AdminDashboard.Infrastructure.Users.Repositories;

public class UserEfCoreRepository : IUserRepository
{
    private readonly CutRollAdminDashboardDbContext _context;
    public UserEfCoreRepository(CutRollAdminDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<string?> AssignRoleAsync(UserRoleIdDto dto)
    {
        var foundUser = await _context.Users.FindAsync(dto.UserId);
        if (foundUser == null) return null;
        foundUser.RoleId = dto.RoleId!;

        var res = await _context.SaveChangesAsync();
        return res > 0 ? foundUser.Id : null;
    }

    public async Task<int> CountUsersByRoleAsync(string roleId)
    {
        return await _context.Users.CountAsync(u => u.RoleId == roleId);
    }

    public Task<string?> CreateAsync(UserCreateDto entity)
    {
        _context.Users.Add(new User
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            AvatarPath = entity.AvatarPath,
            RoleId = entity.RoleId
        });

        var res = _context.SaveChangesAsync();
        return res.ContinueWith(t => t.Result > 0 ? entity.Id : null);
    }

    public async Task<string?> DeleteByIdAsync(string id)
    {
        var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        if (user == null) return null;
        _context.Users.Remove(user);
        var res = await _context.SaveChangesAsync();

        return res > 0 ? user.Id : null;
    }

    public async Task<UserResponseDto?> GetByIdAsync(string id)
    {
        var user = await _context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefaultAsync();
        if (user == null) return null;
        
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            AvatarPath = user.AvatarPath,
            Role = user.Role.Name,
            IsBanned = user.IsBanned,
            IsMuted = user.IsMuted,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.Where(u => u.Email == email).Include(u => u.Role).FirstOrDefaultAsync();
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            AvatarPath = user.AvatarPath,
            Role = user.Role.Name,
            IsBanned = user.IsBanned,
            IsMuted = user.IsMuted,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<UserResponseDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users.Where(u => u.UserName == username).Include(u => u.Role).FirstOrDefaultAsync();
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            AvatarPath = user.AvatarPath,
            Role = user.Role.Name,
            IsBanned = user.IsBanned,
            IsMuted = user.IsMuted,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<IQueryable<User>> GetUsersAsQueryableAsync()
    {
        return await Task.FromResult(_context.Users.AsNoTracking().Include(u => u.Role).AsQueryable());
    }

    public async Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto dto)
    {
        var query = _context.Users.AsQueryable();

    
        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
        {
            var term = $"%{dto.SearchTerm}%"; 
            query = query.Where(u =>
                EF.Functions.ILike(u.UserName, term) ||
                EF.Functions.ILike(u.Email, term));
        }

        
        if (dto.Role.HasValue)
        {
            query = query.Where(u => u.Role.Name == dto.Role.ToString());
        }

        
        if (dto.IsBanned.HasValue)
        {
            query = query.Where(u => u.IsBanned == dto.IsBanned.Value);
        }

        if (dto.IsMuted.HasValue)
        {
            query = query.Where(u => u.IsMuted == dto.IsMuted.Value);
        }

        if (dto.RegisteredAfter.HasValue)
        {
            query = query.Where(u => u.CreatedAt >= dto.RegisteredAfter.Value);
        }

        if (dto.RegisteredBefore.HasValue)
        {
            query = query.Where(u => u.CreatedAt <= dto.RegisteredBefore.Value);
        }

        
        var totalCount = await query.CountAsync();

        
        var pageNumber = dto.PageNumber <= 0 ? 1 : dto.PageNumber;
        var pageSize = dto.PageSize <= 0 ? 10 : dto.PageSize;

        var users = await query
            .OrderBy(u => u.UserName) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Role = u.Role.Name,
                IsBanned = u.IsBanned,
                IsMuted = u.IsMuted,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        
        return new PagedResult<UserResponseDto>
        {
            Data = users,
            TotalCount = totalCount,
            Page = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<string?> ToggleBanAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        user.IsBanned = !user.IsBanned;
        var res = await _context.SaveChangesAsync();
        return res > 0 ? user.Id : null;
    }

    public async Task<string?> ToggleMuteAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        user.IsMuted = !user.IsMuted;
        var res = await _context.SaveChangesAsync();
        return res > 0 ? user.Id : null;
    }

    public async Task<string?> UpdateAsync(UserUpdateDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
        if (user == null) return null;

        user.UserName = dto.UserName ?? user.UserName;
        user.RoleId = dto.RoleId ?? user.RoleId;
        user.Email = dto.Email ?? user.Email;

        var res = await _context.SaveChangesAsync();
        return res > 0 ? user.Id : null;
    }

    public async Task<string?> UpdateAvatarAsync(UserUpdateAvatarDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
        if (user == null) return null;
        user.AvatarPath = dto.AvatarPath;
        var res = await _context.SaveChangesAsync();
        return res > 0 ? user.Id : null;
    }

    public async Task<bool> UserExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.UserName == email);
    }

    public async Task<bool> UserExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == username);
    }
}
