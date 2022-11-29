using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery_System.Models
{
    public class OrderDto
    {
        public int FoodId { get; set; }

        public int CustomerId { get; set; }


        public List<SelectListItem> CustomerList { get; set; }




    }
}
