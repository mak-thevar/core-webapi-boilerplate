using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiBoilerPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ApiBaseController
    {
        private ITodoService _todoService;

        public TodoController(IServiceManager serviceManager)
        {
            this._todoService = serviceManager.TodoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todoList = await _todoService.GetAllAsync();

            return Ok(todoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todoList = await _todoService.GetByIdForCurrentUserAsync(id);

            return Ok(todoList);
        }
    }
}
