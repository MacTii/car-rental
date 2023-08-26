using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")] // Remove /Blog from URL route
    [ApiController]
    public class BlogController : ControllerBase
    {
        #region Injection

        private readonly ILogger<BlogController> _logger;
        private readonly IBlogService _blogService;

        public BlogController(ILogger<BlogController> logger, IBlogService blogService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
        }

        #endregion Injection

        [HttpGet("blogs")]
        // [Authorize]
        public ActionResult GetBlogs()
        {
            var blogDTOs = _blogService.GetBlogs();
            if (!blogDTOs.Any())
                return NoContent(); // Returns HTTP 204 if there are no records

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog records retrieved successfully",
                    Data = blogDTOs
                });
        }

        [HttpGet("blogs/id/{blogID}")]
        // [Authorize]
        public ActionResult GetBlogByID(int blogID)
        {
            var blogDTO = _blogService.GetBlogByID(blogID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog record retrieved successfully",
                    Data = blogDTO
                });
        }

        [HttpGet("blogs/title/{title}")]
        // [Authorize]
        public ActionResult GetBlogByTitle(string title)
        {
            var blogDTO = _blogService.GetBlogByTitle(title);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog record retrieved successfully",
                    Data = blogDTO
                });
        }

        [HttpPost("blogs")]
        [Authorize]
        public ActionResult AddBlog(BlogDTO blogDTO)
        {
            if (blogDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _blogService.AddBlog(blogDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog record created successfully",
                    Data = blogDTO
                });
        }

        [HttpPut("blogs/{blogID}")]
        [Authorize]
        public ActionResult UpdateBlog(int blogID, BlogDTO blogDTO)
        {
            if (blogDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _blogService.UpdateBlog(blogID, blogDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog record updated successfully",
                    Data = blogDTO
                });
        }

        [HttpDelete("blogs/{blogID}")]
        [Authorize]
        public ActionResult DeleteBlog(int blogID)
        {
            _blogService.DeleteBlog(blogID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Blog record deleted successfully"
                });
        }

        [HttpPut("blogs/upload-image/{blogID}")]
        [Authorize]
        public ActionResult UploadImage(IFormFile formFile, int blogID)
        {
            var blogDTO = _blogService.UploadImage(formFile, blogID);

            // HTTP 200
            return Ok(new
            {
                Response = "Blog image uploaded successfully",
                Data = blogDTO
            });
        }
    }
}
