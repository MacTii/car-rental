using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")] // Remove /Comment from URL route
    [ApiController]
    public class CommentController : ControllerBase
    {
        #region Injection

        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        #endregion Injection

        [HttpGet("comments")]
        // [Authorize]
        public ActionResult GetComments()
        {
            var commentDTOs = _commentService.GetComments();
            if (!commentDTOs.Any())
                return NoContent(); // Returns HTTP 204 if there are no records

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Comment records retrieved successfully",
                    Data = commentDTOs
                });
        }

        [HttpGet("comments/{commentID}")]
        [Authorize]
        public ActionResult GetCommentByID(int commentID)
        {
            var commentDTO = _commentService.GetComment(commentID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Comment record retrieved successfully",
                    Data = commentDTO
                });
        }

        [HttpPost("comments")]
        // [Authorize]
        public ActionResult AddComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _commentService.AddComment(commentDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Comment record created successfully",
                    Data = commentDTO
                });
        }

        [HttpPut("comments/{commentID}")]
        [Authorize]
        public ActionResult UpdateComment(int commentID, CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _commentService.UpdateComment(commentID, commentDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Comment record updated successfully",
                    Data = commentDTO
                });
        }

        [HttpDelete("comments/{commentID}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComment(int commentID)
        {
            _commentService.DeleteComment(commentID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Comment record deleted successfully"
                });
        }
    }
}
