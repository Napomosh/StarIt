namespace StarIt.Dal.AuthDal;

public interface IUserTokenDal
{
    public Task<Guid> Create(byte[] userId);
    public Task<Guid> GetUserId(byte[] userId);
    public Task Delete(byte[] tokenId);
}