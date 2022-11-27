using Food_Delivery.Models;
using Food_Delivery_System.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IHotel
    {
        public IEnumerable<Hotel> GetAll();
        public Hotel GetHotelById(int hotelId);

        public Hotel GetHotelDetailByNumber(string Number);
        public Hotel GetHotelDetailByEmail(string Email);

        public IEnumerable<FoodList> GetFoodByHotelName(string HotelName);
        public Messages InsertHotelDetail(Hotel hotelDetail);

        public Messages UpdateHotelDetail (Hotel hotelDetail);

        public Messages DeleteHotelDetail(int hotelDetailId);

        public IEnumerable<Hotel> GetHotelDetailByName(String hotelName);
        public IEnumerable<Hotel> GetHotelType(string hoteltype);
        //public IEnumerable<LFoodDetail> GetFoodDetail(int id);

        //public LoginDto loginbyid(string contactNumber, string password);

    }
}
