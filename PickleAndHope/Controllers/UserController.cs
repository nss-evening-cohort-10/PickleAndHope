using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleAndHope.Models;

namespace PickleAndHope.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : FirebaseEnabledController
    {
        [HttpPost("register")]
        public IActionResult RegisterUser(User userToRegister)
        {
            //register the user

            return Ok();
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            //pull user info from the database based on email or firebaseid or whatever other property

            return Ok(new {Email = UserEmail, FirebaseId = UserId});
        }
    }
}
