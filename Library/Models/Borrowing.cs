using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Library.Models
{
    public class Borrowing
    {

        [Key]
        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RentalDate { get; private set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; private set; }

        public BorrowingState State { get; private set; }
        [Required]
        public int BookID { get;  set; }
        [Required]
        public int BorrowerID { get;  set; }

        public  Book Book { get; set; }
        public  User Borrower { get; set; }


        public Borrowing()
        {
            RentalDate = DateTime.Now;
            State = BorrowingState.InProgrees;
        }

        public void EndBorrowing()
        {
            EndDate = DateTime.Now;
            State = BorrowingState.Closed;
        }

         

    }
    public enum BorrowingState
    {
        InProgrees,
        Closed
       
    }
}
