using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        NewsDbContext _ctx;
        public UserRolesController(NewsDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserRoleDto roleDto)
        {
            UserRole entity = RoleDtoToEntity(roleDto);
            _ctx.UserRoles.Add(entity);
            _ctx.SaveChanges();

            return Ok(entity.Id);
        }

        [HttpPost("AddRole")]
        public IActionResult AddRoleToUser([FromBody] UserAccountRole userRole)
        {
            UserAccountRole userAccountRole = new UserAccountRole();
            userAccountRole.RolId = userRole.RolId;
            userAccountRole.UserId = userRole.UserId;
            _ctx.UserAccountRoles.Add(userAccountRole);
            _ctx.SaveChanges();
            return Ok(true);

        }

        private UserRole RoleDtoToEntity(UserRoleDto roleDto)
        {
            UserRole result = new UserRole()
            {
                Id = roleDto.Id,
                RollName = roleDto.RollName
            };
            return result;
        }
    }
}
