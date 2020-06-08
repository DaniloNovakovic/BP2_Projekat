namespace Core.Interfaces
{
    public interface IUoWFactory
    {
        IUnitOfWork CreateNew(bool seedDatabase = false);
    }
}