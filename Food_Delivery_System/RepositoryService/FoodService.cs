using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Delivery.RepositoryService
{
    public class FoodService : IFood
    {



        public FoodDeliveryDbContext db;

        public FoodService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            db = foodDeliveryDbContext;
        }

        public IEnumerable<Food> GetAll()
        {
            try
            {

                var food= db.Food.ToList();
                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Food GetCoverPhoto(string imageId)
        {
            var coverPhoto = db.Food.FirstOrDefault(x => x.ImageId == imageId);
            return coverPhoto;
        }


        public Food GetFoodTypeById(int foodTypeId)
        {
            try
            {
                var food = db.Food.FirstOrDefault(x => x.FoodId == foodTypeId);
                if (food != null)
                {
                    return food;
                }
                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Messages InsertFoodType(Food foodType)
        {
            Messages msg = new Messages();
         
            try
            {
                var hotel = db.Hotel.FirstOrDefault(x => x.HotelId == foodType.HotelId);
                if (hotel == null)
                {
                    db.Food.Add(foodType);
                    db.SaveChanges();
                    msg.Message = "The Food Type Inserted Succesfully";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "The Hotel Id Not Found";
                    msg.Success = true;
                }

            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
            }
            return msg;
        }


        public Messages UpdateFood(Food foodType)
        {
            Messages msg = new Messages();
            try
            {
                var food = db.Food.FirstOrDefault(x => x.FoodId == foodType.FoodId);
                var hotel=db.Hotel.FirstOrDefault(x=>x.HotelId == foodType.HotelId);
                if (food != null && hotel != null)
                {
                    food.FoodName=foodType.FoodName;
                    
                    food.Price = foodType.Price;
                  
                    db.Update(food);
                    db.SaveChanges();
                    msg.Message = "The Food  Is Updated Succesfully";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "Invalid FoodTypeId";
                    msg.Success = false;
                }

            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                msg.Success = true;
            }

            return msg;
        }


        public Messages DeleteFoodType(int foodId)
        {
            Messages msg = new Messages();
            try
            {

                var food = db.Food.FirstOrDefault(x => x.FoodId == foodId);
                var hotel = db.Hotel.FirstOrDefault(x => x.HotelId == food.HotelId);
                if (food != null)
                {
                    db.Food.Remove(food);
                    db.SaveChanges();
                    msg.Message = "The Food  Is Deleted Succesfully";
                    msg.Success = true;
                }
                else if(hotel != null)
                {
                    msg.Message = "The Food Id Is Not Deleted Because Order The Customer";
                    msg.Success = false;
                }

                else
                {
                    msg.Message = "The hotel Id Is Invalid";
                    msg.Success = false;
                }

            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                msg.Success = false;
            }
            return msg;
        }

        public IEnumerable<Food> GetFoodType(string foodtype)
        {

            var foodType = db.Food.Where(x => x.Type == foodtype).ToList();
            return foodType;

        }

        public IEnumerable<FoodDto> GetAllFood()
        {

            {
                var employeeleave = (from food in db.Food
                                     join hotel in db.Hotel on food.HotelId equals hotel.HotelId
                                     select new FoodDto
                                     {
                                         FoodId = food.FoodId,
                                         FoodName = food.FoodName,
                                         HotelId = hotel.HotelId,
                                         HotelName = hotel.HotelName,
                                         Price = food.Price,
                                         ImageId = food.ImageId,
                                         Location = hotel.Address,
                                         Type=hotel.Type

                                     }).ToList();
                return employeeleave;
            }
        }



        public IEnumerable<Food> GetFoodByName(String FoodName)
        {

            try
            {
                Messages message = new Messages();
                var getId = db.Food.Where(x => EF.Functions.Like(x.FoodName, $"%{FoodName}%")).ToList();
                return getId;
            }

            catch (Exception)   
            {
                throw;
            }


        }

        public IEnumerable<FoodList> GetFoodByHotelId(int hotelId)
        {
            
            {
                
                var foodlist = (from food in db.Food
                                     join hotel in db.Hotel on food.HotelId equals hotel.HotelId
                                     where hotel.HotelId == hotelId
                                      
                                     select new FoodList
                                     {
                                         HotelName= hotel.HotelName,
                                         HotelId= hotel.HotelId,
                                         FoodId = food.FoodId,
                                         FoodName = food.FoodName,
                                         ImageId= food.ImageId,
                                         Price = food.Price,
                                         Type = food.Type,
                                        
                                     }).ToList();
                return foodlist;
            }
        }






    }

}
