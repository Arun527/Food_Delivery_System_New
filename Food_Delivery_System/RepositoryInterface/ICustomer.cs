
using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface ICustomer
    {
        public  IEnumerable<Customer> GetAll();
        public Customer GetCustomerDetail(int customerId);
        public Messages InsertCustomerDetail(Customer customer);

        public Messages UpdateCustomerDetail(Customer customer);

        public Messages DeleteCustomerDetail(int customerId);

   
        public LoginDto loginbyid(string contactNumber, string password); 
    }
}
