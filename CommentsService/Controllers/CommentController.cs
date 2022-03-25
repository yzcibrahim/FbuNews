using CommentsService.Dtos;
using CommentsService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        CommentsDbContext _ctx;
        public CommentController(CommentsDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return _ctx.Comments.ToList();
        }
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Get(int id)
        {
            var result = _ctx.Comments.FirstOrDefault(c => c.Id == id);

            CommentDto commentDto = new CommentDto();
            commentDto.Id = result.Id;
            commentDto.CommentText = result.CommentText;
            commentDto.CreateDate = DateTime.Now;
            commentDto.CreateUserId = result.CreateUserId;
            commentDto.ApprovalId = result.ApprovalId;

            if (result != null)
                return Ok(commentDto);
            return NoContent();

        }
        [HttpPost("add")]
        [Authorize(Roles ="Admin,User")]
        public IActionResult Post([FromBody] CommentDto comment)
        {
            Comment entity = DtoToEntity(comment);
            _ctx.Comments.Add(entity);
            _ctx.SaveChanges();

            return Ok(entity.Id);
        }
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Put([FromBody] CommentDto comment)
        {
            Comment entity = DtoToEntity(comment);
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Delete(int id)
        {
            var deleted = _ctx.Comments.First(c => c.Id == id);
            deleted.IsActive = false;
            _ctx.SaveChanges();
            return Ok(id);
        }
        private Comment DtoToEntity(CommentDto commentdto)
        {
            Comment entity = new Comment()
            {
                Id=commentdto.Id,
                CommentText = commentdto.CommentText,
                CreateDate = DateTime.Now,
                CreateUserId=commentdto.CreateUserId,
                ApprovalId=commentdto.ApprovalId,
                IsActive=true
                
            };
            return entity;
        }
    }
}
