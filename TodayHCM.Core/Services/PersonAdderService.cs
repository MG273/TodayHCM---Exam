namespace TodayHCM.Core.Services;

public class PersonAdderService : IPersonAdderService
{
    private readonly IPersonsRepository _personsRepository;
    public PersonAdderService(IPersonsRepository repository)
    {
        _personsRepository = repository;
    }
    public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
    {
        if (personAddRequest == null)
        {
            throw new ArgumentNullException(nameof(personAddRequest));
        }
        TeUser user = personAddRequest.ToUser();
        await _personsRepository.AddPerson(user);
        return user.ToPersonResponse();
    }
}
