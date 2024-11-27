using DompifyAPI.Application.DTOs.Auth;
using DompifyAPI.Application.Helpers;
using DompifyAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthUseCase authUseCase) : ControllerBase
    {
        private readonly IAuthUseCase _authUseCase = authUseCase;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authUseCase.LoginAsync(request.Email!, request.Password!);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Login successful", user);
            }
            catch (ArgumentException ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _authUseCase.RegisterAsync(request.Email!, request.Password!, request.ConfirmPassword!);

                return ResponseFormatter.Success(StatusCodes.Status201Created, "User registered successfully", null);
            }
            catch (ArgumentException ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("create-new-password")]
        public async Task<IActionResult> CreateNewPassword([FromBody] CreateNewPasswordRequest request)
        {
            try
            {
                await _authUseCase.CreateNewPasswordAsync(request.Email!, request.Password!, request.ConfirmPassword!);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Password updated successfully", null);
            }
            catch (ArgumentException ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("send-otp-email")]
        public async Task<IActionResult> SendOtpEmail([FromBody] SendOtpEmailRequest request)
        {
            try
            {
                bool isSent = await _authUseCase.SendOtpEmailAsync(request.Email!);

                return ResponseFormatter.Success(StatusCodes.Status200OK, isSent ? "OTP sent successfully" : "OTP failed to send", null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("validate-otp-token")]
        public async Task<IActionResult> ValidateOtpToken([FromBody] ValidateOtpTokenRequest request)
        {
            try
            {
                bool isValid = await _authUseCase.ValidateOtpTokenAsync(request.Email!, request.OTP!);

                return ResponseFormatter.Success(StatusCodes.Status200OK, isValid ? "OTP is valid" : "OTP is invalid", null);
            }
            catch (ArgumentException ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
