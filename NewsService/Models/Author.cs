using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsService.Models
{
    public class Author
    {
        [Key]
        public int authorId { get; set; }
        public string password { get; set; }
        public string authorName { get; set; }
        public string authorImage { get; set; }
        public string authorCity { get; set; }

    }
}
