using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IHotel
    {
        public IEnumerable<Hotel> GetAll();
        public Hotel GetHotelById(int hotelId);

        public Hotel GetHotelDetailByNumber(string Number);
        public Hotel GetHotelDetailByEmail(String Email);

        public Messages InsertHotelDetail(Hotel hotelDetail);

        public Messages UpdateHotelDetail (Hotel hotelDetail);

        public Messages DeleteHotelDetail(int hotelDetailId);


        public IEnumerable<Hotel> GetHotelType(string hoteltype);
        //public IEnumerable<LFoodDetail> GetFoodDetail(int id);

        //public LoginDto loginbyid(string contactNumber, string password);

    }
}
