using Microsoft.AspNetCore.Mvc;

public class AuthController(AppsContext appsContext, Appsettings appsettings) : Controller
{

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO request)
    {
        try
        {
            bool emailExist = appsContext.Users.Any(el => el.Email == request.Email);
            if (emailExist)
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Email Already Registered" });
            }

            bool usernameExist = appsContext.Users.Any(el => el.Username == request.Username);
            if (usernameExist)
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Username Already Registered" });
            }

            var (isStrong, reason) = PasswordUtil.CheckPasswordStrength(request.Password);
            if (!isStrong)
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = reason });
            }

            var (encrypted, salt) = PasswordUtil.EncryptPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                EncryptedPassword = encrypted,
                PasswordSalt = salt,
            };

            appsContext.Users.Add(user);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Registration Success" });
        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO request)
    {
        var user = appsContext.Users.FirstOrDefault(el => el.Username == request.Username);
        if (user == null)
        {
            Response.StatusCode = 400;
            return Json(new { success = false, message = "Unknown Username" });
        }

        bool isValid = PasswordUtil.VerifyPassword(request.Password, user.EncryptedPassword, user.PasswordSalt);
        if (isValid)
        {
            string token = PasswordUtil.GenerateToken(user, appsettings.Get("Secret"));
            return Json(new { success = true, message = "Login Success", token });
        }
        else
        {
            return Json(new { success = false, message = "Login Failed", token = string.Empty });
        }
    }
}