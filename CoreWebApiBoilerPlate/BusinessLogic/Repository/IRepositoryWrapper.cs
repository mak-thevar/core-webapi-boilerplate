namespace CoreWebApiBoilerPlate.BusinessLogic.Repository
{
    public interface IRepositoryWrapper
    {
        public IUserRepository UserRepository { get; }
        public ITodoRepository TodoRepository { get; }
        Task<int> SaveAsync();
    }
}