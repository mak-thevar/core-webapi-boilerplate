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

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly DefaultContext _context;

        public UsersController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result =  await _context.Users.ToListAsync();
            if (result.Any())
                return await CreateSuccessResponse(result);
            return await DataNotFound();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

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
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return await DataNotFound();
                }
                else
                {
                    throw;
                }
            }

            return await CreateSuccessResponse("User updated successfully");
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await CreateSuccessResponse(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return await DataNotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return await CreateSuccessResponse($"User {user.Username} has been deleted successfully.");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
