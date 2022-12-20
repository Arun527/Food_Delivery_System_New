using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ServiceStack.Messaging;
using System.Transactions;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery.RepositoryService
{
    public class HotelService: IHotel
    {
        private readonly FoodDeliveryDbContext db;
        public HotelService(FoodDeliveryDbContext obj)
        {
            db = obj;
        }
        public   IEnumerable<Hotel> GetAll()
        {
            Messages msg = new Messages();
            var hotel= db.Hotel.ToList();
            if(!hotel.Any())
            {
                msg.Success = false;
                msg.Message = "This hotel list is empty";
                msg.Status = Statuses.NotFound;
            }
                return hotel;
        }
        public Hotel GetHotelById(int hotelId)
        {
            return db.Hotel.FirstOrDefault(x=>x.HotelId==hotelId);
        }
        public Hotel GetHotelDetailByNumber(string Number)
        {
              var getId = db.Hotel.FirstOrDefault(x => x.ContactNumber == Number);
              return getId;  
        }
        public Hotel GetHotelDetailByEmail(String Email)
        {
                var getId = db.Hotel.FirstOrDefault(x => x.Email == Email);
                return getId;  
        }
        public IEnumerable<Hotel> GetHotelDetailByName(String hotelName)
        {
                return db.Hotel .Where(x => EF.Functions.Like(x.HotelName,$"{hotelName}%")).ToList();
        }
        public IEnumerable<FoodList> GetFoodByHotelName(string HotelName)
        {
            {
                var foodlist = (from food in db.Food
                                join hotel in db.Hotel on food.HotelId equals hotel.HotelId
                                where hotel.HotelName == HotelName 
                                select new FoodList
                                {
                                    HotelName = hotel.HotelName,
                                    FoodName = food.FoodName,
                                    Price = food.Price,
                                    Type = food.Type,
                                }).ToList();
                return foodlist;
            }
        }
        public Messages InsertHotelDetail(Hotel hotelDetail)
        {
            Messages msg = new Messages();
            try
            {
                var hotel = db.Hotel.FirstOrDefault(x => x.ContactNumber == hotelDetail.ContactNumber);
                var email = db.Hotel.FirstOrDefault(x => x.Email == hotelDetail.Email);
              
                if(hotel == null && email==null)
                {
                    db.Add(hotelDetail);
                    db.SaveChanges();
                    msg.Success=true;
                    msg.Message = "The hotel added succesfully";
                    msg.Status= Statuses.Created;
                }
                else
                {
                    msg.Success = false;
                    msg.Status = Statuses.Conflict; 
                    var message = (hotel!=null) ? "The hotel contact number already exist" : "The hotel email id already exist";
                    msg.Message = message;
                }  
            }
            catch(Exception Ex)
            {
                msg.Message = Ex.Message;
                msg.Success=false;
            }
            return msg;
        }
        public Messages UpdateHotelDetail(Hotel hotelDetail)
        {
            Messages msg = new Messages();
            try
            {
                var hotel = db.Hotel.FirstOrDefault(x => x.HotelId == hotelDetail.HotelId);
                var updateEmail = db.Hotel.FirstOrDefault(x => x.Email == hotelDetail.Email);
                var updateNumber = db.Hotel.FirstOrDefault(x => x.ContactNumber == hotelDetail.ContactNumber);
                msg.Message = "The hotel id not registered";
                msg.Status = Statuses.NotFound;
                msg.Success = false;
                if (hotelDetail.HotelId == 0)
                {
                    msg.Message = "The hotel field is required";
                    msg.Status = Statuses.BadRequest;
                    msg.Success = false;
                    return msg;
                }
                if (hotel == null)
                {
                    msg.Message = "Hotel id is not found";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                else if (updateNumber != null && updateNumber.HotelId != hotel.HotelId)
                {
                    msg.Message = $"The ({hotelDetail.ContactNumber}), Phone number is already registered.";
                    msg.number = false;
                    msg.Status = Statuses.Conflict;
                    return msg;
                }
                else if (updateEmail != null && updateEmail.HotelId != hotel.HotelId)
                {
                    msg.Message = $"The ( {hotelDetail.Email}  ), Email id is already registered.";
                    msg.Status = Statuses.Conflict;
                    return msg;
                }
                else
                {
                    hotel.Email = hotelDetail.Email;
                    hotel.Type = hotelDetail.Type;
                    hotel.ContactNumber = hotelDetail.ContactNumber;
                    hotel.Address = hotelDetail.Address;
                    hotel.HotelName = hotelDetail.HotelName;
                    hotel.IsActive = hotelDetail.IsActive;
                    db.Update(hotel);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "The hotel updated succesfully";
                    msg.Status = Statuses.Success;
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                msg.Success = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        public Messages DeleteHotelDetail(int hotelDetail)
        {
            Messages msg = new Messages();
            try
            {
                var hotel = db.Hotel.FirstOrDefault(x => x.HotelId == hotelDetail);
                var food = db.Food.FirstOrDefault(x => x.HotelId == hotelDetail);
                if(hotel != null && food==null)
                {
                    db.Remove(hotel);
                    db.SaveChanges();
                    msg.Message = "The hotel id deleted succesfully";
                    msg.Success=true;
                    msg.Status = Statuses.Success;
                }
                else if(food != null)
                {
                    msg.Message = "This hotel already having food list, So Can't delete.";
                    msg.Success = false;
                    msg.Status = Statuses.BadRequest;
                }
                else
                {
                    msg.Message = "The hotel id not found";
                    msg.Success = false;
                    msg.Status = Statuses.NotFound;
                }
            }
            catch(Exception ex)
            {
                msg.Message = ex.Message;
            }
            return msg ;
        }
        public IEnumerable<Hotel>  GetHotelType(string hoteltype)
        {
           var  hotelType = db.Hotel.Where(x=>x.Type==hoteltype).ToList();
           return hotelType;
        }
    }
}
