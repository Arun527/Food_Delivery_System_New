using Food_Delivery.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace Food_Delivery_System.Models
{
    public class OrderShipmentDto
    {
        public int OrderShipmentDetailId { get; set; }

        public int DeliveryPersonId { get; set; }

        
       
        public int OrderDetailId { get; set; }

        public List<SelectListItem>? DeliveryList { get; set; }

        public List<SelectListItem>? ShipmentList { get; set; }
    }
}
