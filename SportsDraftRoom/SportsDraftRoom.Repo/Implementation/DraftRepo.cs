namespace SportsDraftRoom.Repo.Implementation;

public class DraftRepo : IDraftRepo
{
    private readonly ISdrContext _context;
    public DraftRepo(ISdrContext context)
    {
        _context = context;
    }
    //Implement methods for draft operations here, e.g., GetDrafts, CreateDraft, etc.
    //Example:
     public async Task<Team> GetTeamsByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
    }
}
