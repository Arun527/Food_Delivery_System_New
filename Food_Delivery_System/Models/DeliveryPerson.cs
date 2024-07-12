
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;

namespace Food_Delivery.Models
{
    public class DeliveryPerson
    {
        [Key]
        public int DeliveryPersonId { get; set; }

        public string? DeliveryPersonName { get; set; }
      

        public string? VechileNo { get; set; }
        public string? ContactNumber { get; set; }


        public string? Gender { get; set; }

        
        public DateTime CreatedOn { get; set; }=DateTime.Now;

        public bool IsActive { get; set; }=true;

      

    }
}
