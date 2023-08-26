using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        #region Injection

        private readonly ILogger<CommentService> _logger;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentService(ILogger<CommentService> logger, IMapper mapper, ICommentRepository commentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        #endregion Injection

        public IEnumerable<CommentDTO> GetComments()
        {
            var comments = _commentRepository.GetAll();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public CommentDTO GetComment(int commentID)
        {
            var comment = _commentRepository.GetByID(commentID);
            return _mapper.Map<CommentDTO>(comment);
        }

        public void AddComment(CommentDTO commentDTO)
        {
            _commentRepository.Insert(_mapper.Map<Comment>(commentDTO));
            _commentRepository.Save();
        }

        public void UpdateComment(int commentID, CommentDTO commentDTO)
        {
            _commentRepository.Update(commentID, _mapper.Map<Comment>(commentDTO));
            _commentRepository.Save();
        }

        public void DeleteComment(int commentID)
        {
            _commentRepository.Delete(commentID);
            _commentRepository.Save();
        }
    }
}
