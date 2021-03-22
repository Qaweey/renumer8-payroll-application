﻿ using API.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Remuner8_Backend.EntityModels;
using Remuner8_Backend.Repositories;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Remuner8_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAccountRepository login;
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(IUserAccountRepository login, SignInManager<IdentityUser>signInManager)
        {
            this.login = login;
            this.signInManager = signInManager;
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<ActionResult> Validate([FromBody] PasswordReadDto model)
        {
            try
            {
                var userIdentity = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };
               var signinUser= await  signInManager.PasswordSignInAsync(userIdentity, model.Password1, true, false);
                if (signinUser.Succeeded)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status401Unauthorized);


            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}