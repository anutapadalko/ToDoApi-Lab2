using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    private readonly ToDoContext _context;
    public AttachmentsController(ToDoContext context)
    {
        _context = context;
    }

    // GET: api/Attachment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachment()
    {
        return await _context.Attachments.ToListAsync();
    }

    // GET: api/Attachment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Attachment>> GetAttachment(int id)
    {
        var attachment = await _context.Attachments.FindAsync(id);

        if (attachment == null)
        {
            return NotFound();
        }

        return attachment;
    }

    // PUT: api/Attachment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAttachment(int? id, Attachment attachment)
    {
        if (id != attachment.Id)
        {
            return BadRequest();
        }

        _context.Entry(attachment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AttachmentExists(id))
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

    // POST: api/Attachment
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Attachment>> PostAttachment(Attachment attachment)
    {
        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAttachment", new { id = attachment.Id }, attachment);
    }

    // DELETE: api/Attachment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(int? id)
    {
        var attachment = await _context.Attachments.FindAsync(id);
        if (attachment == null)
        {
            return NotFound();
        }

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AttachmentExists(int? id)
    {
        return _context.Attachments.Any(e => e.Id == id);
    }
}
