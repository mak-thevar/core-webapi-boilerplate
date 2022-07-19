using CoreWebApiBoilerPlate.DataLayer.Entities;

namespace CoreWebApiBoilerPlate.BusinessLogic.Repository
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<int?>  GetDefaultStatusByName(string name);
        Task<Comment> AddCommentAsync(int todoId, Comment comment);
    }
}
