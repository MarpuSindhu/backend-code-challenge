using CodeChallenge.Api.Models;
using CodeChallenge.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers;

[ApiController]
[Route("api/v1/organizations/{organizationId}/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _repository;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageRepository repository, ILogger<MessagesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetAll(Guid organizationId)
    {
        // TODO: Implement
        var messages = _repository.GetAllByOrganizationAsync(organizationId);
        return ok(messages):
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetById(Guid organizationId, Guid id)
    {
        // TODO: Implement
        var message = _repository.GetByIdAsync(organizationId, id);
        if(message == null){
           return NotFound();
        }
        return Ok(message);
        
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Create(Guid organizationId, [FromBody] CreateMessageRequest request)
    {
        // TODO: Implement
        var message = new Message
        {
          Id = Guid.NewGuid(),
          OrganizationId= organizationId,
          Title= request.Title,
          Content = request.Content,
          IsActive = true
        };
        var created = _repository.CreateAsync(message):
        return ok(created);
        
        
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid organizationId, Guid id, [FromBody] UpdateMessageRequest request)
    {
        // TODO: Implement
         var existing = _repository.GetIdByAsync(organizationId, id);
         if (existing == null)
              return NotFound();
         existing.Title= request.Title;
         existing.Content = request.Content;
         var updated = _repository.UpdateAsync(existing):
         return Ok(updated);
        
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid organizationId, Guid id)
    {
        // TODO: Implement
        var deleted = _repository.DeleteAsync(organizationId, id):
        if(!deleted){
          return NotFound();
          }
          return Ok(deleted):
          
        
        throw new NotImplementedException();
    }
}

