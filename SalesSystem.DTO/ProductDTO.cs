﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string? Name { get; set; }
        public int? IdCategorie { get; set; }
        public int? CategoryDescription { get; set; }
        public int? Stock { get; set; }
        public string? Price { get; set; }
        public int? IsActive { get; set; }
    }
}