using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public string RollName { get; set; }
       
    }

   
}
