using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
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
        private readonly CommentValidator _validator;

        public CommentService(ILogger<CommentService> logger, IMapper mapper, ICommentRepository commentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _validator = new CommentValidator();
        }

        #endregion Injection

        public IEnumerable<CommentDTO> GetComments()
        {
            var comments = _commentRepository.GetAll();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public CommentDTO GetComment(int commentID)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var comment = _commentRepository.GetByID(commentID);
            if(comment == null)
                throw new InvalidOperationException($"Comment with ID: {commentID} not found.");

            return _mapper.Map<CommentDTO>(comment);
        }

        public void AddComment(CommentDTO commentDTO)
        {
            // Check if the input data is valid
            var validationResult = _validator.Validate(commentDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _commentRepository.Insert(_mapper.Map<Comment>(commentDTO));
            _commentRepository.Save();
        }

        public void UpdateComment(int commentID, CommentDTO commentDTO)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var comment = _commentRepository.GetByID(commentID);
            if (comment == null)
                throw new InvalidOperationException($"Comment with ID: {commentID} not found.");

            // Check if the input data is valid
            var validationResult = _validator.Validate(commentDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _commentRepository.Update(commentID, _mapper.Map<Comment>(commentDTO));
            _commentRepository.Save();
        }

        public void DeleteComment(int commentID)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var comment = _commentRepository.GetByID(commentID);
            if (comment == null)
                throw new InvalidOperationException($"Comment with ID: {commentID} not found.");

            _commentRepository.Delete(commentID);
            _commentRepository.Save();
        }
    }
}
