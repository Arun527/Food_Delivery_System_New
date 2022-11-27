using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ServiceStack.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using System.Text.Json.Serialization;

namespace Food_Delivery.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public virtual int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public Customer? Customer { get; set; }
        public DateTime OrderdateTime { get; set; } = DateTime.Now;

      
    }
}
