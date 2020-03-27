using System;
using System.Collections.Generic;
using LB_151.Data;
using LB_151.Models;
using Microsoft.AspNetCore.Mvc;

namespace LB_151.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly CategoryDatabaseService _categoryDatabaseService;

        public CategoryController(AuthenticationService authenticationService,
            CategoryDatabaseService categoryDatabaseService)
        {
            _authenticationService = authenticationService;
            _categoryDatabaseService = categoryDatabaseService;
        }

        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            var authenticationString = HttpContext.Request.Headers["Authorization"];

            if (_authenticationService.IsValid(authenticationString))
            {
                return _categoryDatabaseService.Get();
            }

            return _categoryDatabaseService.GetStripped();
        }


        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _categoryDatabaseService.Get(id);
            var authenticationString = HttpContext.Request.Headers["Authorization"];

            if (category == null)
            {
                return NotFound();
            }

            if (_authenticationService.IsValid(authenticationString)) return category;
            return category.Stripped();
        }

        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            var authenticationString = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.IsValid(authenticationString))
            {
                _categoryDatabaseService.Create(category);

                return CreatedAtRoute("GetCategory", new {id = category.Id.ToString()}, category);
            }

            return Unauthorized();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Category categoryIn)
        {
            var authenticationString = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.IsValid(authenticationString))
            {
                var category = _categoryDatabaseService.Get(id);

                if (category == null)
                {
                    return NotFound();
                }

                _categoryDatabaseService.Update(id, categoryIn);

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var authenticationString = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.IsValid(authenticationString))
            {
                var category = _categoryDatabaseService.Get(id);

                if (category == null)
                {
                    return NotFound();
                }

                _categoryDatabaseService.Remove(category.Id);

                return NoContent();
            }

            return Unauthorized();
        }
    }
}