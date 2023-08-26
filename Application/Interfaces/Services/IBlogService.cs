using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBlogService
    {
        public IEnumerable<BlogDTO> GetBlogs();
        public BlogDTO GetBlogByID(int blogID);
        public BlogDTO GetBlogByTitle(string title);
        public void AddBlog(BlogDTO blogDTO);
        public void UpdateBlog(int blogID, BlogDTO blogDTO);
        public void DeleteBlog(int blogID);
        public BlogDTO UploadImage(IFormFile formFile, int blogID);
    }
}
