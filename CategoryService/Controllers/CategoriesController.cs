using CategoryService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        NewsCategoryDBContext _ctx;
        //IJwtAuth _jwtAuth;
        public CategoriesController(NewsCategoryDBContext ctx)
        {
            _ctx = ctx;
            //_jwtAuth = jwtAuth;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_ctx.Categories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cat = _ctx.Categories.SingleOrDefault(c => c.Id == id);
            return Ok(cat);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult Post([FromBody] CategoryDto categoryDto)
        {
            Category entity = DtoToEntity(categoryDto);
            _ctx.Categories.Add(entity);
            _ctx.SaveChanges();
            return Ok(entity.Id);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string categoryName)
        {
            var catbyname = _ctx.Categories.FirstOrDefault(c => c.CategoryName.ToLower()==categoryName.ToLower());
            return Ok(catbyname);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _ctx.Categories.First(c => c.Id == id);
            _ctx.Categories.Remove(deleted);
            _ctx.SaveChanges();
            return Ok(id);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] CategoryDto categoryDto)
        {
            Category entity = DtoToEntity(categoryDto);
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
            return Ok(entity);
        }


        private Category DtoToEntity(CategoryDto categoryDto)
        {
            Category entity = new Category()
            {
                Id = categoryDto.Id,
                CategoryName = categoryDto.CategoryName,
                IsHeader = categoryDto.IsHeader,
                IsMainPage = categoryDto.IsMainPage
                
            };
            return entity;
        }
    }
}
