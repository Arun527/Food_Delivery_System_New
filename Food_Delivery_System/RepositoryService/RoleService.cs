using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;

namespace Food_Delivery.RepositoryService
{
    public class RoleService : IRole
    {
        private readonly FoodDeliveryDbContext db;
        public RoleService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Role.ToList();
        }


        public Messages InsertRole(Role roll)
        {
            Messages msg = new Messages();
            try
            {
                    db.Add(roll);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Role Added Succesfully";
                    return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }


        public Messages UpdateRole(Role roll)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This Role id not registered";
                var updateRoll = db.Role.FirstOrDefault(x => x.RoleId == roll.RoleId);
                if (updateRoll != null)
                {
                    updateRoll.RoleName = roll.RoleName;
                 
                    db.Update(roll);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Role Updated Succesfully!!";
                }
                return msg;
            }

            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }


        public Messages DeleteRole(int RollId)
        {
            Messages msg = new Messages();
            var deleteRole = db.Role.FirstOrDefault(x => x.RoleId == RollId);
            if (deleteRole != null)
            {
                db.Remove(deleteRole);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Role Deleted Succesfully";
            }
            return msg;
        }

    }
}
