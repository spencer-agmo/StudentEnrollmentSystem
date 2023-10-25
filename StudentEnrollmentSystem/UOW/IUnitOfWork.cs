using StudentEnrollmentSystem.Repository;

namespace StudentEnrollmentSystem.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
