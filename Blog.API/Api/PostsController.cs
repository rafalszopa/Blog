using Blog.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<PostDto> Get(int id)
        {
            return new PostDto
            {
                Id = id,
                Title = "Example post",
                Content = "Very interesting programing article"
            };
        }
    }
}
