using erj_api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [Route("api/Users/GetAll")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            var users = await _usersRepository.GetAsync(id);
            return Ok(users);
        }
    }
}
