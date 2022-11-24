
using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface ICustomer
    {
        public  IEnumerable<Customer> GetAll();
        public Customer GetCustomerDetailById(int customerId);
        public Customer GetCustomerDetailByNumber(String Number);
        public Customer GetCustomerDetailByEmail(String Email);
        public Messages InsertCustomerDetail(Customer customer);

        public Messages UpdateCustomerDetail(Customer customer);  

        public Messages DeleteCustomerDetail(int customerId);

        public Customer GetNumber(string Number);  


    }
}
