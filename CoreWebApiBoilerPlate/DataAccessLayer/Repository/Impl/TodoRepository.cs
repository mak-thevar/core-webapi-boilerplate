using CoreWebApiBoilerPlate.DataLayer.Context;
using CoreWebApiBoilerPlate.DataLayer.Entities;
using CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.DataLayer.Repository.Impl
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

        public async Task<int?> GetDefaultStatusByName(string name)
        {
            var result = await dbContext.TodoStatus.SingleOrDefaultAsync(x => x.IsDefault && x.Description.ToLower() == name.ToLower());
            return result?.Id;
        }
    }
}
