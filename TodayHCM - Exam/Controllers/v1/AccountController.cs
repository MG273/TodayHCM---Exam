namespace TodayHCM___Exam.Controllers.v1;

[AllowAnonymous]
public class AccountController : TodayHCMControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPersonAdderService _personAdderService;
    private readonly IJwtService _jwtService;

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IPersonAdderService personAdderService,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _personAdderService = personAdderService;
        _jwtService = jwtService;
    }

    /// <summary>
    /// request a post method for register a user
    /// </summary>
    /// <param name="registerDTO">all parameters are necessary</param>
    /// <returns>Return JWT</returns>
    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
    {
        //Validation
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }


        //Create user
        ApplicationUser user = new ApplicationUser()
        {
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.PhoneNumber,
            UserName = registerDTO.Email,
            FirstName = registerDTO.PersonName
        };

        var person = new PersonAddRequest
        {
            email = registerDTO.Email,
            family = registerDTO.PersonName,
            name = registerDTO.PersonName,
            password = registerDTO.Password,
            userName = registerDTO.Email
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (result.Succeeded)
        {
            //sign-in
            await _signInManager.SignInAsync(user, isPersistent: false);

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            await _userManager.UpdateAsync(user);
            await _personAdderService.AddPerson(person);

            return Ok(authenticationResponse);
        }
        else
        {
            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description)); //error1 | error2
            return Problem(errorMessage);
        }
    }

    /// <summary>
    /// UI can check if Email is already used
    /// </summary>
    /// <param name="email">User Email</param>
    /// <returns>return True or False</returns>
    [HttpGet]
    public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Ok(false);
        }
        else
        {
            return Ok(true);
        }
    }

    /// <summary>
    /// UI send post request to login
    /// </summary>
    /// <param name="loginDTO">all parameters are necessary</param>
    /// <returns>return JWT</returns>
    [HttpPost("login")]
    public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
    {
        //Validation
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }


        var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return NoContent();
            }
            //sign-in
            await _signInManager.SignInAsync(user, isPersistent: false);

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        else
        {
            return Problem("Invalid email or password");
        }
    }

    /// <summary>
    /// UI send get request to logout, user must auth
    /// </summary>
    /// <returns>empty</returns>
    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> GetLogout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }

}
