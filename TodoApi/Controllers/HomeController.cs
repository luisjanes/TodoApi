using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] TodoDbContext context) 
            => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetbyId(int id, [FromServices] TodoDbContext context)
        {
            var Todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (Todo == null)
            {
                return NotFound("Não encontrado");
            }
            return Ok(Todo);
        }

        [HttpPost("/todo")]
        public IActionResult Post([FromBody] Todo todo, [FromServices]TodoDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/{todo.Id}",todo);
        }

        [HttpPut("/todo/{id:int}")]
        public IActionResult Update(int id, [FromBody] Todo todo, [FromServices] TodoDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x=>x.Id == id);
            if (model == null)
                return NotFound($"Para o id {id}, não foi encontrado registro");
            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/todo/{id:int}")]
        public IActionResult Delete(int id, [FromServices] TodoDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound($"Para o id {id}, não foi encontrado registro");
            model.Id = id;
            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}
