using CryptoAnalyzerCore.DataBase.Services;
using CryptoAnalyzerCore.Model;
using Microsoft.AspNetCore.Mvc;
using CryptoAnalyzerWebUI.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CryptoAnalyzerWebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthenticationService _authenticationService;

    public AuthController()
    {
        _userService = new UserService();
        _authenticationService = new AuthenticationService();
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Authenticate the user and get the User object
        User user = _userService.AuthenticateUser(model.Login, model.Password);

        if (user != null)
        {
            string token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
    
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // Clear session data
        HttpContext.Session.Clear();

        // Return a success response
        return Ok();
    }
    
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest model)
    {
        // Validate the registration model
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if the username or email already exists
        if (_userService.IsUsernameTaken(model.Login))
        {
            ModelState.AddModelError("Login", "Username is already taken");
            return BadRequest(ModelState);
        }

        if (_userService.IsEmailTaken(model.Email))
        {
            ModelState.AddModelError("Email", "Email is already registered");
            return BadRequest(ModelState);
        }

        // Create a new user object
        var user = new User
        {
            Login = model.Login,
            Email = model.Email,
            Password = model.Password,
            RegistrationDate = DateTime.UtcNow
        };

        // Store the user in the data store
        _userService.AddUser(user);

        // Generate a JWT token for the registered user
        string token = _authenticationService.GenerateJwtToken(user);

        // Return the token in the response
        return Ok(new { Token = token });
    }
}
