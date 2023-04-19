using CoreWebApiBoilerPlate.DataLayer.Entities;

namespace CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<int?> GetDefaultStatusByName(string name);
        Task<Comment> AddCommentAsync(int todoId, Comment comment);
    }
}
