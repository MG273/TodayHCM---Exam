namespace TodayHCM.Core.Domain.RepositoryContracts;

public interface IPersonsRepository
{
    /// <summary>
    /// add an user to DB
    /// </summary>
    /// <param name="teUser">object user</param>
    /// <returns>info</returns>
    Task<TeUser> AddPerson(TeUser teUser);
}