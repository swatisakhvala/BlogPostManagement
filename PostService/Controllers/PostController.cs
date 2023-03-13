﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PostService.Command;
using PostService.Model;
using PostService.Query;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

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
        public async Task<dynamic> GetBlogPostByIdAsync(int Id)
        {
            AuthenticationToken Authtoken = new AuthenticationToken();
            var authorization = Request.Headers[HeaderNames.Authorization].Count != 0 ? Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1] : "";

            bool token = await Authtoken.Validate(authorization);
            if (!token)
            {
                Response.StatusCode = 401;
                return "Not Authenticated";
            }

            var PostDetails = await mediator.Send(new GetPostById() { Id = Id });
            return PostDetails;
        }

        [HttpPost]
        public async Task<dynamic> AddBlogPostAsync(BlogPost blogPost)
        {
            AuthenticationToken Authtoken = new AuthenticationToken();
            var authorization = Request.Headers[HeaderNames.Authorization].Count != 0 ? Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1] : "";

            bool token = await Authtoken.Validate(authorization);
            if (!token)
            {
                Response.StatusCode = 401;
                return "Not Authenticated";
            }

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
