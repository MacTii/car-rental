using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<Comment> GetAll()
        {
            var comments = _context.Comments.ToList();
            return comments;
        }

        public Comment GetByID(int commentID)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var comment = _context.Comments.Find(commentID);
            return comment ?? throw new InvalidOperationException($"Comment with ID: {commentID} not found.");
        }

        public void Insert(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void Update(int commentID, Comment comment)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var existingComment = _context.Comments.Find(commentID) ?? throw new InvalidOperationException($"Comment with ID: {commentID} not found.");

            CopyProperties(comment, existingComment);

            _context.Entry(existingComment).State = EntityState.Modified;
        }

        public void Delete(int commentID)
        {
            if (commentID < 1)
                throw new ArgumentException($"Invalid comment ID: {commentID}. Comment ID must be greater than or equal to 1.");

            var comment = _context.Comments.Find(commentID) ?? throw new InvalidOperationException($"Comment with ID: {commentID} not found.");
            _context.Comments.Remove(comment);
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
