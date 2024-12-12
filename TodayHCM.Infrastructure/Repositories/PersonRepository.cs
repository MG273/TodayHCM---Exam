namespace TodayHCM.Infrastructure.Repositories;

public class PersonRepository(TodayHcmExamContext _todayHcmExamContext) : IPersonsRepository
{
    private readonly TodayHcmExamContext _context = _todayHcmExamContext;
    public async Task<TeUser> AddPerson(TeUser teUser)
    {
        _context.TeUsers.Add(teUser);
        await _context.SaveChangesAsync();

        return teUser;
    }
}