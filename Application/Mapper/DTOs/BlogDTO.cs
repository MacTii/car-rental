using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class BlogDTO
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string AuthorSurname { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DetailedDescription { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Image { get; set; } = null!; // base64 string
        public List<Comment>? Comments { get; set; }
    }
}
