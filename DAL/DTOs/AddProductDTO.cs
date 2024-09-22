using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class AddProductDTO
    {
        [Required(ErrorMessage = "Product Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Required]
        [Range(10, 300, ErrorMessage = "Price must be between 10 and 300.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public string Category { get; set; }
    }
}
