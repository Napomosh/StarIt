namespace StarIt.Bl;

public interface ICurrentUser
{
    public Task<bool> IsAuthenticated();
    public Task<Guid> GetUserIdByToken();
}