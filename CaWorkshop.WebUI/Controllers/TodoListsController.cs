using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaWorkshop.WebUI.Data;
using CaWorkshop.WebUI.Models;

namespace CaWorkshop.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
            return await _context.TodoLists
                .Select(l => new TodoList
                {
                    Id = l.Id,
                    Title = l.Title,
                    Items = l.Items.Select(i => new TodoItem
                    {
                        Id = i.Id,
                        ListId = i.ListId,
                        Title = i.Title,
                        Done = i.Done,
                        Priority = i.Priority,
                        Note = i.Note
                    }).ToList()
                }).ToListAsync();
        }

        // PUT: api/TodoLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id, TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return BadRequest();
            }
            
            _context.TodoLists.Update(todoList);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostTodoList(TodoList todoList)
        {
            _context.TodoLists.Add(todoList);
            
            await _context.SaveChangesAsync();

            return todoList.Id;
        }

        // DELETE: api/TodoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var todoList = await _context.TodoLists.FindAsync(id);
            
            if (todoList == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(todoList);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoListExists(int id)
        {
            return (_context.TodoLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
