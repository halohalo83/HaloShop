namespace HaloShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HaloShopDbContext dbContext;

        public HaloShopDbContext Init()
        {
            return dbContext ?? (dbContext = new HaloShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}