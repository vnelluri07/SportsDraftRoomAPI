namespace SportsDraftRoom.Repo.Implementation;

public class DraftRepo : IDraftRepo
{
    private readonly ISdrContext _context;
    //private readonly IDbContextFactory<SdrContext> _contextFactory;
    public DraftRepo(ISdrContext context)
    {
        _context = context;
      //  _contextFactory = contextFactory;
    }
    //Implement methods for draft operations here, e.g., GetDrafts, CreateDraft, etc.
    //Example:
     public async Task<Team> GetTeamsByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id) ?? new Team();
    }

    public async Task<IEnumerable<TeamsDto>> GetTeamsAsync(
        CancellationToken cancellationToken)
    {
        var teams = await  _context.Teams
            .Where(t => t.IsActive)
            .Select(t => new TeamsDto
            {
                Id = t.Id,
                Name = t.Name,
                IsActive = t.IsActive,
                Budget = t.Budget
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return teams;
    }

    public async Task<IEnumerable<AuctionItemsDto>> GetAuctionItemsAsync(
        CancellationToken cancellationToken)
    {
        var auctionItems = await _context.AuctionItems
            .Where(ai => ai.IsActive)
            .Select(ai => new AuctionItemsDto
            {
                Id = ai.Id,
                Name = ai.Name,
                IsActive = ai.IsActive,
                Picture = ai.Picture,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return auctionItems;
    }

    public async Task SaveTeamAuctionInfo(TeamAuctionItemInfo teamAuctionItemInfo, CancellationToken cancellationToken)
    {
        var info = new TeamAuctionItem
        {
            TeamId = teamAuctionItemInfo.TeamId,
            AuctionItemId = teamAuctionItemInfo.AuctionItemId,
            //CreatedBy = teamAuctionItemInfo.CreatedBy,
            CreatedBy = 200,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        await _context.TeamAuctionItems.AddAsync(info, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }


    //public async Task<int> GetDraftRoomIdAsync(CancellationToken cancellationToken)
    //{
    //    await using (var db = await _contextFactory.CreateDbContextAsync(cancellationToken))
    //    {
    //        int nextId = await db.Database
    //            .ExecuteSqlRawAsync("SELECT NEXT VALUE FOR DRAFT_ID_SEQ");

    //        return nextId;
    //    }
    //}
}
