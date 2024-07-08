using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<Comment> AddCommentAsync(int todoId, Comment comment);
    }
}
