
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;

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


        public Messages InsertDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            Messages msg = new Messages();
            try
            {
                var deliveryPersonId = db.DeliveryPerson.FirstOrDefault(x => x.ContactNumber == deliveryPerson.ContactNumber);
                msg.Success = false;
                msg.Message = "This DeliveryPerson Already Exists";
                if (deliveryPersonId == null)
                {
                    db.Add(deliveryPerson);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "DeliveryPerson Added Succesfully";
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


        public Messages UpdateDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This DeliveryPersonId not registered";
                var updateDeliveryPerson = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == deliveryPerson.DeliveryPersonId);
                if (updateDeliveryPerson != null)
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

        public LoginDto loginbyid(string contactNumber, string password)
        {
            var result = (from Role in db.Role
                        
                          join DeliveryPerson in db.DeliveryPerson on Role.RoleId equals DeliveryPerson.RoleId
                          where DeliveryPerson.ContactNumber == contactNumber && DeliveryPerson.Password == password
                          select new LoginDto()
                          {
                              Password = DeliveryPerson.Password,
                              ContactNumber = DeliveryPerson.ContactNumber,
                              RoleId = Role.RoleId,
                            
                           
                              RoleNmae = Role.RoleName
                          }).FirstOrDefault();
            return result;
        }

    }
}
