using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Command;
using PostService.Model;
using PostService.Query;

namespace PostService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        public PostController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("Id")]
        public async Task<BlogPost> GetBlogPostByIdAsync(int Id)
        {
            var PostDetails = await mediator.Send(new GetPostById() { Id = Id });
            return PostDetails;
        }

        [HttpPost]
        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            var PostDetails = await mediator.Send(new CreatePostCommand(
                blogPost.Title,
                blogPost.MetaTitle,
                blogPost.Slag,
                blogPost.Summary,
                blogPost.PostContent,
                blogPost.IsPublished,
                blogPost.PublishedOn,
                blogPost.CreatedOn,
                blogPost.UpdatedOn,
                blogPost.BlogComment
                ));

            return PostDetails;
        }
    }
}
