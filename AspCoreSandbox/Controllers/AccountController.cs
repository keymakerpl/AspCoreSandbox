using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace AspCoreSandbox.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<StoreUser> _signinManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, SignInManager<StoreUser> signinManager, UserManager<StoreUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _signinManager = signinManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, loginViewModel.RemeberMe, false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }
            }

            ModelState.AddModelError("", "Failed to login");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.Username);

                if (user != null)
                {
                    var result = await _signinManager.CheckPasswordSignInAsync(user, viewModel.Password, false);

                    if (result.Succeeded)
                    {
                        // Tworzymy token do autoryzacji przez WEB API
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()) //unikalny id tokena
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_configuration["Tokens:Issuer"], 
                            _configuration["Tokens:Audience"], 
                            claims, 
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: cred);

                        var results = new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo };

                        return Created("", results);
                    }

                }
            }

            return BadRequest();
        }
    }
}
