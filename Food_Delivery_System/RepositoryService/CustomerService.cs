
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
                    msg.Message = "This contact number already exists";
                    return msg;
                }
                if (customerEmail != null)
                {
                    msg.Success = false;
                    msg.Message = "This email id already exists";
                    
                }
                else
                {
                    db.Add(customer);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "This  customer added succesfully";
                   
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

                Messages messages = new Messages();
                messages.Success = false;
                var userExist = GetCustomerDetailById(customer.CustomerId);
                var phoneExist = GetCustomerDetailByNumber(customer.ContactNumber);
                var emailIdExist = GetCustomerDetailByEmail(customer.Email);
                if (userExist == null)
                {
                    messages.Message = "User id is not found";
                    return messages;
                }
                else if (phoneExist != null && phoneExist.CustomerId != userExist.CustomerId)
                {
                    messages.Message = "The (" + customer.ContactNumber + "), Phone number is already registered.";
                    return messages;
                }
                else if (emailIdExist != null && emailIdExist.CustomerId != userExist.CustomerId)
                {
                    messages.Message = "The (" + customer.Email + "), Email id is already registered.";
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
            }
            else if(order!=null)
            {
                msg.Success = false;
                msg.Message = "This customer ordered food..!";
            }
            else
            {
                msg.Success = false;
                msg.Message = "This customer id not registered";
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
    
   