using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<AuthController> _logger;
    
        public AuthController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("auth/Register")]
        public async Task<IActionResult> Create([FromBody] RegisterUserDTO user)
        {
            _logger.LogInformation("auth/Create");
            var newUser = new IdentityUser {Email = user.Email, UserName = user.Name};

            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (isCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, user.IsAdmin ? "admin" : "user");
                return Ok(newUser);
            } 

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
            // Vérifier si l'utilisateur avec le même email existe
            var existingUser = await _userManager.FindByNameAsync(user.Name);
            if (existingUser != null)
            {
                // Maintenant, nous devons vérifier si l'utilisateur a entré le bon mot de passe.
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

            // Nous ne voulons pas donner trop d'informations sur la raison de l'échec de la demande pour des raisons de sécurité.
            return BadRequest(new AuthResponse
            {
                Result = false,
                Message = "Invalid authentication request"
            });
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles, IList<Claim> claims)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Nous obtenons notre secret à partir des paramètres de l'application.
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            // Nous devons utiliser les claims qui sont des propriétés de notre token et qui donnent des informations sur le token.
            // qui appartiennent à l'utilisateur spécifique à qui il appartient
            // donc il peut contenir son id, son nom, son email. L'avantage est que ces informations
            // sont générées par notre serveur qui est valide et de confiance.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim("School", "Epsic"),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
                Claims = new Dictionary<string, object>(),
                Expires = DateTime.UtcNow.AddHours(6),
                // ici, nous ajoutons l'information sur l'algorithme de cryptage qui sera utilisé pour décrypter notre token.
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