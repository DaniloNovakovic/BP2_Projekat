namespace Core.Interfaces
{
    public interface IUoWFactory
    {
        void Initialize();

        IUnitOfWork CreateNew();
    }
}