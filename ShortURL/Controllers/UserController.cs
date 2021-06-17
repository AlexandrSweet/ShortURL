using Businesslogic.Models;
using Businesslogic.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortURL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("get-users")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpPost]
        [Route("add-user")]
        public ActionResult<string> AddUser(UserDto user)
        {
            if (user.Login != null && user.Password != null)
            {
                return _userService.AddUser(user);
            }
            return BadRequest("Dont try to add an invalid data !!!");
        }
    }
}
