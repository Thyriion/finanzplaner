using FiTE.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiTE.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public Decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public String Purpose { get; set; } = string.Empty;
        public Boolean Recurring { get; set; }
        public Transactionstype Type { get; set; }
        public ApplicationUser User { get; set; }
    }
}
