using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<Blog> GetAll()
        {
            var blogs = _context.Blogs.Include(x => x.Comments).ToList();
            return blogs;
        }

        public Blog GetByID(int blogID)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _context.Blogs
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.ID == blogID);
            return blog ?? throw new InvalidOperationException($"Blog with ID: {blogID} not found.");
        }

        public Blog GetByTitle(string title)
        {
            if (title.IsNullOrEmpty())
                throw new ArgumentException($"Invalid blog title: {title}. Blog title can't be empty or null.");

            var blog = _context.Blogs
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.Title == title);
            return blog ?? throw new InvalidOperationException($"Blog with title: {title} not found.");
        }

        public void Insert(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public void Update(int blogID, Blog blog)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var existingBlog = _context.Blogs.Find(blogID) ?? throw new InvalidOperationException($"Blog with ID: {blogID} not found.");

            CopyProperties(blog, existingBlog);

            _context.Entry(existingBlog).State = EntityState.Modified;
        }

        public void Delete(int blogID)
        {
            if (blogID < 1)
                throw new ArgumentException($"Invalid blog ID: {blogID}. Blog ID must be greater than or equal to 1.");

            var blog = _context.Blogs.Find(blogID) ?? throw new InvalidOperationException($"Blog with ID: {blogID} not found.");
            _context.Blogs.Remove(blog);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private static void CopyProperties(object source, object destination)
        {
            Type type = source.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(destination, value);
                }
            }
        }
    }
}
