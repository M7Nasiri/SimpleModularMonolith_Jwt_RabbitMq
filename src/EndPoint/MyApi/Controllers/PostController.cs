using Application.Posts.Create;
using Application.Posts.Edit;
using Common.AspNetCore;
using CoreModule.Query.Posts.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyApi.DTOs.Posts;
using MyApi.Security;
using Presentation.Facade.Posts;
using Domain.RoleAgg.Enums;


namespace MyApi.Controllers
{
    [Authorize]
    [PermissionChecker(Permission.PostManagement)]
    public class PostController : ApiController
    {
        private readonly IPostFacade _postFacade;
        private readonly IConfiguration _configuration;
        public PostController(IPostFacade postFacade, IConfiguration configuration)
        {
            _postFacade = postFacade;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<PostFilterResult>> GetAll([FromQuery]PostFilterParams param)
        {
            return QueryResult(await _postFacade.GetPostByFilter(param));
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<ApiResult<PostDto?>> GetById(Guid Id)
        {
            return QueryResult(await _postFacade.GetPostById(Id));
        }

        [HttpPost]
        public async Task<ApiResult> CreatePost([FromBody] CreatePostDto dto)
        {
            var userId = User.GetUserId();
            var command = new CreatePostCommand(userId, dto.Title, dto.Text);
            var result = await _postFacade.CreatePost(command);
            return CommandResult(result);
        }
        [HttpPut]
        public async Task<ApiResult> EditPost([FromBody] EditPostDto dto)
        {
            var userId = User.GetUserId();
            var command = new EditPostCommand(dto.PostId,userId, dto.Title, dto.Text);
            var result = await _postFacade.EditPost(command);
            return CommandResult(result);
        }

    }
}
