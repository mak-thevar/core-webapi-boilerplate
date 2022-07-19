using CoreWebApiBoilerPlate.BusinessLogic.Repository;
using CoreWebApiBoilerPlate.DataLayer.Context;
using CoreWebApiBoilerPlate.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.BusinessLogic.Impl
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
            var result = await this.dbContext.Comments.AddAsync(comment);
            return result.Entity;
        }

        public async Task<int?> GetDefaultStatusByName(string name)
        {
            var result = await this.dbContext.TodoStatus.SingleOrDefaultAsync(x=>x.IsDefault  && x.Description.ToLower() == name.ToLower());
            return result?.Id;
        }
    }
}
