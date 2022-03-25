using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsService.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int ApprovalId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public bool IsActive { get; set; }

        //[ForeignKey("CreateUserId")]
        //public virtual UserAccount UserRole { get; set; }





    }
}
