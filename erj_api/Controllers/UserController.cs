using erj_api.Models;
using erj_api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace erj_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _usersRepository;
        public UserController(IUserRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }


        [HttpGet]
        [Route("api/Users/GetAll")]
        public async Task<ActionResult> Get()
        {           
            var users = await _usersRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/Users/Get")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            var users = await _usersRepository.GetAsync(id);
            return Ok(users);
        }

        [HttpPost]
        [Route("api/Users/Add")]
        public async Task<ActionResult> Add(Users user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return BadRequest();

            var existingUser = await _usersRepository.GetFirstOrDefaultByDynamicAsync(new { Username = user.UserName });
            if(existingUser != null || existingUser.Id != Guid.Empty)
                return BadRequest();

            if(ValidatePassword(user.Password, user.UserName))
                return BadRequest();

            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            var users = await _usersRepository.InsertAsync(user);
            return Ok(users);
        }

        [HttpPost]
        [Route("api/Users/changePassword")]
        public async Task<ActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                return BadRequest();

            var existingUser = await _usersRepository.GetFirstOrDefaultByDynamicAsync(new { Username = username, Password = oldPassword });
            if (existingUser == null || existingUser.Id == Guid.Empty)
                return BadRequest();

            if (ValidatePassword(newPassword, username))
                return BadRequest();

            existingUser.ModifiedDate = DateTime.Now;
            await _usersRepository.UpdateAsync(existingUser);
            return Ok();
        }
        private bool ValidatePassword(string password, string username)
        {
            bool hasRequiredLength = password.Length >= 8;
            Regex rg = new Regex(@"(?=.*[a-z])");
            bool hasLowerCase = rg.IsMatch(password);
            rg = new Regex(@"(?=.*[A-Z])");
            bool hasUpperCase = rg.IsMatch(password);
            rg = new Regex(@"(?=.*\d)");
            bool hasDigit = rg.IsMatch(password);
            rg = new Regex(@"(.)\1\1");
            bool hasConsecutive = rg.IsMatch(password);
            rg = new Regex(@"[!@#$%^*(),.?:{}_|]");
            bool hasSpecialCharacter = rg.IsMatch(password);
            rg = new Regex(@"[<>&/""']");
            bool hasForbiddenCharacters = rg.IsMatch(password);
            bool has4Policies = ((hasLowerCase ? 1 : 0) + (hasUpperCase ? 1 : 0) + (hasDigit ? 1 : 0) + (hasSpecialCharacter ? 1 : 0)) >= 4;
            bool containsUsername = password.ToLower().Contains(username.ToLower().Split("@")[0]);
            bool containsPassword = password.ToLower().Contains("password");
            return hasRequiredLength && has4Policies && !hasConsecutive
                && !containsUsername && !containsPassword && !hasForbiddenCharacters;
        }

    }
}
