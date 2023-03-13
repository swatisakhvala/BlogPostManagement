using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Model;

namespace PostService.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContextClass _dbContext;
        public PostRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> AddPost(BlogPost blogPost)
        {
            var result = _dbContext.BlogPost.Add(blogPost);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BlogPost> GetPostByIdWithComment(int Id) 
        {
            return await _dbContext.BlogPost.Include(x=>x.BlogComment).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
