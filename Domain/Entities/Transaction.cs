using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Entities
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("guid")]
        public Guid Guid { get; set; }

        [Column("transaction_date")]
        public DateTime? TransactionDate { get; set; }

        [Column("transaction_type")]
        public char? TransactionType { get; set; }

        [Column("amount")]
        public decimal? Amount { get; set; }

        [Column("remarks")]
        public string? Remarks { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("remainder")]
        public DateTime? Remainder { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("currency_id")]
        public int? CurrencyId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("deleted_by")]
        public string? DeletedBy { get; set; }
    }
}
