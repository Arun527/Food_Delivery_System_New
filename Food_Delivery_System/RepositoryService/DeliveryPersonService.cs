using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using ServiceStack.Messaging;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery.RepositoryService
{
    public class DeliveryPersonService : IDeliveryPerson
    {
        private readonly FoodDeliveryDbContext db;
        public DeliveryPersonService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<DeliveryPerson> GetAllDeliveryPersons()
        {
            return db.DeliveryPerson.ToList();
        }

        public DeliveryPerson GetDeliveryPerson(int deliveryPersonId)
        {
            var getId = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == deliveryPersonId);
            return getId;
        }

        public DeliveryPerson GetDeliveryPersonByNumber(string Number)
        {
            try
            {     
                var getId = db.DeliveryPerson.FirstOrDefault(x => x.ContactNumber == Number);
                return getId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Messages InsertDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            Messages msg = new Messages();
            try
            {
                var deliveryPersonId = db.DeliveryPerson.FirstOrDefault(x => x.ContactNumber == deliveryPerson.ContactNumber);
                msg.Success = false;
                msg.Message = "This delivery person already exists";
                msg.Status = Statuses.Conflict;
                if (deliveryPersonId == null)
                {
                        db.Add(deliveryPerson);
                        db.SaveChanges();
                        msg.Success = true;
                        msg.Message = "Delivery person added succesfully";
                        msg.Status= Statuses.Created;
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }

        public Messages UpdateDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This contact number is already exists";
                msg.Status = Statuses.NotFound;
                var updateDeliveryPerson = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == deliveryPerson.DeliveryPersonId);
                var number = db.DeliveryPerson.FirstOrDefault(x => x.ContactNumber == deliveryPerson.ContactNumber);

                if (updateDeliveryPerson != null )
                {
                    
                        updateDeliveryPerson.DeliveryPersonName = deliveryPerson.DeliveryPersonName;
                        updateDeliveryPerson.Gender = deliveryPerson.Gender;
                        updateDeliveryPerson.VechileNo = deliveryPerson.VechileNo;
                        db.Update(updateDeliveryPerson);
                        db.SaveChanges();
                        msg.Success = true;
                        msg.Message = "Delivery person updated succesfully!!";
                        return msg;

                }
               else 
                {
                    msg.Success = false;
                    msg.Message = "Contact number already exist!!";
                    msg.number = false;
                    return msg;
                }

               return msg;
                
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
         
        }

        public Messages DeleteDeliveryPerson(int deliveryPersonId)
        {
            Messages msg = new Messages();
            var deleteDeliveryPerson = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == deliveryPersonId);
            if(deliveryPersonId ==0)
            {
                msg.Success = false;
                msg.Message = "Delivery Person deleted succesfully";
                msg.Status = Statuses.BadRequest;
            }
            if (deleteDeliveryPerson != null)
            {
                db.Remove(deleteDeliveryPerson);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Delivery Person deleted succesfully";
                msg.Status=Statuses.Success;
            }
            else
            {
                msg.Success=false;
                msg.Message = "This delivery person id is not found";
                msg.Status = Statuses.NotFound;
            }
            return msg;
        }
    }
}
