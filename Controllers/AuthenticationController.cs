using System;
using LB_151.Data;
using LB_151.Models;
using Microsoft.AspNetCore.Mvc;

namespace LB_151.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult Authenticate(PasswordRequest passwordRequest)
        {
            return Ok(_authenticationService.Authenticate(passwordRequest.Password, passwordRequest.Email));
        }
    }

    public class PasswordRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}