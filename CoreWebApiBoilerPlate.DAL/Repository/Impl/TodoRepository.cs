using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Context;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Impl
{
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        private readonly DefaultDBContext dbContext;

        public TodoRepository(DefaultDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment> AddCommentAsync(int todoId, Comment comment)
        {
            
            comment.TodoId = todoId;
            var result = await dbContext.Comments.AddAsync(comment);
            return result.Entity;
        }
    }
}
