using System;
using System.Collections.Generic;
using System.Text;

namespace Practica4.Models.DTOs.Requests
{   public class ProductForCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
