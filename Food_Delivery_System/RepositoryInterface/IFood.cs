using Food_Delivery.Models;
using Food_Delivery_System.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IFood
    {
        public IEnumerable<Food> GetAll();

        public Food GetFoodTypeById(int foodTypeId);

        public Messages InsertFoodType(Food foodType);

        public Messages UpdateFood(Food foodType);

        public Messages DeleteFoodType(int foodTypeId);

        public IEnumerable<Food> GetFoodType(string foodtype);
        public IEnumerable<FoodDto> GetAllFood();
        public Food GetCoverPhoto(string imageid);
        public IEnumerable<FoodList> GetFoodByHotelId(int hotelId);
    }
}
