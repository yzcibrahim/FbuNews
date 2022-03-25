using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsService.Dtos;
using NewsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        NewsDbContext _ctx;

        public NewsController(NewsDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<News> Get()
        {
            return _ctx.News.ToList();
        }

        [HttpGet("{id}")]
        public News Get(int id)
        {
            return _ctx.News.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Post([FromBody] NewsDtos newsDtos)
        {
            News news = DtoToEntity(newsDtos);
            _ctx.News.Add(news);
            _ctx.SaveChanges();

            return Ok(news.Id);
        }

        [HttpPut]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Put([FromBody] NewsDtos newsDtos)
        {

            var entity = DtoToEntity(newsDtos);

            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var entity = _ctx.News.FirstOrDefault(c=>c.Id == id);
            
            _ctx.News.Remove(entity);
            _ctx.SaveChanges();
            return Ok(id);
        }

        private News DtoToEntity(NewsDtos newsDtos)
        {
            News entity = new News()
            {
                Id = newsDtos.Id,
                CategoryId = newsDtos.CategoryId,
                CreateUserId = newsDtos.CreateUserId,
                Title = newsDtos.Title,
                Brief = newsDtos.Brief,
                Content = newsDtos.Content,
                IsActive = true,
                IsShowOnSlide = true,
                ImagePath = newsDtos.ImagePath,
                ReadCount = newsDtos.ReadCount,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
        };



            return entity;
        }
    }
}
