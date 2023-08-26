using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string AuthorSurname { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DetailedDescription { get; set; } = null!;
        public DateTime Date { get; set; }
        public byte[] Image { get; set; } = null!;
        public List<Comment>? Comments { get; set; }
    }
}
