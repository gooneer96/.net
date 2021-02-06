using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;



namespace Library.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }

        public string Login { get; set; }

        [DataType(DataType.Password)]
        [PasswordPropertyText(true)]        
        public string Password { get; set; }
    }
}
