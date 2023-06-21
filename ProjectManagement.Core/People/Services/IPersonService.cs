namespace ProjectManagement.Core.People.Services;

public interface IPersonService
{
    Task<Person> GetById(Guid id);
}
