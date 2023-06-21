using ProjectManagement.Core.People.Services;

namespace ProjectManagement.API.GraphQL.Queries;

public class PersonQueries
{
    private readonly IPersonService _service;

    public PersonQueries(IPersonService service)
    {
        _service = service;
    }
    
    public async Task<object> GetById(Guid id)
    {
        return await _service.GetById(id);
    }
}
