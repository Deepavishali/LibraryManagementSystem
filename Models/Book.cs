using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book name is required")]
        [StringLength(100)]
        public string ? BookName { get; set; }

        [Required(ErrorMessage ="Author name is required")]
        [StringLength(50)]
        public string ? Author { get; set; }

        [Required(ErrorMessage ="Publisher Name is required")]
        [StringLength(50)]
        public string ? Publisher { get; set; }

        [Required(ErrorMessage ="Category is Required")]
        [StringLength(50)]
        public string ? Category { get; set; }

        [Required]
        [Range(0,10000,ErrorMessage ="Quantity must be atleast 0")]
        public int  Quantity { get; set; }

        [Required]
        [Range(0,10000,ErrorMessage ="Availlable copies must be atleast 0")]
        public int ? AvailableCopies { get; set; }

        [Required]
        [Range(0.01,100000.00,ErrorMessage ="Price must be greater than zero")]
        public double  ? Price { get; set; }

    }
}
