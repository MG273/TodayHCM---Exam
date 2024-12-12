namespace TodayHCM.Core.ServiceContracts;

public interface IPersonAdderService
{
    Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);
}
