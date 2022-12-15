
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Server.IIS.Core;
using ServiceStack.Messaging;
using static Food_Delivery.Models.Messages;

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

        public IEnumerable<Customer> GetCustomerDetailByIsActive(bool isActive)
        {

            try
            {
                Message message = new Message();
                
                var customerList = db.Customer.Where(x => x.IsActive == isActive).ToList();
             
             
                return customerList;
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
                    msg.number = false;
                    msg.Message = "This contact number already exists";
                    msg.Status = Statuses.Conflict;
                    return msg;
                }
                if (customerEmail != null)
                {
                    msg.Success = false;
                    msg.Message = "This email id already exists";
                    msg.Status= Statuses.Conflict;
                    return msg;
                }
                else
                {
                    db.Add(customer);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "This  customer added succesfully";
                    msg.Status = Statuses.Created;
                    return msg;
                }
                
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

                Messages messages = new Messages();
                messages.Success = false;
                var userExist = GetCustomerDetailById(customer.CustomerId);
                var phoneExist = GetCustomerDetailByNumber(customer.ContactNumber);
                var emailIdExist = GetCustomerDetailByEmail(customer.Email);
                if (userExist == null)
                {
                    messages.Message = "customer id is not found";
                    messages.Status = Statuses.NotFound;
                    return messages;
                }
                else if (phoneExist != null && phoneExist.CustomerId != userExist.CustomerId)
                {
                    messages.Message = "The (" + customer.ContactNumber + "), Phone number is already registered.";
                    messages.number = false;
                    messages.Status = Statuses.Conflict;
                    return messages;
                }
                else if (emailIdExist != null && emailIdExist.CustomerId != userExist.CustomerId)
                {
                    messages.Message = "The (" + customer.Email + "), Email id is already registered.";
                    messages.Status = Statuses.Conflict;
                    return messages;
                }
                else
                {
                    userExist.Name = customer.Name;
                    userExist.Email = customer.Email;
                    userExist.Gender = customer.Gender;
                    userExist.Address = customer.Address;
                    userExist.ContactNumber = customer.ContactNumber;
                    userExist.IsActive = customer.IsActive;
                    db.Update(userExist);
                    db.SaveChanges();
                    msg.Success = true;
                    messages.Status = Statuses.Success;
                    msg.Message = "Customer updated succesfully!!";
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
            var order = db.OrderDetail.FirstOrDefault(x => x.CustomerId == customerId);
            if (deleteCustomer != null && order==null)
            {
                db.Remove(deleteCustomer);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Customer deleted succesfully";
                msg.Status= Statuses.Success;
               
            }
            else if(order!=null)
            {
                msg.Success = false;
                msg.Message = "This customer ordered food..!, so you can't delete";
                msg.Status = Statuses.BadRequest;
            }
            else
            {
                msg.Success = false;
                msg.Message = "This customer id not registered";
                msg.Status = Statuses.NotFound;
            }
            return msg;
        }

        public Customer  GetNumber(string Number)
        {
            var customer = db.Customer.FirstOrDefault(x => x.ContactNumber == Number);
           
               
            return customer;

        }
      
    }

 }
    
   