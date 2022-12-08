using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Messaging;
using System.Transactions;

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
            try
            {
                var hotel= db.Hotel.ToList();
                return hotel;
            }

            catch(Exception)
            {
                throw;
            }
           
        
           
        }
        public Hotel GetHotelById(int hotelId)
        {


            var hotel = db.Hotel.Find( hotelId);
            return hotel;
         
       
        }
        public Hotel GetHotelDetailByNumber(string Number)
        {

            try
            {
                Message message = new Message();
                var getId = db.Hotel.FirstOrDefault(x => x.ContactNumber == Number);
                return getId;
            }

            catch (Exception)
            {
                throw;
            }


        }



        public Hotel GetHotelDetailByEmail(String Email)
        {

            try
            {
                Message message = new Message();
                var getId = db.Hotel.FirstOrDefault(x => x.Email == Email);
                return getId;
            }

            catch (Exception)
            {
                throw;
            }


        }

        public IEnumerable<Hotel> GetHotelDetailByName(String hotelName)
        {

            try
            {
                Message message = new Message();
                var getId = db.Hotel.Where(x => EF.Functions.Like(x.HotelName,$"{hotelName}%")).ToList();
                return getId;
            }

            catch (Exception)
            {
                throw;
            }


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
                msg.Success = true;
                if(hotel == null)
                {
                    db.Add(hotelDetail);
                    db.SaveChanges();
                    msg.Success=true;
                    msg.Message = "The Hotel Added Succesfully";
                }
                else
                {
                    msg.Success=false;
                    msg.Message = "The Hotel Already Exist";
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
               var hotel=db.Hotel.FirstOrDefault(x => x.HotelId == hotelDetail.HotelId);
                var updateEmail= db.Hotel.FirstOrDefault(x=>x.Email==hotelDetail.Email);
                var updateNumber = db.Hotel.FirstOrDefault(x => x.ContactNumber == hotelDetail.ContactNumber);
                msg.Message = "The hotel Id Not Registered";
                msg.Success = false;
                if (hotel != null)
                {
                        if (updateEmail!=null && updateNumber!=null)
                        {
                              var hotelEmail = db.Hotel.FirstOrDefault(x => x.Email == hotelDetail.Email);
                              var id = hotelEmail.HotelId;
                              var hotelnum = db.Hotel.FirstOrDefault(x => x.ContactNumber == hotelDetail.ContactNumber);
                              var number = hotelnum.HotelId;

                           if (id!=null && hotelDetail.HotelId!=id)
                           {
                                 msg.Success = false;
                                 msg.Message = "This Email Id Already taked";
                                 return msg;
                           }
                        if (number != null && hotelDetail.HotelId != number)
                        {
                            msg.Success = false;
                            msg.Message = "This Contact Number  Already taked";
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
                            return msg;
                           }
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
                        return msg;
                     }

               }
               return msg;
            }
            catch(Exception ex)
            {
                msg.Message = ex.Message;
                msg.Success = false;
                msg.Message=ex.Message;
               
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
                    msg.Message = "The hotel Id deleted Succesfully";
                    msg.Success=true;
                }
                else if(food != null)
                {
                    msg.Message = "The hotel Food Is Available For Users";
                    msg.Success = false;
                }

                else
                {
                    msg.Message = "The hotel Id Is Invalid";
                    msg.Success = false;
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












        //public IEnumerable<LFoodDetail> GetFoodDetail(int hotelId)
        //{

        //    {
        //        var employeeleave = (from Food in db.Food
        //                             join Hotel in db.Hotel on Food.HotelId equals Hotel.HotelId
        //                             where Hotel.HotelId == hotelId
        //                             select new LFoodDetail
        //                             {
        //                                 HotelId = Hotel.HotelId,
        //                                 FoodId = Food.FoodId,
        //                                 FoodName = Food.FoodName,
        //                                 Price = Food.Price
        //                             }).ToList();
        //        return employeeleave;
        //    }
        //}


        //public LoginDto loginbyid(string contactNumber, string password)
        //{
        //    var result = (from Role in db.Role
        //                  join Hotel in db.Hotel on Role.RoleId equals Hotel.RoleId
        //                  where Hotel.ContactNumber == contactNumber && Hotel.Password == password

        //                  select new LoginDto()
        //                  {
        //                      Password = Hotel.Password,
        //                      ContactNumber = Hotel.ContactNumber,
        //                      RoleId = Hotel.RoleId,
        //                      HotelId = Hotel.HotelId,

        //                      RoleNmae = Role.RoleName
        //                  }).FirstOrDefault();
        //    return result;
        //}


    }
}
