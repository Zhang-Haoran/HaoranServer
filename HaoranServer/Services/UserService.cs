using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.UserDto;
using AutoMapper;

public class UserService
{
    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public UserService(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.user.Where(u => !u.IsDeleted).Include(r => r.Review).ToListAsync();
    }

    public async Task<User> GetUser(int userId)
    {
        return await _context.user.Where(u => !u.IsDeleted).Include(r => r.Review).FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task UpdateUser(int userId, UserPutDto userPutDto)
    {
        var user = await _context.user.FindAsync(userId);
        if (user != null)
        {
            _mapper.Map(userPutDto, user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> CreateUser(UserPostDto userPostDto)
    {
        User user = _mapper.Map<User>(userPostDto);
        _context.user.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task SoftDeleteUser(int userId)
    {
        var user = await _context.user.FindAsync(userId);
        if (user != null)
        {
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public bool UserExists(int userId)
    {
        return _context.user?.Any(e => e.UserId == userId) ?? false;
    }
}
