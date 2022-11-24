
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Server.IIS.Core;
using ServiceStack.Messaging;

namespace Food_Delivery.RepositoryService
{
    public class CustomerService : ICustomer
    {


        private readonly FoodDeliveryDbContext db;
        public CustomerService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customer.ToList();
        }


        public Customer GetCustomerDetailById(int customerId)
        {

            try
            {
                Message message = new Message();
                var getId = db.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                return getId;
            }

            catch (Exception)
            {
                throw;
            }

        }



        public Customer GetCustomerDetailByNumber(string Number)
        {

            try
            {
                Message message = new Message();
                var getId = db.Customer.FirstOrDefault(x => x.ContactNumber == Number);
                return getId;
            }

            catch (Exception)
            {
                throw;
            }


        }



        public Customer GetCustomerDetailByEmail(String Email)
        {

            try
            {
                Message message = new Message();
                var getId = db.Customer.FirstOrDefault(x => x.Email == Email);
                return getId;
            }

            catch (Exception)
            {
                throw;
            }


        }

        public Messages InsertCustomerDetail(Customer customer)
        {
            Messages msg = new Messages();
            try
            {
                var customerNum = db.Customer.FirstOrDefault(x => x.ContactNumber == customer.ContactNumber);
                var customerEmail = db.Customer.FirstOrDefault(x => x.Email == customer.Email);

                if (customerNum != null)
                {

                    msg.Success = false;
                    msg.Message = "This Contact Number Already Exists";
                    return msg;
                }
                if (customerEmail != null)
                {
                    msg.Success = false;
                    msg.Message = "This EmailId Already Exists";
                    
                }
                else
                {
                    db.Add(customer);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "This  Customer Added Succesfully";
                   
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }


        public Messages UpdateCustomerDetail(Customer customer)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This Customer id not registered";
                var updateCustomer = db.Customer.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (updateCustomer != null)
                {
                   
                        updateCustomer.Name = customer.Name;
                        updateCustomer.Email = customer.Email;
                        updateCustomer.Gender = customer.Gender;
                        updateCustomer.Address = customer.Address;
                        updateCustomer.ContactNumber = customer.ContactNumber;
                        db.Update(updateCustomer);
                        db.SaveChanges();
                        msg.Success = true;
                        msg.Message = "Customer Updated Succesfully!!"; 
                }
                return msg;
            }

            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }
        public Messages DeleteCustomerDetail(int customerId)
        {
            Messages msg = new Messages();
            var deleteCustomer = db.Customer.FirstOrDefault(x => x.CustomerId == customerId);
            if (deleteCustomer != null)
            {
                db.Remove(deleteCustomer);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Customer Deleted Succesfully";
            }
            else
            {
                msg.Success = false;
                msg.Message = "This Customer Id Not Registered";
            }
            return msg;
        }

        //public LoginDto loginbyid(string contactNumber, string password)
        //{
        //    var result = (from Role in db.Role
        //                  join Customer in db.Customer on Role.RoleId equals Customer.RoleId


        //                  where Customer.ContactNumber == contactNumber && Customer.Password == password

        //                    select new LoginDto()
        //                    {
        //                     Password = Customer.Password,
        //                     ContactNumber=Customer.ContactNumber,
        //                     RoleId=Role.RoleId,
        //                     CustomerId=Customer.CustomerId,
        //                     Gender=Customer.Gender,
        //                     RoleNmae=Role.RoleName
        //                    }).FirstOrDefault();
        //    return result;
        //    }


        public Customer  GetNumber(string Number)
        {
           


            var custbfomer = db.Customer.FirstOrDefault(x => x.ContactNumber == Number);
           
               
            return custbfomer;

        }

      
    }

 }
    
   