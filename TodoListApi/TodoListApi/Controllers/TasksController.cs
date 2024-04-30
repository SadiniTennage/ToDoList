using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Models;
using Task = TodoListApi.Models.Task;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<Task> tasks = new List<Task>();
        private static int nextId = 1;

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            return Ok(tasks);
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<Task> AddTask(Task task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return NoContent();
        }
    }
}
