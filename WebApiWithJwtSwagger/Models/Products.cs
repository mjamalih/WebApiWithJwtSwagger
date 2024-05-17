using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiWithJwtSwagger.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Category { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockQty { get; set; }

    }
}
