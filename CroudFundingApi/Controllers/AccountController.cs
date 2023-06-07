using Core.Identity;
using Core.Interfaces;
using CroudFundingApi.Dto;
using CroudFundingApi.Errors;
using CroudFundingApi.Extentions;
using CroudFundingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CroudFundingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IServices _services;
       

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices,IServices services)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices; 
            _services = services;
        }
        [HttpPost("Logn")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if(user==null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result=await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                DisplayNaem = user.address.FirstName+" "+user.address.LastName,
                Token = _tokenServices.GetToken(user)
            };

        }

        [Authorize]
        [HttpPost("VerfyEmail/{email}/{token}")]
        public async Task<bool> VerifyEmailAsync(string IdUser, string token)
        {
            var user = await _userManager.FindByIdAsync(IdUser);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromQuery]RegisterDto registerDto)
        {
            if (registerDto.imageFile == null || registerDto.imageFile.Length == 0)
            {
                return BadRequest("No image file provided.");
            }
            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await registerDto.imageFile.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                address=new Address
                {
                    FirstName=registerDto.FirstName,
                    LastName=registerDto.LastName,
                    Phone=registerDto.phone,
                    ImageData=imageData
                }
               
            };

            var result=await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _services.SendEmailConfirmAsync(registerDto.Email, "https://croundFunding.com/confirm-email/"+user.Email+"/"+token);
           
            return new UserDto
            {
                DisplayNaem = registerDto.FirstName +" "+registerDto.LastName,
                Email = registerDto.Email,
                Token = token
            };
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var user = await _userManager.FindEmailFromClaimPrincipal(User);
            return new UserDto
            {
                Email = user.Email,
                DisplayNaem = user.address.FirstName+" "+user.address.LastName,
                Token = _tokenServices.GetToken(user)
            };
        }
        [HttpGet("EmailExists")]
        public async Task<bool> CheckEmailExistsAsync([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email) !=null;

        }

        [HttpPost("ResetPassword")]
        public async Task<bool> ResetPaswwordSendEmailAsync(string idUser)
        {
            var user = await _userManager.FindByIdAsync(idUser);
            var token=await _userManager.GeneratePasswordResetTokenAsync(user);

            return await _services.SendEmailConfirmAsync(user.Email, "https://croundFunding.com/ChangePassword/" + user.Email + "/" + token);
        }

        [Authorize]
        [HttpPost("SetNewPassword")]
        public async Task<bool> SetNewPasswordAsync(string token,string idUser,string newPassword)
        {

            var user = await _userManager.FindByIdAsync(idUser);
          var result=  await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                // Password updated successfully
                return true;
            }
            else
            {
                // Failed to update the password
                return false;
            }
        }

    }
}
