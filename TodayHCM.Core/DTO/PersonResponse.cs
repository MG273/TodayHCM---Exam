namespace TodayHCM.Core.DTO;

public class PersonResponse
{
    public int ID { get; set; }
    public string? Name { get; set; }

    public string? Family { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }
}
public static class PersonExtensions
{
    /// <summary>
    /// An extension method to convert an object of TeUser class into PersonResponse class
    /// </summary>
    /// <param name="user">The Person object to convert</param>
    /// <returns>Returns the converted PersonResponse object</returns>
    public static PersonResponse ToPersonResponse(this TeUser user)
    {
        return new PersonResponse()
        {
            Password = user.Password,
            UserName = user.UserName,
            Email = user.Email,
            Family = user.Family,
            Name = user.Name,
            ID = user.Id
        };
    }
}
