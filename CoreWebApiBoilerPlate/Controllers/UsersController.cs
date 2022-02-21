using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDeskBBQN.Infrastructure.Data;
using CoreWebApiBoilerPlate.Controllers.Base;
using CoreWebApiBoilerPlate.Entity;
using Microsoft.AspNetCore.Authorization;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using CoreWebApiBoilerPlate.Models;
using AutoMapper;

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        private readonly IRepositoryWrapper _context;
        private readonly IMapper mapper;
        public UsersController(IRepositoryWrapper context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result =  await _context.UserRepository.GetAllAsync();
            if (result.Any())
                return await CreateSuccessResponse(result);
            return await DataNotFound();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                return await DataNotFound();
            }

            return await CreateSuccessResponse(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return await CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new Models.Errors("Error","UserId not found"));
            }

            _context.UserRepository.UpdateAsync(user);
            await _context.Save();
            return await CreateSuccessResponse("User updated successfully");
        }

        /// <summary>
        /// Register a new User
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser(UserRequestModel userReqModel)
        {
            var user = mapper.Map<User>(userReqModel);
            var result = await _context.UserRepository.AddAsync(user);
            await _context.Save();
            return await CreateSuccessResponse(user);
        }
    }
}
