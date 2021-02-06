using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        public string Description { get; set; }       
       
        }

}
    

