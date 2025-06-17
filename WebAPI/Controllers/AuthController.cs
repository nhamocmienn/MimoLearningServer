using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Org.BouncyCastle.Crypto.Generators;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly EmailService _emailService;
        private readonly JwtHelper _jwt;

        public AuthController(MyDbContext context, EmailService emailService, JwtHelper jwt)
        {
            _context = context;
            _emailService = emailService;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var otp = OtpHelper.GenerateOtp(user.Email);
            _emailService.SendEmail(user.Email, "Email Verification OTP", $"Your OTP is: {otp}");

            return Ok("Registered. Check email for OTP.");
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyOtpRequest request)
        {
            if (!OtpHelper.VerifyOtp(request.Email, request.Otp))
                return BadRequest("Invalid OTP");

            OtpHelper.ClearOtp(request.Email);
            return Ok("Email verified successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.Email, user.Role);
            return Ok(new { Token = token, Role = user.Role, Name = user.Name });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return NotFound("Email not found");

            var otp = OtpHelper.GenerateOtp(user.Email);
            _emailService.SendEmail(user.Email, "Reset Password OTP", $"Your OTP is: {otp}");

            return Ok("OTP sent to email");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!OtpHelper.VerifyOtp(request.Email, request.Otp))
                return BadRequest("Invalid OTP");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return NotFound();

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();
            OtpHelper.ClearOtp(user.Email);

            return Ok("Password reset successful");
        }
    }
}
