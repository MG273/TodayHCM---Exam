namespace TodayHCM.Core.DTO;

public class PersonAddRequest
{
    public string? name { get; set; }
    public string? family { get; set; }
    public string? userName { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }

    public TeUser ToUser()
    {
        return new TeUser() { Name = name, Email = email,  Family = family, UserName = userName, Password=password };
    }
}
