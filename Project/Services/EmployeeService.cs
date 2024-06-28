using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Context;
using JWT.Helpers;
using JWT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.ResponceModels;

namespace Project.Services;

public interface IEmployeeService
{
    Task<EmployeeResponseModel> CreateUserAsync(RegisterModel registerModel);
    Task<TokenResponseModel> RefreshTokenAsync(RefreshTokenModel refreshTokenModel);
    Task<TokenResponseModel> LoginAsync(LoginModel loginModel);
}
public class EmployeeService(DatabaseContext _context, IConfiguration _configuration) : IEmployeeService
{

    public async Task<EmployeeResponseModel> CreateUserAsync(RegisterModel registerModel)
    {
        var hashedPassword = SecurityHelpers.GetHashedPasswordAndSalt(registerModel.Password);
        var emp = new Employee
        {
            Login = registerModel.Login,
            Password = hashedPassword.Item1,
            Salt = hashedPassword.Item2,
            Role = registerModel.Role,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();

        return new EmployeeResponseModel
        {
            Id = emp.Id,
            Login = emp.Login,
            Role = emp.Role
        };
    }

    public async Task<TokenResponseModel> RefreshTokenAsync(RefreshTokenModel refreshTokenModel)
    {
        var emp = await _context.Employees
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenModel.RefreshToken);

        if (emp == null || emp.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        var token = GenerateJwtToken(emp);
        emp.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        emp.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        return new TokenResponseModel
        {
            AccessToken = token,
            RefreshToken = emp.RefreshToken
        };
    }

    public async Task<TokenResponseModel> LoginAsync(LoginModel loginModel)
    {
        var emp = await _context.Employees
            .FirstOrDefaultAsync(u => u.Login == loginModel.Login);

        if (emp == null || emp.Password != SecurityHelpers.GetHashedPasswordWithSalt(loginModel.Password, emp.Salt))
        {
            throw new UnauthorizedAccessException("Wrong username or password");
        }

        var token = GenerateJwtToken(emp);
        emp.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        emp.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        return new TokenResponseModel
        {
            AccessToken = token,
            RefreshToken = emp.RefreshToken
        };
    }

    private string GenerateJwtToken(Employee emp)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, emp.Login),
            new Claim(ClaimTypes.Role, emp.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}