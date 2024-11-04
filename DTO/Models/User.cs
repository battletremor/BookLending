using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }                 // Unique identifier for the user

        [Column(TypeName = "nvarchar(255)")]
        public string UserName { get; set; }            // Username for display and login

        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }               // User's email address

        [Column(TypeName = "nvarchar(255)")]
        public string PasswordHash { get; set; }        // Hashed password for authentication

        [Column(TypeName = "nvarchar(255)")]
        public string FullName { get; set; }            // Full name of the user

        [Column(TypeName = "nvarchar(255)")]
        public string? FavoriteGenres { get; set; }      // Comma-separated list of favorite genres (e.g., "Fiction, Sci-Fi")

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }         // Account creation date

        [ForeignKey("UserId")]
        public ICollection<Book> Books { get; set; }    // Collection of books owned by the user
    }

}
