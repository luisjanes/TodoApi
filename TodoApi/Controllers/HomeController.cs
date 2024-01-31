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
        public List<Todo> Get([FromServices] TodoDbContext context)
        {
            return context.Todos.ToList();
        }
        [HttpGet("/{id:int}")]
        public Todo GetbyId(int id, [FromServices] TodoDbContext context)
        {
            return context.Todos.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("/todo")]
        public Todo Post([FromBody] Todo todo, [FromServices]TodoDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }

        [HttpPut("/todo{id:int}")]
        public Todo Update(int id, [FromBody] Todo todo, [FromServices] TodoDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x=>x.Id == id);
            if (model == null)
                return todo;
            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return model;
        }

        [HttpDelete("/todo{id:int}")]
        public Todo Delete(int id, [FromServices] TodoDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return null;
            model.Id = id;
            context.Todos.Remove(model);
            context.SaveChanges();
            return model;
        }
    }
}
