using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsService.Models
{
    public class News
    {
        [Key]
        public int newsId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string tag { get; set; }
        public string newsCity { get; set; }
        public DateTime datetime { get; set; }

        public virtual Author author { get; set; }
    }
}
