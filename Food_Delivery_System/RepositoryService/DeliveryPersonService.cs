using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using ServiceStack.Messaging;

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
                var delivery=db.DeliveryPerson.FirstOrDefault(x=>x.ContactNumber==deliveryPerson.ContactNumber);
                msg.Success = false;
                msg.Message = "This DeliveryPerson Already Exists";
                if (deliveryPersonId == null)
                {
                    if (delivery != null)
                    {
                        var contactnimber = db.DeliveryPerson.FirstOrDefault(x => x.ContactNumber == deliveryPerson.ContactNumber);
                        var id = deliveryPerson.DeliveryPersonId;
                        if(id !=null && deliveryPerson.DeliveryPersonId != null)
                        {
                            msg.Success = false;
                            msg.Message = "The Contact Number Already taked";
                            return msg;
                        }
                        else
                        {
                            db.Add(deliveryPerson);
                            db.SaveChanges();
                            msg.Success = true;
                            msg.Message = "DeliveryPerson Added Succesfully";
                            return msg;

                        }

                    }


                    else
                    {
                        db.Add(deliveryPerson);
                        db.SaveChanges();
                        msg.Success = true;
                        msg.Message = "DeliveryPerson Added Succesfully";
                        return msg;
                    }
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
                msg.Message = "This DeliveryPersonId not registered";
                var updateDeliveryPerson = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == deliveryPerson.DeliveryPersonId);

                if (updateDeliveryPerson != null )
                {
                    updateDeliveryPerson.DeliveryPersonName = deliveryPerson.DeliveryPersonName;
                    updateDeliveryPerson.Gender= deliveryPerson.Gender;
                    db.Update(updateDeliveryPerson);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "DeliveryPerson Updated Succesfully!!";
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
            if (deleteDeliveryPerson != null)
            {
                db.Remove(deleteDeliveryPerson);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "DeliveryPerson Deleted Succesfully";
            }
            return msg;
        }
    }
}
