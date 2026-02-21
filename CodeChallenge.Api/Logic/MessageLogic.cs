using CodeChallenge.Api.Models;
using CodeChallenge.Api.Repositories;

namespace CodeChallenge.Api.Logic;
{
   public class MessageLogic : IMessageLogic
   {
     private readonly IMessageRepository _repository;
     public MessageLogic(IMessageRepository repository)
     {
       _repository = repository
     }
      public async Task<Result> CreateMessageAsync(Guid organizationId, CreateMessageRequest request)
      {
        if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length < 3|| request.Title.Length > 200)
              return await.Result.ValidationError("Title is required and must be between 3 and 200 characters");
        
        if (string.IsNullOrWhiteSpace(request.Content) || request.Content.Length < 10 || request.Content.Length > 1000)
                return await Result.ValidationError("Content must be between 10 and 1000 characters");
        
        var existingmessages = await _repository.GetAllByOrganizationAsync(organizationId);
        
        if (existingmessages.Any(X=>X.Title.Equals(request.Title ,StringComparison.OrdinalIgnoreCase)))
            return Result.Conflict("Title must be unique per organization"); 
            
        var message = new Message
        {
          Id = Guid.NewGuid(),
          OrganizationId= organizationId,
          Title = request.Ttile,
          Content = request.Content,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        };
          
         var created = await _repository.CreateAsync(message);
         return Result.Success();                        
      }
            
      public async Task<Result> UpdateMessageAsync(Guid organizationId, Guid id, UpdateMessageRequest request)
      {
        var existing = await _repository.GetIdByAsync(organizationId,id);
        if (existing == null)
            return Result.NotFound();
        
        if(!existing.IsActive)
            return Result.ValidateError("cannot delete InActive comment);

        if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length < 3|| request.Title.Length > 200)
              return await.Result.ValidationError("Title is required and must be between 3 and 200 characters");
        
        if (string.IsNullOrWhiteSpace(request.Content) || request.Content.Length < 10 || request.Content.Length > 1000)
                return await Result.ValidationError("Content must be between 10 and 1000 characters");
        
        var existingmessages = await _repository.GetAllByOrganizationAsync(organizationId);
        
        if (existingmessages.Any(X=>X.Title.Equals(request.Title ,StringComparison.OrdinalIgnoreCase)))
            return Result.Conflict("Title must be unique per organization");                                 
                                        
          existing.Title = request.Title;
          existing.Content = request.Content;
          existing.UpdateAt.DateTime.UtcNow;
            
          await _repository.UpdateMessageAsync(existing);  
          return Result.Success();                              
        
      }
                                        
     
      public async Task<Result> DeleteMessageAsync(Guid organizationId, Guid id)
      {
        var existing = await _repository.GetIdByAsync(organizationId,id);
          if (existing == null)
            return Result.NotFound();
          if(!existing.IsActive)
            return Result.ValidateError("cannot delete InActive comment);
           await _repository.DeleteMessageAsync(existing):
           return Result.Success();                             
      }
                                        
      public async Task<Message?> GetMessageAsync(Guid organizationId, Guid id)
      {
        var message = await _repository.GetIdByAsync(organizationId, id);
        if (message == null)
          return NotFound();
        return Ok(message);
      }
                                        
      public async Task<IEnumerable<Message>> GetAllMessagesAsync(Guid organizationId)
      {
        return  await _repository.GetAllByOrganizationAsync(organizationId);
        
      }
