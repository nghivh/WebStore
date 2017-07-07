namespace WebStore.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
