using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusAPI.Data;
using TekusAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class CustomFieldController : ControllerBase
{
    private readonly ProvidersTekusDbContext _context;

    public CustomFieldController(ProvidersTekusDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomField>>> GetCustomFields()
    {
        return await _context.CustomFields.ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomField>> GetCustomField(int id)
    {
        var customField = await _context.CustomFields.FindAsync(id);

        if (customField == null)
        {
            return NotFound();
        }

        return customField;
    }

    
    [HttpPost]
    public async Task<ActionResult<CustomField>> PostCustomField(CustomField customField)
    {
        _context.CustomFields.Add(customField);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCustomField), new { id = customField.IdCustomField }, customField);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomField(int id, CustomField customField)
    {
        if (id != customField.IdCustomField)
        {
            return BadRequest();
        }

        _context.Entry(customField).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

}

