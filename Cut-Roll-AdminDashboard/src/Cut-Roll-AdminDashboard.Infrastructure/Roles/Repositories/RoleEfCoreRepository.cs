namespace Cut_Roll_AdminDashboard.Infrastructure.Roles.Repositories;

using Cut_Roll_AdminDashboard.Core.Common.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Dtos;
using Cut_Roll_AdminDashboard.Core.Roles.Enums;
using Cut_Roll_AdminDashboard.Core.Roles.Models;
using Cut_Roll_AdminDashboard.Core.Roles.Repositories;
using Cut_Roll_AdminDashboard.Core.Users.Dtos;
using Cut_Roll_AdminDashboard.Core.Users.Models;
using Cut_Roll_AdminDashboard.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;

public class RoleEfCoreRepository : IRoleRepository
{
    private readonly CutRollAdminDashboardDbContext _context;

    public RoleEfCoreRepository(CutRollAdminDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountUsersInRoleAsync(string roleId)
    {
        return await _context.Users.CountAsync(u => u.RoleId == roleId);
    }

    public async Task<string?> CreateAsync(RoleCreateDto entity)
    {
        var role = new Role
        {
            Id = entity.Id,
            Name = entity.Name
        };

        _context.Roles.Add(role);
        var res = await _context.SaveChangesAsync();
        return res > 0 ? role.Id : null;
    }

    public async Task<string?> DeleteByIdAsync(string id)
    {
        var role = await _context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

        if (role != null)
            _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return role?.Id;
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
    {
        return await _context.Roles
            .Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .ToListAsync();
    }

    public async Task<RoleResponseDto?> GetByIdAsync(string id)
    {
        return await _context.Roles
            .Where(r => r.Id == id)
            .Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<RoleResponseDto?> GetByNameAsync(UserRoles role)
    {
        return await _context.Roles
            .Where(r => r.Name == role.ToString())
            .Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PagedResult<UserResponseDto>> GetUsersInRoleAsync(UserGetByRoleIdDto dto)
    {
        var query = _context.Users.Include(u => u.Role).AsQueryable();

        var roleExists = await _context.Roles.AnyAsync(r => r.Id == dto.RoleId);

        if (roleExists == false)
            throw new ArgumentException($"Role with ID '{dto.RoleId}' does not exist.");

        query = query.Where(u => u.RoleId == dto.RoleId);
        var totalCount = query.Count();
        var users = query.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

        var data = await users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Username = u.UserName,
            Email = u.Email,
            Role = u.Role.Name
        }).ToListAsync();

        return new PagedResult<UserResponseDto>
        {
            Data = data,
            TotalCount = totalCount,
            Page = dto.PageNumber,
            PageSize = dto.PageSize
        };
    }

    public async Task<bool> RoleExistsAsync(UserRoles role)
    {
        return await _context.Roles.AnyAsync(r => r.Name == role.ToString());
    }

    public async Task<int> SetupRolesAsync()
    {
        List<UserRoles> roleNames = [UserRoles.Admin, UserRoles.User, UserRoles.Publisher];
        List<string> ids = ["57082502-2ccf-4610-b865-fdd780b8bf1d", "6424977e-131b-4f9f-aa3f-9626dd293021", "c0f3b8d1-2e4a-4f5c-9b6d-7c8e1f3a5b2c"];



        for (int i = 0; i < roleNames.Count; i++)
        {
            var roleExists = await this.RoleExistsAsync(roleNames[i]);

            if (!roleExists)
            {
                var role = new RoleCreateDto()
                {
                    Id = ids[i],
                    Name = roleNames[i].ToString()
                };
                var result = await this.CreateAsync(role);

                if (result == null)
                    throw new InvalidOperationException("cannot create roles!!");

            }
        }

        return 3;
    }
}
