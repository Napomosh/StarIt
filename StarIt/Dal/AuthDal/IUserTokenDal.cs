namespace StarIt.Dal.AuthDal;

public interface IUserTokenDal
{
    public Task<Guid> Create(Guid userId);
    public Task<Guid> GetUserId(Guid userId);
    public Task Delete(Guid tokenId);
    public Task Delete(string tokenId);
}