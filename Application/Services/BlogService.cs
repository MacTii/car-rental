using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        #region Injection

        private readonly ILogger<BlogService> _logger;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public BlogService(ILogger<BlogService> logger, IMapper mapper, IBlogRepository blogRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        #endregion Injection

        public IEnumerable<BlogDTO> GetBlogs()
        {
            var blogs = _blogRepository.GetAll();
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }

        public BlogDTO GetBlogByID(int blogID)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _blogRepository.GetByID(blogID);
            if (blog == null)
                throw new InvalidOperationException($"Blog with ID: {blogID} not found.");

            return _mapper.Map<BlogDTO>(blog);
        }

        public BlogDTO GetBlogByTitle(string title)
        {
            var blog = _blogRepository.GetByTitle(title);
            if (blog == null)
                throw new InvalidOperationException($"Blog with title: {title} not found.");

            return _mapper.Map<BlogDTO>(blog);
        }

        public void AddBlog(BlogDTO blogDTO)
        {
            _blogRepository.Insert(_mapper.Map<Blog>(blogDTO));
            _blogRepository.Save();
        }

        public void UpdateBlog(int blogID, BlogDTO blogDTO)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _blogRepository.GetByID(blogID);
            if (blog == null)
                throw new InvalidOperationException($"Blog with ID: {blogID} not found.");

            _blogRepository.Update(blogID, _mapper.Map<Blog>(blogDTO));
            _blogRepository.Save();
        }

        public void DeleteBlog(int blogID)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _blogRepository.GetByID(blogID);
            if (blog == null)
                throw new InvalidOperationException($"Blog with ID: {blogID} not found.");

            _blogRepository.Delete(blogID);
            _blogRepository.Save();
        }

        public BlogDTO UploadImage(IFormFile formFile, int blogID)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _blogRepository.GetByID(blogID);
            if (blog == null)
                throw new InvalidOperationException($"Blog with ID: {blogID} not found.");

            using (MemoryStream memoryStream = new())
            {
                formFile.CopyTo(memoryStream);
                blog.Image = memoryStream.ToArray();
            }

            _blogRepository.Update(blogID, blog);
            _blogRepository.Save();

            return _mapper.Map<BlogDTO>(blog);
        }
    }
}
