namespace WebStore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        WebStoreDbContext dbContext;

        public WebStoreDbContext Init()
        {
            return dbContext ?? (dbContext = new WebStoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
