using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebApiBoilerPlate.Entity;
using HelpDeskBBQN.Infrastructure.Data;
using CoreWebApiBoilerPlate.Controllers.Base;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository;
using CoreWebApiBoilerPlate.Models;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ApiControllerBase
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public PostsController(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get the posts list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(DefaultResponseModel<IList<Post>>), 200)]
        [ProducesResponseType(typeof(DefaultResponseModel<string>), 404)]
        public async Task<IActionResult> GetPosts()
        {
            var result = await repository.PostRepository.GetAllAsync();
            if (!result.Any())
                return await DataNotFound();
            return await CreateSuccessResponse(result.ToList());
        }

        /// <summary>
        /// Get the post by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DefaultResponseModel<Post>), 200)]
        [ProducesResponseType(typeof(DefaultResponseModel<string>), 404)]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await repository.PostRepository.GetByIdAsync(id);

            if (post == null)
            {
                return await DataNotFound();
            }

            return await CreateSuccessResponse(post);
        }

        /// <summary>
        /// Update post by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces(typeof(DefaultResponseModel<string>))]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return await CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new Errors("Invalid Id", "The given post id is not valid."));
            }

            try
            {
                var result = repository.PostRepository.UpdateAsync(post);
                await repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return await DataNotFound();
                }
                else
                {
                    throw;
                }
            }

            return await CreateSuccessResponse("Updated successfully.", System.Net.HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a new post
        /// </summary>
        /// <param name="postRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(DefaultResponseModel<Post>))]
        public async Task<IActionResult> PostPost(PostRequestModel postRequestModel)
        {
            var post = mapper.Map<Post>(postRequestModel);
            var result = await repository.PostRepository.AddAsync(post);
            await repository.Save();
            return await CreateSuccessResponse(result);
        }

        /// <summary>
        /// Delete the Post by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(typeof(DefaultResponseModel<string>))]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await repository.PostRepository.GetByIdAsync(id);
            if (post == null)
            {
                return await DataNotFound();
            }
            repository.PostRepository.DeleteAsync(post);
            await repository.Save();

            return await CreateSuccessResponse("Deleted successfully.");
        }

        private bool PostExists(int id)
        {
            var exists = repository.PostRepository.GetByIdAsync(id);
            var result = exists.Result != null;
            return result;
        }
    }
}
