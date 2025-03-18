//using AutoMapper;
//using BarberShop.Contract;
//using BarberShop.Models.BlogPostDtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BarberShop.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BlogPostsController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        private readonly IBlogPostRepository _blogPostRepository;

//        public BlogPostsController(IMapper mapper, IBlogPostRepository blogPostRepository)
//        {
//            _mapper = mapper;
//            _blogPostRepository = blogPostRepository;
//        }

//        // GET: api/BlogPosts
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<GetBlogPostDto>>> GetBlogPosts()
//        {
//            var blogPosts = await _blogPostRepository.GetAllAsync();
//            var blogPostDtos = _mapper.Map<IEnumerable<GetBlogPostDto>>(blogPosts);

//            if (!blogPostDtos.Any())
//            {
//                return NotFound("No blog posts found.");
//            }
//            return Ok(blogPostDtos);
//        }

//        // GET: api/BlogPosts/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<GetBlogPostDto>> GetBlogPost(int id)
//        {
//            var blogPost = await _blogPostRepository.GetAsync(id);

//            if (blogPost == null)
//            {
//                return NotFound();
//            }

//            var blogPostDto = _mapper.Map<GetBlogPostDto>(blogPost);
//            return Ok(blogPostDto);
//        }

//        // POST: api/BlogPosts
//        [HttpPost]
//        public async Task<ActionResult<BlogPostDto>> PostBlogPost(CreateBlogPostDto createBlogPostDto)
//        {
//            var blogPost = _mapper.Map<BlogPost>(createBlogPostDto);
//            await _blogPostRepository.AddAsync(blogPost);

//            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
//            return CreatedAtAction("GetBlogPost", new { id = blogPostDto.Id }, blogPostDto);
//        }

//        // PUT: api/BlogPosts/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBlogPost(int id, UpdateBlogPostDto updateBlogPostDto)
//        {
//            if (id != updateBlogPostDto.Id)
//            {
//                return BadRequest("Mismatched BlogPost ID.");
//            }

//            var blogPost = await _blogPostRepository.GetAsync(id);
//            if (blogPost == null)
//            {
//                return NotFound();
//            }

//            _mapper.Map(updateBlogPostDto, blogPost);
//            await _blogPostRepository.UpdateAsync(blogPost);

//            return NoContent();
//        }

//        // DELETE: api/BlogPosts/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBlogPost(int id)
//        {
//            var blogPost = await _blogPostRepository.GetAsync(id);
//            return NoContent();
//        }
//    }
//}
