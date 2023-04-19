namespace CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        public IUserRepository UserRepository { get; }
        public ITodoRepository TodoRepository { get; }
        Task<int> SaveAsync();
    }
}