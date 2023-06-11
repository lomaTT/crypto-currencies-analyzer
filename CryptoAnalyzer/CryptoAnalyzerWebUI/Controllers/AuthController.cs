using CryptoAnalyzerCore.DataBase.Services;
using CryptoAnalyzerCore.Model;
using Microsoft.AspNetCore.Mvc;
using CryptoAnalyzerWebUI.Authentication;

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
        Console.WriteLine("Login");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
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
    
    [HttpPost("logintest")]
    public IActionResult Login()
    {
        // Authenticate the user and get the User object
        User user = _userService.AuthenticateUser("user1", "password1");

        if (user != null)
        {
            string token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Perform logout logic here, such as invalidating the token or clearing session data
        // For example, if you're using JWT tokens, you could add the token to a blacklist or revoke it in some way

        // Return a success response
        return Ok();
    }
    
    [HttpPost("register")]
    public IActionResult Register(User registrationModel)
    {
        // Validate the registration model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the username or email already exists
        if (_userService.IsUsernameTaken(registrationModel.Login))
        {
            ModelState.AddModelError("Login", "Username is already taken");
            return BadRequest(ModelState);
        }

        if (_userService.IsEmailTaken(registrationModel.Email))
        {
            ModelState.AddModelError("Email", "Email is already registered");
            return BadRequest(ModelState);
        }

        // Create a new user object
        var user = new User
        {
            Login = registrationModel.Login,
            Email = registrationModel.Email,
            Password = registrationModel.Password,
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
