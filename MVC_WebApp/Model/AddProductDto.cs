using System.ComponentModel.DataAnnotations;

namespace MVC_WebApp.Model
{
    public class AddProductDto
    {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
        }
}
