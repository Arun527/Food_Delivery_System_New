using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.EntityFrameworkCore;
using static Food_Delivery.Models.Messages;

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
                msg.Message = "The hotel id not found";
                msg.Success = false;
                msg.Status = Statuses.NotFound;
                var hotel = db.Hotel.FirstOrDefault(x => x.HotelId == foodType.HotelId);
                if (foodType.HotelId != 0)
                {
                    if (hotel == null)
                    {
                        db.Food.Add(foodType);
                        db.SaveChanges();
                        msg.Message = "The food type inserted succesfully";
                        msg.Success = true;
                        msg.Status = Statuses.Success;
                    }
                    return msg;
                }
                else
                {
                    msg.Message = "The hotel id field is required";
                    msg.Success = false;
                    msg.Status = Statuses.BadRequest;
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
                var foodId = db.Food.FirstOrDefault(x => x.FoodId == foodType.FoodId);
                var hotelId= db.Hotel.FirstOrDefault(x=>x.HotelId == foodType.HotelId);
                if (foodId != null && hotelId!= null)
                {
                    foodId.FoodName=foodType.FoodName;
                    foodId.Price = foodType.Price;
                    db.Update(foodId);
                    db.SaveChanges();
                    msg.Message = "The food  is updated succesfully";
                    msg.Success = true;
                    msg.Status = Statuses.Success;
                }
                else
                {
                    if (foodId == null)
                    {
                        msg.Message = "The food id not found";
                        msg.Success = false;
                        msg.Status = Statuses.NotFound;
                    }
                    else
                    {
                        msg.Message = "The hotel id not found";
                        msg.Success = false;
                        msg.Status = Statuses.NotFound;
                    }
                }
                return msg;
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
                if (food != null)
                {
                    var hotel = food.HotelId.Value;
                    if (food != null && hotel == null)
                    {
                        db.Food.Remove(food);
                        db.SaveChanges();
                        msg.Message = "The food  is deleted succesfully";
                        msg.Success = true;
                        msg.Status = Statuses.Success;
                    }
                    else if (hotel != null)
                    {
                        msg.Message = "The food id is not deleted because order the customer";
                        msg.Success = false;
                        msg.Status = Statuses.BadRequest;
                    }
                }
                else
                {
                    msg.Message = "The food id not found";
                    msg.Success = false;
                    msg.Status = Statuses.NotFound;
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
                Messages message = new Messages();
                var getId = db.Food.Where(x => EF.Functions.Like(x.FoodName, $"%{FoodName}%")).ToList();
                return getId;
        }
        public IEnumerable<FoodList> GetFoodByHotelId(int hotelId)
        {
            {
                    var foodlist = (from food in db.Food
                                    join hotel in db.Hotel on food.HotelId equals hotel.HotelId
                                    where hotel.HotelId == hotelId
                                    select new FoodList
                                    {
                                        HotelName = hotel.HotelName,
                                        HotelId = hotel.HotelId,
                                        FoodId = food.FoodId,
                                        FoodName = food.FoodName,
                                        ImageId = food.ImageId,
                                        Price = food.Price,
                                        Type = food.Type,
                                    }).ToList();
                    return foodlist;
            }
        }
    }
}
