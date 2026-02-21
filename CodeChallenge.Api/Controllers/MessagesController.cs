using CodeChallenge.Api.Models;
using CodeChallenge.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers;

[ApiController]
[Route("api/v1/organizations/{organizationId}/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageLogic _messageLogic;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageLogic messageLogic, ILogger<MessagesController> logger)
    {
        _messageLogic = messageLogic;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetAllAsync(Guid organizationId)
    {
        // TODO: Implement
        var messages = _messageLogic.GetAllMessagesAsync(organizationId);
        return ok(messages):
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetById(Guid organizationId, Guid id)
    {
        // TODO: Implement
        var message = _messageLogic.GetMessageasync(organizationId, id);
        if(message == null){
           return NotFound();
        }
        return Ok(message):
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Create(Guid organizationId, [FromBody] CreateMessageRequest request)
    {
        // TODO: Implement
        var result = _messageLogic.CreateMessageAsync(organizationId,message):
        if (result.IsValidationError)
            return BadRequest(result.Error);

        if (result.IsConflict)
            return Conflict(result.Error);

        return Created("", null);
        
        
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid organizationId, Guid id, [FromBody] UpdateMessageRequest request)
    {
        // TODO: Implement
         var result  = _messageLogic.UpdateMessageAsync(organizationId, id, request);

        if (result.IsNotFound)
            return NotFound();

        if (result.IsValidationError)
            return BadRequest(result.Error);

        if (result.IsConflict)
            return Conflict(result.Error);

        return NoContent();

        
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid organizationId, Guid id)
    {
        // TODO: Implement
        var result = _messageLogic.DeleteMessageAsync(organizationId, id):
         if (result.IsNotFound)
            return NotFound();

        if (result.IsValidationError)
            return BadRequest(result.Error);

        return NoContent();
          
    }
}


