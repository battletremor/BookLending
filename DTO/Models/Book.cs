using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }                     // Unique identifier for the book

        [Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }               // Book title

        [Column(TypeName = "nvarchar(255)")]
        public string Author { get; set; }              // Book author

        [Column(TypeName = "nvarchar(100)")]
        public string Genre { get; set; }               // Genre of the book (e.g., Fiction, Non-fiction)

        [Column(TypeName = "nvarchar(50)")]
        public string Condition { get; set; }           // Condition of the book (e.g., New, Used, Damaged)
        
        [Column(TypeName = "bit")]
        public bool IsAvailable { get; set; }           // Availability status (true if available for exchange)
        
        [ForeignKey("User")]
        public int UserId { get; set; }                 // Foreign key to the User model

        [Column(TypeName = "datetime")]
        public DateTime ListedAt { get; set; }          // Date when the book was listed
    }

}
