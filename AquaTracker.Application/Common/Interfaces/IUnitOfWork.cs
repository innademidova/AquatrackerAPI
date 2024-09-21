namespace AquaTracker.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}