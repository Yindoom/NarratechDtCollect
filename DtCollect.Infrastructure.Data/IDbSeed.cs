namespace DtCollect.Infrastructure.Data
{
    public interface IDbSeed
    {
        void SeedDb(DbContextDtCollect ctx);
    }
}