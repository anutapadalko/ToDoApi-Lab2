using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class SubTasksController : ControllerBase
{
    private readonly ToDoContext _context;
    public SubTasksController(ToDoContext context)
    {
        _context = context;
    }

    // GET: api/SubTask
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubTask>>> GetSubTask()
    {
        return await _context.SubTasks.ToListAsync();
    }

    // GET: api/SubTask/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubTask>> GetSubTask(int id)
    {
        var subtask = await _context.SubTasks.FindAsync(id);

        if (subtask == null)
        {
            return NotFound();
        }

        return subtask;
    }

    // PUT: api/SubTask/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubTask(int? id, SubTask subtask)
    {
        if (id != subtask.Id)
        {
            return BadRequest();
        }

        _context.Entry(subtask).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubTaskExists(id))
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

    // POST: api/SubTask
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SubTask>> PostSubTask(SubTask subtask)
    {
        _context.SubTasks.Add(subtask);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubTask", new { id = subtask.Id }, subtask);
    }

    // DELETE: api/SubTask/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubTask(int? id)
    {
        var subtask = await _context.SubTasks.FindAsync(id);
        if (subtask == null)
        {
            return NotFound();
        }

        _context.SubTasks.Remove(subtask);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubTaskExists(int? id)
    {
        return _context.SubTasks.Any(e => e.Id == id);
    }
}
