using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookCategory
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public int BookID { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public  Book Book { get; set; }
        public  Category Category { get; set; }
    }
}
