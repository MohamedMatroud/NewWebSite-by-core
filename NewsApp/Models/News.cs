using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public DateTime Date { get; set; }
        [DisplayName("Image Name")]
        public string Image { get; set; }
        public string Topic { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
