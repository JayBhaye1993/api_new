using BlogMgtAPI.Models;
using BlogMgtAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace BlogMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogManagement : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogManagement(IBlogService blogService)
        {
            _blogService = blogService;
        }
        // GET: api/<BlogManagement>
        [HttpGet]
        public async Task<IEnumerable<BlogModel>> Get()
        {
            return await _blogService.GetBlog();

        }

        // GET api/<BlogManagement>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            BlogModel response =  await _blogService.GetBlogById(id);
            return Ok(response);
        }

        // POST api/<BlogManagement>
        [HttpPost]
        public ActionResult<BlogModel> Post([FromBody] BlogModel blogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BlogModel response = _blogService.PostBlog(blogModel);

            return CreatedAtAction(nameof(Get), new { id = blogModel.Id }, response);

        }

        // PUT api/<BlogManagement>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BlogModel blogModel)
        {
            BlogModel response = await _blogService.UpdateBlogById(blogModel);

            return Ok(response);
        }

        // DELETE api/<BlogManagement>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            IEnumerable<BlogModel> response =  _blogService.DeleteBlog(id);

            return Ok(response);
        }
    }
}
