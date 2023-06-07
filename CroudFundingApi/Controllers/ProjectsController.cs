using Core.Entities;
using Core.Interfaces;
using CroudFundingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace CroudFundingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IGenericRepository<ProjectEntity> _genericRepositoryProject;
       // private readonly IGenericRepository<RegisterEntity> _genericRepositoryRegister;
       private readonly IServices _iservices;
        public ProjectsController(IGenericRepository<ProjectEntity> genericRepositoryProject, IServices iservices)
        {
            _genericRepositoryProject = genericRepositoryProject;
           //_genericRepositoryRegister = genericRepositoryRegister;
            _iservices = iservices;
        }
        //[HttpPost]
        //public async Task<ActionResult<ProjectEntity>> CreateProjectAsync([FromQuery]ProjectEntity project)
        //{
        //    return Ok(await _genericRepositoryProject.CreateAsync(project));
        //}

       /* [HttpPost("register")]
        public async Task<ActionResult<RegisterEntity>> CreateRegisterAsync([FromQuery] RegisterEntity register)
        {

            var new_register = await _genericRepositoryRegister.CreateAsync(register);
            var token = _iservices.GenerateEmailVerificationToken(new_register.Email);
           await _iservices.SendRegistrationEmailAsync(new_register.Email, token);
            return Ok(new_register);   

        }
       */

      /*  [HttpPost("activate")]
        public async Task<IActionResult> ActivateRegisterAsync(string token)
        {
            string email;
            
            if(_iservices.ValidateEmailVerificationToken(token, out email))
            {

                if(email != null)
                {
                    var register = await _genericRepositoryRegister.ActiveUserAsync(email);
                    if(register != null)
                    {
                        return Ok();
                    }
                    else

                        return BadRequest("user not exists after verfy email");
                }
                else
                {
                    return BadRequest("emial is null ");
                }
            }
            else
            {
                return BadRequest("token expire");
            }
        }
      */
       
    }
}
