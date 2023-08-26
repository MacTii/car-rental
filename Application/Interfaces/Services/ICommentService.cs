using Application.Mapper.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        public IEnumerable<CommentDTO> GetComments();
        public CommentDTO GetComment(int commentID);
        public void AddComment(CommentDTO commentDTO);
        public void UpdateComment(int commentID, CommentDTO commentDTO);
        public void DeleteComment(int commentID);
    }
}
