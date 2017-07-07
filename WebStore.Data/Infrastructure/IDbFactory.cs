using System;

namespace WebStore.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        WebStoreDbContext Init();   
    }
}
