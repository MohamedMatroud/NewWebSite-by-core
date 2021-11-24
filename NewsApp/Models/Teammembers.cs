using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class Teammembers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        [DisplayName("Image Name")]
        public string Image { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile TeameImage { get; set; }
    }
}
