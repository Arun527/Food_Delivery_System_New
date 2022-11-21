using Food_Delivery.Models;


namespace Food_Delivery.RepositoryInterface
{
    public interface IFood
    {
        public IEnumerable<Food> GetAll();

        public Food GetFoodTypeById(int foodTypeId);

        public Messages InsertFoodType(Food foodType);

        public Messages UpdateFood(Food foodType);

        public Messages DeleteFoodType(int foodTypeId);
       
        public IEnumerable<FoodDto> GetAllFood();
        public IEnumerable<FoodDto> GetFoodByHotelId(int hotelId);
    }
}
