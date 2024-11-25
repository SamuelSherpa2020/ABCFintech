using ABC.Data.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABC.Data.DataServices.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();
    }
}
