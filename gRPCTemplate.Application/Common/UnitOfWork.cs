using gRPCTemplate.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace gRPCTemplate.Application.Common;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MyContext _dbContext;

    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(MyContext context)
    {
        _dbContext = context;
    }

    public DbSet<T> DbSet<T>() where T : class
    {
        return _dbContext.Set<T>();
    }

    public async Task BeginTransaction(CancellationToken cancellationToken = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        _disposed = false;
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        try
        {
            await Save(cancellationToken);
            await _transaction!.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await Rollback();
            throw;
        }
    }

    public async Task Rollback(CancellationToken cancellationToken = default)
    {
        await _transaction!.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _dbContext.Dispose();

        _disposed = true;
    }
}