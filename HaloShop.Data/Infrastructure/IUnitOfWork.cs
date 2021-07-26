namespace HaloShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}