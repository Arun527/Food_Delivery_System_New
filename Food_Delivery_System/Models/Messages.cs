namespace Food_Delivery.Models
{
    public class Messages
    {

        public string Message { get; set; } = string.Empty;


        public  Statuses Status { get; set; }

        public bool Success { get; set; }

      
        public enum Statuses
        {
           Success,
           Created,
           BadRequest,
           NotFound,
           Conflict
        }


    }
}
