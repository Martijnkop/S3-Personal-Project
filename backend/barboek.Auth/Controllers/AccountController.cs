using barboek.Auth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace barboek.Auth.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private DataStore _dbContext;

    private readonly string MYSECRET = "DitIsEenSecretEnHiermeeZalJeHetMoetenDoen";

    public AccountController(ILogger<AccountController> logger, DataStore dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("register")]
    [Produces("application/json")]
    public IActionResult Register(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest();
        }

        Account? account = _dbContext.Accounts.FirstOrDefault(u => u.Email == email);
        if (account != null)
        {
            return NotFound();
        }

        byte[] refreshtoken = NewRefreshToken();

        Account user = new Account {
            Id = Guid.NewGuid(),
            Email = email,
            Password = HashPassword(email, password),
            RefreshToken = refreshtoken,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(30)
        };

        _dbContext.Accounts.Add(user);
        _dbContext.SaveChanges();

        Object returnObject = new
        {
            valid = true,
            accessToken = NewAccessToken(user),
            refreshToken = BitConverter.ToString(refreshtoken),
        };

        return Ok(returnObject);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest();
        }

        Account? account = _dbContext.Accounts.FirstOrDefault(u => u.Email == email);
        if (account == null)
        {
            return NotFound();
        }

        byte[] PasswordBytes = HashPassword(email, password);
        if (!account.Password.SequenceEqual(PasswordBytes))
        {
            return NotFound();
        }

        byte[] refreshtoken = NewRefreshToken();

        Object returnObject = new
        {
            valid = true,
            accessToken = NewAccessToken(account),
            refreshToken = BitConverter.ToString(refreshtoken),
        };

        _dbContext.Accounts.Update(account);
        account.RefreshToken = refreshtoken;
        account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
        _dbContext.SaveChanges();

        return Ok(returnObject);
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout(string accessToken)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://auth.barboek.nl/",
                ValidateAudience = true,
                ValidAudience = "https://barboek.nl/", 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(MYSECRET)),
                ValidateLifetime = true,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);

            JwtSecurityToken jwt = (JwtSecurityToken)validatedToken;

            Claim? idClaim = jwt.Claims.FirstOrDefault(c => c.Type == "userId", null);
            if (idClaim == null)
            {
                return BadRequest();
            }

            Guid id = Guid.Parse(idClaim.Value);

            Account? account = _dbContext.Accounts.FirstOrDefault(u => u.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            _dbContext.Accounts.Update(account);
            account.RefreshToken = new byte[0];
            account.RefreshTokenExpiry = DateTime.UnixEpoch;
            _dbContext.SaveChanges();

            return Ok();
        }
        catch (SecurityTokenValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("validaterefreshtoken")]
    public IActionResult ValidateRefreshToken(string refreshToken)
    {
        if (refreshToken == null || refreshToken.Length == 0)
        {
            return BadRequest();
        }
        byte[] refreshTokenBytes = Convert.FromHexString(refreshToken);

        List<Account?> users = _dbContext.Accounts.ToList();
        Account? user = users.FirstOrDefault(u => u.RefreshToken.SequenceEqual(refreshTokenBytes), null);
        if (user == null || DateTime.Compare(user.RefreshTokenExpiry, DateTime.Now) <= 0)
        {
            return BadRequest();
        }

        _dbContext.Accounts.Update(user);
        user.RefreshToken = NewRefreshToken();

        _dbContext.SaveChanges();

        string accessToken = NewAccessToken(user);

        object returnObject = new
        {
            valid = true,
            accessToken = accessToken,
            refreshToken = BitConverter.ToString(user.RefreshToken)
        };

        return Ok(returnObject);
    }

    private string NewAccessToken(Account user)
    {
        if (user == null)
        {
            return "";
        }


        SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(MYSECRET));

        string issuer = "https://auth.barboek.nl/";
        string audience = "https://barboek.nl/";

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private byte[] NewRefreshToken()
    {
        Random random = new Random();
        byte[] data = new byte[32];
        random.NextBytes(data);

        return data;
    }

    private byte[] HashPassword(string email, string password)
    {
        string secret = "MySecret123";

        using (SHA256 sha = SHA256.Create())
        {

            byte[] secretBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(secret));
            byte[] emailBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(email));
            byte[] passwordBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            byte[] completeBytes = secretBytes.Concat(emailBytes.Concat(passwordBytes)).ToArray();

            return sha.ComputeHash(completeBytes).ToArray();
        }
    }
}