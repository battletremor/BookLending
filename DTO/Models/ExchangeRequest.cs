using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class ExchangeRequest
    {
        [Key]
        public int ExchangeRequestId { get; set; }                             // Unique identifier for the request

        [ForeignKey("Book")]
        public int BookId { get; set; }                         // Foreign key to the Book being exchanged

        [ForeignKey("User")]
        public int RequesterId { get; set; }                    // Foreign key to the User who initiated the request

        [ForeignKey("User")]
        public User Requester { get; set; }                     // The user making the request

        [Column(TypeName = "nvarchar(100)")]
        public string ExchangeTerms { get; set; }               // Terms of the exchange (e.g., delivery method, duration)

        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; }                      // Status of the request (e.g., Pending, Accepted, Rejected, Completed)

        [Column(TypeName = "datetime")]
        public DateTime RequestedAt { get; set; }               // Date when the request was made

        [Column(TypeName = "datetime")]
        public DateTime? CompletedAt { get; set; }              // Date when the exchange was completed, if applicable
    }

}
