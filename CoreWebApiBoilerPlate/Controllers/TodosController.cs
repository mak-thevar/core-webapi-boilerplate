using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.Domain.Entities.Enums;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApiBoilerPlate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ApiBaseController
    {

        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        public TodosController(IMapper mapper, IRepositoryWrapper repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }



        // GET: api/<TodosController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<List<TodoResponseModel>>))]
        public async Task<IActionResult> Get()
        {
            var result = await repository.TodoRepository.GetAllAsync("Comments", "CreatedBy", "TodoStatus");
            var response = mapper.Map<IReadOnlyList<TodoResponseModel>>(result);
            return CreateSuccessResponse(response);
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<TodoResponseModel>))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await repository.TodoRepository.GetByIdAsync(id, "CreatedBy,Comments,Comments.CreatedBy");
            if (result is null)
                return DataNotFound();
            return CreateSuccessResponse(result);
        }

        // POST api/<TodosController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<TodoResponseModel>))]
        public async Task<IActionResult> Post([FromBody] TodoRequestModel model)
        {
            var todo = mapper.Map<Todo>(model);
            var result = await repository.TodoRepository.AddAsync(todo);
            await repository.SaveAsync();
            return CreateSuccessResponse(result);
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<TodoResponseModel>))]
        public async Task<IActionResult> Put(int id, [FromBody] TodoRequestModel model)
        {

            var result = await repository.TodoRepository.UpdateAsync(id, model);
            await repository.SaveAsync();
            return CreateSuccessResponse(result);
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.TodoRepository.DeleteAsync(id);
            if (result)
                return CreateSuccessResponse($"Todo with id : {id} deleted successfully.");
            return CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new List<string> { "Error while deleting." });
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> AddComment(int id, CommentRequestDTO requestModel)
        {
            var comment = mapper.Map<Comment>(requestModel);
            try
            {
                var result = await repository.TodoRepository.AddCommentAsync(id, comment);
                await repository.SaveAsync();
                return CreateSuccessResponse(result);
            }
            catch (DBConcurrencyException)
            {
                return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"Please check if the todo with id {id} exists." });
            }

        }

        [HttpPost("completed/{id}")]
        public async Task<IActionResult> Completed(int id)
        {
            var result = await repository.TodoRepository.GetByIdAsync(id);
            if (result is null)
                return CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new List<string> { "Not found" });
            result.Status = TodoStatusEnum.Completed;
            await repository.SaveAsync();
            return CreateSuccessResponse(result);
        }
    }
}
