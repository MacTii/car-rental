using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        public IEnumerable<Blog> GetAll();
        public Blog? GetByID(int blogID);
        public Blog? GetByTitle(string title);
        public void Insert(Blog blog);
        public void Update(int blogID, Blog blog);
        public void Delete(int blogID);
        public void Save();
    }
}
