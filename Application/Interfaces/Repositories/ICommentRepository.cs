using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAll();
        public Comment? GetByID(int commentID);
        public void Insert(Comment comment);
        public void Update(int commentID, Comment comment);
        public void Delete(int commentID);
        public void Save();
    }
}
