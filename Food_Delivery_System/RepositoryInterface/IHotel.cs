using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IHotel
    {
        public IEnumerable<Hotel> GetAll();
        public Hotel GetHotelById(int hotelId);

      

        public Messages InsertHotelDetail(Hotel hotelDetail);

        public Messages UpdateHotelDetail (Hotel hotelDetail);

        public Messages DeleteHotelDetail(int hotelDetailId);
        public IEnumerable<LFoodDetail> GetFoodDetail(int id);
    
        public LoginDto loginbyid(string contactNumber, string password);

    }
}
