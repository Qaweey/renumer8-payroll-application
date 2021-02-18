﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remuner8_Backend.EntityModels;
using Remuner8_Backend.Repositories;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Remuner8_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository login;

        // POST api/<LoginController>
        public LoginController(ILoginRepository login)
        {
            this.login = login;
        }

        // GET: api/<PasswordController>
        [HttpPost]
        public ActionResult Validate([FromBody] PasswordModel model)
        {
            try
            {
                if (login.ValidateCredentials(model))
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}