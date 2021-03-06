using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotiWish_back.Configuration;
using SpotiWish_back.Model;

namespace SpotiWish_back.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("auth/CreateAdmin")]
        public async Task<IActionResult> Create([FromBody] RegisterUserDTO registerUser)
        {
            _logger.LogInformation("auth/CreateAdmin");
            var newUser = new User() {Email = registerUser.Email, UserName = registerUser.Name};

            var isCreated = await _userManager.CreateAsync(newUser, registerUser.Password);
            
            if (isCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "admin");
                return Ok(newUser);
            } 
            _logger.LogError("Can't create");
            return BadRequest(new AuthResponse
            {
                Result = false,
                Message = string.Join(Environment.NewLine, isCreated.Errors.Select(x => x.Description).ToList())
            });

        }
        [HttpPost]
        [Route("auth/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            _logger.LogInformation("auth/Register");
            var newUser = new User() {Email = registerUser.Email, UserName = registerUser.Name};

            var isCreated = await _userManager.CreateAsync(newUser, registerUser.Password);
            
            if (isCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "user");
                return Ok(newUser);
            } 
            _logger.LogError("Can't create");
            return BadRequest(new AuthResponse
            {
                
                Result = false,
                Message = string.Join(Environment.NewLine, isCreated.Errors.Select(x => x.Description).ToList())
            });

        }

        [HttpPost]
        [Route("auth/Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
        {
            _logger.LogInformation("auth/Login");
            // V??rifier si l'utilisateur avec le m??me Username existe
            var existingUser = await _userManager.FindByNameAsync(user.Name);
            if (existingUser != null)
            {
                // Maintenant, nous devons v??rifier si l'utilisateur a entr?? le bon mot de passe.
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var roles = await _userManager.GetRolesAsync(existingUser);
                    var claims = await _userManager.GetClaimsAsync(existingUser);

                    var jwtToken = GenerateJwtToken(existingUser, roles, claims);

                    return Ok(new AuthResponse
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }
            }

            // Nous ne voulons pas donner trop d'informations sur la raison de l'??chec de la demande pour des raisons de s??curit??.
            return BadRequest(new AuthResponse
            {
                Result = false,
                Message = "Invalid authentication request"
            });
        }

        private string GenerateJwtToken(User user, IList<string> roles, IList<Claim> claims)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Nous obtenons notre secret ?? partir des param??tres de l'application.
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            // Nous devons utiliser les claims qui sont des propri??t??s de notre token et qui donnent des informations sur le token.
            // qui appartiennent ?? l'utilisateur sp??cifique ?? qui il appartient
            // donc il peut contenir son id, son nom, son email. L'avantage est que ces informations
            // sont g??n??r??es par notre serveur qui est valide et de confiance.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
                Claims = new Dictionary<string, object>(),
                Expires = DateTime.UtcNow.AddHours(6),
                // ici, nous ajoutons l'information sur l'algorithme de cryptage qui sera utilis?? pour d??crypter notre token.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            foreach (var role in roles)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));

            foreach (var claim in claims)
                tokenDescriptor.Claims.TryAdd(claim.Type, claim.Value);

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
} 