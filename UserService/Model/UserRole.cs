using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RollName { get; set; }
       
        public virtual ICollection<UserAccountRole> UserAccounts { get; set; }
    }

    public class UserAccountRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RolId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserAccount UserAccount { get; set; }
        [ForeignKey("RolId")]
        public virtual UserRole UserRole { get; set; }
    }
}
