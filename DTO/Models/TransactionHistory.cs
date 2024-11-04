using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class TransactionHistory
    {
        [Key]
        public int TransactionHistoryId { get; set; }                              // Unique identifier for the transaction

        [ForeignKey("ExchangeRequest")]
        public int ExchangeRequestId { get; set; }               // Foreign key to the related ExchangeRequest

        [Column(TypeName = "nvarchar(50)")]
        public string StatusChange { get; set; }                 // Status change (e.g., Request Accepted, Request Rejected)

        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }                  // Timestamp of the transaction

        [Column(TypeName = "nvarchar(255)")]
        public string? Notes { get; set; }                        // Additional notes for the transaction
    }

}
