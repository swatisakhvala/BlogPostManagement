using CommentService.Command;
using CommentService.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CommentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator mediator;
        public CommentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<dynamic> AddCommentAsync(BlogComment blogComment)
        {
            AuthenticationToken Authtoken = new AuthenticationToken();
            var authorization = Request.Headers[HeaderNames.Authorization].Count != 0 ? Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1] : "";

            bool token = await Authtoken.Validate(authorization);
            if (!token)
            {
                Response.StatusCode = 401;
                return "Not Authenticated";
            }

            var CommentDetails = await mediator.Send(new CreateCommentCommand(
                blogComment.BlogPostId,
                blogComment.Title,
                blogComment.Comment,
                blogComment.IsPublished,
                blogComment.PublishedOn,
                blogComment.CreatedOn,
                blogComment.UpdatedOn
                ));

            return CommentDetails;
        }
    }
}
