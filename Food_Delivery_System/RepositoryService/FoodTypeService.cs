//using Food_Delivery.Models;
//using Food_Delivery.RepositoryInterface;
//using ServiceStack.Messaging;

//namespace Food_Delivery.RepositoryService
//{

   
//    public class FoodTypeService: IFoodType
//    {
//        private readonly  FoodDeliveryDbContext db;

//        public FoodTypeService(FoodDeliveryDbContext foodDeliveryDbContext)
//        {
//            db = foodDeliveryDbContext;
//        }

//        //public IEnumerable<FoodType> GetAll()
//        //{
//        //    try
//        //    {
//        //        return db.FoodType.ToList();
//        //    }
//        //    catch(Exception)
//        //    {
//        //        throw;
//        //    }
//        //}

//        public FoodType GetById(int id)
//        {
//            try
//            {
//                var foodType = db.FoodType.FirstOrDefault(x => x.FoodTypeId == id);
//                return foodType;
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }


//        public Messages InsertFoodType(FoodType foodType)
//        {
//            Messages msg=new Messages();
//            try
//            {
//                var hotel=db.FoodType.FirstOrDefault(x=>x.FoodTypeId == foodType.FoodTypeId);
//                if (hotel == null)
//                {

//                    db.FoodType.Add(foodType);
//                    db.SaveChanges();
//                    msg.Message = "The Food Type Inserted Succesfully";
//                    msg.Success = true;

//                }
//                else
//                {
//                    msg.Message = "The Food Type Inserted Succesfully";
//                    msg.Success = true;
//                }
               
//            }
//            catch(Exception ex)
//            {
//               msg.Message=ex.Message;
//            }
//            return msg;
//        }

//        public Messages UpdateFoodType(FoodType foodType)
//        {
//            Messages msg = new Messages();
//            try
//            {
//              var hotel = db.FoodType.FirstOrDefault(x => x.FoodTypeId == foodType.FoodTypeId);
//               if(hotel != null)
//                  {
//                     db.FoodType.Update(foodType);
//                     db.SaveChanges();
//                     msg.Message = "The Food Type Is Updated Succesfully";
//                     msg.Success=true;
//                  }
//               else
//                  {
//                     msg.Message = "Invalid FoodTypeId";
//                     msg.Success = false;
//                  }

//            }
//            catch(Exception ex)
//            {
//                msg.Message = ex.Message;
//                msg.Success = true; 
//            }

//            return msg;
//        }

//        public Messages DeleteFoodType(int foodTypeId)
//        {
//            Messages msg = new Messages();
//            try
//            {
//                var type=db.FoodType.FirstOrDefault(x => x.FoodTypeId == foodTypeId);
//                if (type != null)
//                {
//                    db.FoodType.Remove(type);
//                    db.SaveChanges();
//                    msg.Message = "The Food Type Is Deleted Succesfully";
//                    msg.Success = true;
//                }
//                else
//                {
//                    msg.Message = "The hotel Id Is Invalid";
//                    msg.Success = false;
//                }
              
//            }
//            catch(Exception ex)
//            {
//                msg.Message=ex.Message;
//                msg.Success=false;
//            }
//            return msg;
//        }
//    }
//}
