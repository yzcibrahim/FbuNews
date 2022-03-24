using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {


        NewsDbContext _ctx;
        IJwtAuth _jwtAuth;
        public UserAccountController(NewsDbContext ctx, IJwtAuth jwtAuth)
        {
            _ctx = ctx;
            _jwtAuth = jwtAuth;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usr= _ctx.UserAccounts.FirstOrDefault(c=>c.Id==id);
            if (usr != null)
                return Ok(usr);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<UserAccount> Get()
        {
            var claims = Request.HttpContext.User.Claims;
            var userId = Convert.ToInt32(claims.First(c => c.Type == "userId").Value);
            return _ctx.UserAccounts.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserAccountDto usrAccount)
        {
            UserAccount entity = DtoToEntity(usrAccount);
            _ctx.UserAccounts.Add(entity);
            _ctx.SaveChanges();

            return Ok(entity.Id);
        }

        [HttpPut]
        public IActionResult asdf([FromBody] UserAccountDto usrAccount)
        {
            UserAccount entity = DtoToEntity(usrAccount);
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _ctx.UserAccounts.First(c => c.Id == id);
            deleted.IsActive = false;
            _ctx.SaveChanges();
            return Ok(id);
        }

        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserAccountDto userCredential)
        {
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        private UserAccount DtoToEntity(UserAccountDto usrDto)
        {
            UserAccount entity = new UserAccount()
            {
                Email = usrDto.Email,
                IsActive = true,
                Password = usrDto.Password,
                UserName = usrDto.UserName
            };
            return entity;
        }
    }
}
