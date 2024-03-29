﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class CommentDTO
    {
        public int ID { get; set; }
        public int BlogID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
