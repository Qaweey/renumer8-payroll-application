﻿using API;
using API.Authentication;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Remuner8_Backend.Dtos;
using Remuner8_Backend.EntityModels;
using Remuner8_Backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Remuner8_Backend.Controllers
{
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserAccountRepository RegisterRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMapper _mapper;

        public RegisterController(IUserAccountRepository registerRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
        {
            RegisterRepository = registerRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult <IEnumerable<PasswordReadDto>>> GetUsersAsync()
        {
            try
            {
                return Ok(await RegisterRepository.GetUsersAsync());
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        [HttpGet]
        [Route("api/[controller]/{email}")]
        public async Task<ActionResult <PasswordReadDto>> GetUserAsync(string email)
        {
            try
            {
                var userItem = await RegisterRepository.GetUserAsync(email);
                if (userItem != null)
                {
                    return Ok(_mapper.Map<PasswordReadDto>(userItem));
                }
                return NotFound($"User with email: {email} was not found");
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> AddUserAsync(PasswordCreateDto passwordcreatedto)
        {
            var identityUser = new IdentityUser
            {
                UserName = passwordcreatedto.Email,
                Email = passwordcreatedto.Email

            };
            var signInUser=await userManager.CreateAsync(identityUser);
            if (signInUser.Succeeded)
            {
                await  signInManager.SignInAsync(identityUser,true);
                return Ok();
            }
            return BadRequest();
           
          
        }

        [HttpDelete]
        [Route("api/[controller]/{email}")]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            try
            {
                var user = await RegisterRepository.GetUserAsync(email);

                if (user != null)
                {
                    RegisterRepository.DeleteUser(user);
                    return Ok(new Response { Status = "Success", Message = "User Deleted Successfully" });
                }
                return NotFound($"User with email: {email} was not found");
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        //[HttpPatch]
        //[Route("api/[controller]/{email}")]
        //public IActionResult EditUser(Password password)
        //{
        //    var existingUser = await RegisterRepository.GetUserAsync(password.Email);
        //    if (existingUser != null)
        //    {
        //        // password.Password1 = existingUser.Password1;
        //        RegisterRepository.EditUser(password);
        //    }
        //    return Ok(password);
        //}
    }
}