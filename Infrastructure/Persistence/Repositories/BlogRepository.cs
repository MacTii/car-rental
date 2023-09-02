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

        public Blog? GetByID(int blogID)
        {
            var blog = _context.Blogs
                .Include(b => b.Comments)
                .SingleOrDefault(b => b.ID == blogID);

            return blog;
        }

        public Blog? GetByTitle(string title)
        {
            var blog = _context.Blogs
                .Include(b => b.Comments)
                .SingleOrDefault(b => b.Title == title);

            return blog;
        }

        public void Insert(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public void Update(int blogID, Blog blog)
        {
            var existingBlog = _context.Blogs.Single(x => x.ID == blogID);
            CopyProperties(blog, existingBlog);

            _context.Entry(existingBlog).State = EntityState.Modified;
        }

        public void Delete(int blogID)
        {
            var blog = _context.Blogs.Single(x=> x.ID == blogID);
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
